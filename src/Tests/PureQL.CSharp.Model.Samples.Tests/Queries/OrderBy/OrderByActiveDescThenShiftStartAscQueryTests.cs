using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByActiveDescThenShiftStartAscQueryTests
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
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_active",
                        "type": {
                          "name": "boolean"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByActiveDescThenShiftStartAscQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann"
                    ],
                    [
                      "Cara"
                    ],
                    [
                      "Fay"
                    ],
                    [
                      "Dan"
                    ],
                    [
                      "Eve"
                    ],
                    [
                      "Bob"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new OrderByActiveDescThenShiftStartAscQuery().Result
            ).TextValue
        );
    }
}
