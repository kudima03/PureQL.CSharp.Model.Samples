using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record InnerJoinOnNegatedKeyEqualityQueryTests
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
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "eachNot",
                        "condition": {
                          "operator": "eachEqual",
                          "left": {
                            "entity": "schema_with_foreign_keys.users",
                            "field": "user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "right": {
                            "entity": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InnerJoinOnNegatedKeyEqualityQuery().Value).TextValue
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
                      "Bob"
                    ],
                    [
                      "Cara"
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
                      "Cara"
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
                      "Ann"
                    ],
                    [
                      "Cara"
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
                      "Ann"
                    ],
                    [
                      "Bob"
                    ],
                    [
                      "Cara"
                    ],
                    [
                      "Eve"
                    ],
                    [
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InnerJoinOnNegatedKeyEqualityQuery().Result).TextValue
        );
    }
}
