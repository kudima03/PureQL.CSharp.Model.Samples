using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record RegressionInATotalQueryTests
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
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_product_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "floor",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_qty",
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
                  },
                  "aggregate": [
                    {
                      "alias": "slope",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "intercept",
                      "expression": {
                        "operator": "regressionIntercept",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "r_squared",
                      "expression": {
                        "operator": "regressionRSquared",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "slope_of_large_lines",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
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
                            "operator": "floor",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
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
                      "alias": "the_other_way",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "x": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "slope_by_hand",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "covariancePopulation",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "operator": "floor",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "operator": "variancePopulation",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "selector": {
                                "operator": "floor",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_qty",
                                  "type": {
                                    "name": "decimal"
                                  }
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
                      "alias": "correlation_squared",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "correlation",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "operator": "floor",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
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
                    },
                    {
                      "alias": "predicted_price_of_ten",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "regressionIntercept",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "y": {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "x": {
                              "operator": "floor",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "values": [
                              {
                                "operator": "regressionSlope",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "y": {
                                  "source": "schema_with_foreign_keys.products",
                                  "field": "product_price",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "x": {
                                  "operator": "floor",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
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
                        ]
                      }
                    },
                    {
                      "alias": "with_a_nullable_operand",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          ]
                        },
                        "x": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RegressionInATotalQuery().Value).TextValue
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
                      "name": "slope",
                      "type": "double"
                    },
                    {
                      "name": "intercept",
                      "type": "double"
                    },
                    {
                      "name": "r_squared",
                      "type": "double"
                    },
                    {
                      "name": "slope_of_large_lines",
                      "type": "double"
                    },
                    {
                      "name": "the_other_way",
                      "type": "double"
                    },
                    {
                      "name": "slope_by_hand",
                      "type": "double"
                    },
                    {
                      "name": "correlation_squared",
                      "type": "double"
                    },
                    {
                      "name": "predicted_price_of_ten",
                      "type": "double"
                    },
                    {
                      "name": "with_a_nullable_operand",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "-3.411714285714286",
                      "20.499714285714287",
                      "0.8144267692797411",
                      "-1.9607142857142856",
                      "-0.23871482224931695",
                      "-3.411714285714286",
                      "0.8144267692797411",
                      "-13.61742857142857",
                      "-150"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegressionInATotalQuery().Result).TextValue
        );
    }
}
