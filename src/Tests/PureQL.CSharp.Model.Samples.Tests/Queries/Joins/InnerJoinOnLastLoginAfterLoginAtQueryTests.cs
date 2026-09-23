using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record InnerJoinOnLastLoginAfterLoginAtQueryTests
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
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "audit.logins",
                      "on": {
                        "operator": "eachGreaterThan",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "entity": "audit.logins",
                          "field": "login_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InnerJoinOnLastLoginAfterLoginAtQuery().Value).TextValue
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
                      "Bob"
                    ],
                    [
                      "Dan"
                    ],
                    [
                      "Eve"
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
                    ],
                    [
                      "Dan"
                    ],
                    [
                      "Eve"
                    ],
                    [
                      "Eve"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InnerJoinOnLastLoginAfterLoginAtQuery().Result).TextValue
        );
    }
}
