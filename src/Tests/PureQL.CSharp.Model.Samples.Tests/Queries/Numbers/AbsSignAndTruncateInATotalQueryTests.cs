using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record AbsSignAndTruncateInATotalQueryTests
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
                      "alias": "imbalance",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
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
                                  "value": "pending"
                                }
                              }
                            },
                            {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "predicate": {
                                "operator": "not",
                                "type": {
                                  "name": "boolean"
                                },
                                "condition": {
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
                                    "value": "pending"
                                  }
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "mostly",
                      "expression": {
                        "operator": "sign",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
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
                                  "value": "pending"
                                }
                              }
                            },
                            {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "predicate": {
                                "operator": "not",
                                "type": {
                                  "name": "boolean"
                                },
                                "condition": {
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
                                    "value": "pending"
                                  }
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "average_to_cents",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "whole_average",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "smallest_size",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_against_target",
                      "expression": {
                        "operator": "sign",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
                            }
                          ]
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AbsSignAndTruncateInATotalQuery().Value).TextValue
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
                      "name": "imbalance",
                      "type": "double"
                    },
                    {
                      "name": "mostly",
                      "type": "long"
                    },
                    {
                      "name": "average_to_cents",
                      "type": "double"
                    },
                    {
                      "name": "whole_average",
                      "type": "long"
                    },
                    {
                      "name": "smallest_size",
                      "type": "double"
                    },
                    {
                      "name": "orders_against_target",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "525.25",
                      "-1",
                      "137.7",
                      "137",
                      "50",
                      "-1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AbsSignAndTruncateInATotalQuery().Result).TextValue
        );
    }
}
