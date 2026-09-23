using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record FourLevelTreeOverJoinedColumnsMixingCrossEntityArithmeticAndBooleanOpsQueryTests
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
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachAnd",
                    "conditions": [
                      {
                        "operator": "eachOr",
                        "conditions": [
                          {
                            "operator": "eachEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "operator": "eachGreaterThan",
                            "left": {
                              "operator": "eachAdd",
                              "values": [
                                {
                                  "entity": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "number"
                                  }
                                },
                                {
                                  "entity": "schema_with_foreign_keys.users",
                                  "field": "user_age",
                                  "type": {
                                    "name": "number"
                                  }
                                }
                              ]
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 300
                            }
                          }
                        ]
                      },
                      {
                        "operator": "eachNot",
                        "condition": {
                          "operator": "eachEqual",
                          "left": {
                            "entity": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "cancelled"
                          }
                        }
                      }
                    ]
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new FourLevelTreeOverJoinedColumnsMixingCrossEntityArithmeticAndBooleanOpsQuery().Value
            ).TextValue
        );
    }
}
