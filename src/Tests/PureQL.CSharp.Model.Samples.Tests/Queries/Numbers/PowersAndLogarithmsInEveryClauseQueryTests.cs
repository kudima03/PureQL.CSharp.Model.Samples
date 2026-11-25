using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record PowersAndLogarithmsInEveryClauseQueryTests
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
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "power",
                              "type": {
                                "name": "decimal"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "exponent": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 2
                              }
                            },
                            "right": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_age",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2000
                                }
                              ]
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "sqrt",
                      "type": {
                        "name": "decimal"
                      },
                      "operand": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "12"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "magnitude",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "log10",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "greatest",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "doublings",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "log",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "greatest",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            ]
                          },
                          "base": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "exp",
                      "type": {
                        "name": "decimal"
                      },
                      "operand": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "ln",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "greatest",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            ]
                          }
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 60
                    }
                  },
                  "select": [
                    {
                      "alias": "magnitude",
                      "expression": {
                        "key": "magnitude",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "doublings",
                      "expression": {
                        "key": "doublings",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "lower_bound",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        },
                        "exponent": {
                          "key": "magnitude",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
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
                      "alias": "average_root",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "sqrt",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "magnitude",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "doublings",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PowersAndLogarithmsInEveryClauseQuery().Value).TextValue
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
                      "name": "magnitude",
                      "type": "long"
                    },
                    {
                      "name": "doublings",
                      "type": "long"
                    },
                    {
                      "name": "lower_bound",
                      "type": "double"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "average_root",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "6",
                      "10",
                      "1",
                      "8.674675786448736"
                    ],
                    [
                      "2",
                      "6",
                      "100",
                      "2",
                      "10.024968827881711"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PowersAndLogarithmsInEveryClauseQuery().Result).TextValue
        );
    }
}
