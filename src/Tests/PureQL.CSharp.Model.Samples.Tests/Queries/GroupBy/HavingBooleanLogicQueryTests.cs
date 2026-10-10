using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingBooleanLogicQueryTests
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
                  "groupBy": [
                    {
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "or",
                    "conditions": [
                      {
                        "operator": "and",
                        "conditions": [
                          {
                            "operator": "greaterThan",
                            "left": {
                              "operator": "sum",
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 250
                            }
                          },
                          {
                            "operator": "not",
                            "condition": {
                              "operator": "any",
                              "predicate": {
                                "operator": "equal",
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
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
                          }
                        ]
                      },
                      {
                        "operator": "greaterThanOrEqual",
                        "left": {
                          "operator": "count"
                        },
                        "right": {
                          "param_name": "min_orders",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "sum",
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new HavingBooleanLogicQuery().Value).TextValue
        );
    }
}
