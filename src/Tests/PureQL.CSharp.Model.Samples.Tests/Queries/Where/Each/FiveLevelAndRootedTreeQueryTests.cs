using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record FiveLevelAndRootedTreeQueryTests
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
                            "operator": "eachNot",
                            "condition": {
                              "operator": "eachAnd",
                              "conditions": [
                                {
                                  "operator": "eachGreaterThan",
                                  "left": {
                                    "entity": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "number"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "number"
                                    },
                                    "value": 100
                                  }
                                },
                                {
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
                                    "value": "pending"
                                  }
                                }
                              ]
                            }
                          },
                          {
                            "operator": "eachGreaterThanOrEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "number"
                              }
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
                        "operator": "eachOr",
                        "conditions": [
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
                          },
                          {
                            "operator": "eachAnd",
                            "conditions": [
                              {
                                "operator": "eachLessThan",
                                "left": {
                                  "entity": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "number"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "number"
                                  },
                                  "value": 100
                                }
                              },
                              {
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
                                  "value": "shipped"
                                }
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new FiveLevelAndRootedTreeQuery().Value).TextValue
        );
    }
}
