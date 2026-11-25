using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record OrderKpisInOneRowQueryTests
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
                  "aggregate": [
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "shipped_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
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
                            "value": "shipped"
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_share",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        },
                        "then": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "else": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              },
                              "predicate": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
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
                                  "value": "shipped"
                                }
                              }
                            },
                            {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "any_cancelled",
                      "expression": {
                        "operator": "any",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
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
                    },
                    {
                      "alias": "latest_order_at",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
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
            new QueryJson(new OrderKpisInOneRowQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "shipped_orders",
                      "type": "long"
                    },
                    {
                      "name": "shipped_share",
                      "type": "double"
                    },
                    {
                      "name": "any_cancelled",
                      "type": "bool"
                    },
                    {
                      "name": "latest_order_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6",
                      "3",
                      "0.5",
                      "True",
                      "2024-06-06T15:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderKpisInOneRowQuery().Result).TextValue
        );
    }
}
