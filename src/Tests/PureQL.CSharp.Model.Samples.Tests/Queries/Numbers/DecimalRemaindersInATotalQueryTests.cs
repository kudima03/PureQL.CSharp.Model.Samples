using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record DecimalRemaindersInATotalQueryTests
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
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "modulo",
                      "type": {
                        "name": "decimal"
                      },
                      "left": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": "0.1"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "fifties",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
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
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 50
                        }
                      }
                    },
                    {
                      "alias": "left_over",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
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
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 50
                        }
                      }
                    },
                    {
                      "alias": "average_cents",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "modulo",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_hours",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "average",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "datetimeDiffSeconds",
                            "type": {
                              "name": "decimal"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-05-31T12:30:00Z"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3600
                        }
                      }
                    },
                    {
                      "alias": "orders_per_ten",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "integer"
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
                          "value": 10
                        }
                      }
                    },
                    {
                      "alias": "orders_per_one_and_a_half",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1.5"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DecimalRemaindersInATotalQuery().Value).TextValue
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
                      "name": "fifties",
                      "type": "long"
                    },
                    {
                      "name": "left_over",
                      "type": "double"
                    },
                    {
                      "name": "average_cents",
                      "type": "double"
                    },
                    {
                      "name": "average_hours",
                      "type": "long"
                    },
                    {
                      "name": "orders_per_ten",
                      "type": "long"
                    },
                    {
                      "name": "orders_per_one_and_a_half",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "15",
                      "1",
                      "0.2",
                      "81",
                      "5",
                      "0.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DecimalRemaindersInATotalQuery().Result).TextValue
        );
    }
}
