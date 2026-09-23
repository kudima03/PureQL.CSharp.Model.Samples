using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record CalendarAndNumericQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_edge_datetime",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_precision_value",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CalendarAndNumericQuery().Value).TextValue
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
                      "name": "user_edge_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "user_precision_value",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-02-29T00:00:00",
                      "1.7976931348623157E\u002B308"
                    ],
                    [
                      "2024-12-31T23:59:59",
                      "-1.7976931348623157E\u002B308"
                    ],
                    [
                      "2024-03-10T02:30:00",
                      "5E-324"
                    ],
                    [
                      "2024-11-03T01:30:00",
                      "-5E-324"
                    ],
                    [
                      "0001-01-01T00:00:00",
                      "1E\u002B308"
                    ],
                    [
                      "9999-12-31T23:59:59",
                      "123456789.123456"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CalendarAndNumericQuery().Result).TextValue
        );
    }
}
