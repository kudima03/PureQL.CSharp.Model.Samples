using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectDateAndDateTimeColumnsTogetherFromOrdersQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_at",
                      "type": {
                        "name": "datetime"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SelectDateAndDateTimeColumnsTogetherFromOrdersQuery().Value
            ).TextValue
        );
    }

    [Fact]
    public void ResultMatchesExpectedRows()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "name": "",
                  "columns": [
                    {
                      "name": "placed_on",
                      "type": "date"
                    },
                    {
                      "name": "placed_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "2024-06-01T10:00:00"
                    ],
                    [
                      "2024-06-02",
                      "2024-06-02T11:00:00"
                    ],
                    [
                      "2024-06-03",
                      "2024-06-03T12:00:00"
                    ],
                    [
                      "2024-06-04",
                      "2024-06-04T13:00:00"
                    ],
                    [
                      "2024-06-05",
                      "2024-06-05T14:00:00"
                    ],
                    [
                      "2024-06-06",
                      "2024-06-06T15:00:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectDateAndDateTimeColumnsTogetherFromOrdersQuery().Result
            ).TextValue
        );
    }
}
