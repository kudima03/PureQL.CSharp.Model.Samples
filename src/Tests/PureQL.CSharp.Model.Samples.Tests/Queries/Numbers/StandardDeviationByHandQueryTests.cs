using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record StandardDeviationByHandQueryTests
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
                  },
                  "aggregate": [
                    {
                      "alias": "mean",
                      "expression": {
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
                    },
                    {
                      "alias": "variance",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "subtract",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "values": [
                              {
                                "operator": "average",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "selector": {
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
                                }
                              },
                              {
                                "operator": "power",
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
                                "exponent": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2
                                }
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    },
                    {
                      "alias": "standard_deviation",
                      "expression": {
                        "operator": "sqrt",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "greatest",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "values": [
                            {
                              "operator": "subtract",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "values": [
                                {
                                  "operator": "average",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "selector": {
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
                                  }
                                },
                                {
                                  "operator": "power",
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
                                  "exponent": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 2
                                  }
                                }
                              ]
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "coefficient_of_variation",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "sqrt",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "operand": {
                              "operator": "greatest",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "values": [
                                {
                                  "operator": "subtract",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "values": [
                                    {
                                      "operator": "average",
                                      "type": {
                                        "name": "decimal",
                                        "nullable": true
                                      },
                                      "selector": {
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
                                      }
                                    },
                                    {
                                      "operator": "power",
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
                                      "exponent": {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 2
                                      }
                                    }
                                  ]
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 0
                                }
                              ]
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
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
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "geometric_mean",
                      "expression": {
                        "operator": "exp",
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
                            "operator": "ln",
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
                          "predicate": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
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
                              "value": 0
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "root_mean_square",
                      "expression": {
                        "operator": "sqrt",
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_as_a_power_of_two",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "greatest",
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new StandardDeviationByHandQuery().Value).TextValue
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
                      "name": "mean",
                      "type": "double"
                    },
                    {
                      "name": "variance",
                      "type": "double"
                    },
                    {
                      "name": "standard_deviation",
                      "type": "double"
                    },
                    {
                      "name": "coefficient_of_variation",
                      "type": "double"
                    },
                    {
                      "name": "geometric_mean",
                      "type": "double"
                    },
                    {
                      "name": "root_mean_square",
                      "type": "double"
                    },
                    {
                      "name": "orders_as_a_power_of_two",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "200.16666666666666",
                      "6633.388888888889",
                      "81.44561921238544",
                      "0.4068890218770297",
                      "182.01440935707507",
                      "216.102020659996",
                      "1.584962500721156"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new StandardDeviationByHandQuery().Result).TextValue
        );
    }
}
