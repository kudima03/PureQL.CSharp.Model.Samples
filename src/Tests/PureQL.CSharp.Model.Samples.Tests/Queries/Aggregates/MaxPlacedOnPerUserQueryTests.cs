using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record MaxPlacedOnPerUserQueryTests
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
                      "operator": "max_date",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      },
                      "alias": "max_placed_on"
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MaxPlacedOnPerUserQuery().Value).TextValue
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
                      "name": "max_placed_on",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-02"
                    ],
                    [
                      "2024-06-03"
                    ],
                    [
                      "2024-06-06"
                    ],
                    [
                      "2024-06-05"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MaxPlacedOnPerUserQuery().Result).TextValue
        );
    }
}
