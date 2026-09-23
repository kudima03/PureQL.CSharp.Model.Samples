using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByFullTieOnEveryKeyQueryTests
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
                        "field": "signup_date",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      }
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
            new QueryJson(new OrderByFullTieOnEveryKeyQuery().Value).TextValue
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
                      "Cara"
                    ],
                    [
                      "Ann"
                    ],
                    [
                      "Fay"
                    ],
                    [
                      "Bob"
                    ],
                    [
                      "Dan"
                    ],
                    [
                      "Eve"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByFullTieOnEveryKeyQuery().Result).TextValue
        );
    }
}
