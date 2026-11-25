using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record RegressionOperandKindsQueryTests
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
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "of_decimals",
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
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_an_integer_and_a_decimal",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "operator": "round",
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
                      "alias": "of_a_decimal_and_an_integer",
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
                          "operator": "round",
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
                      "alias": "of_integers",
                      "expression": {
                        "operator": "regressionIntercept",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "operator": "round",
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
                          "operator": "integerDivide",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "operator": "round",
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
                            "value": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_nullable_y",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "operator": "if",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "1"
                            }
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "value": null
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
                      "alias": "of_a_nullable_x",
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
                          "operator": "if",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "1"
                            }
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "with_a_predicate",
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
                          "operator": "round",
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
                            "operator": "round",
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
                      "alias": "of_literals",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "x": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        }
                      }
                    },
                    {
                      "alias": "of_nulls",
                      "expression": {
                        "operator": "regressionIntercept",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "x": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "of_a_parameter",
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
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    },
                    {
                      "alias": "of_a_constant_x",
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
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RegressionOperandKindsQuery().Value).TextValue
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
                      "name": "of_decimals",
                      "type": "double"
                    },
                    {
                      "name": "of_an_integer_and_a_decimal",
                      "type": "double"
                    },
                    {
                      "name": "of_a_decimal_and_an_integer",
                      "type": "double"
                    },
                    {
                      "name": "of_integers",
                      "type": "double"
                    },
                    {
                      "name": "of_a_nullable_y",
                      "type": "double"
                    },
                    {
                      "name": "of_a_nullable_x",
                      "type": "double"
                    },
                    {
                      "name": "with_a_predicate",
                      "type": "double"
                    },
                    {
                      "name": "of_literals",
                      "type": "double"
                    },
                    {
                      "name": "of_nulls",
                      "type": "double"
                    },
                    {
                      "name": "of_a_parameter",
                      "type": "double"
                    },
                    {
                      "name": "of_a_constant_x",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "-0.23871482224931695",
                      "-3.411714285714286",
                      "0.75",
                      "-0.4553734061930783",
                      "-1.9607142857142856",
                      "0.8928571428571429",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "1",
                      "-0.23871482224931695",
                      "-3.411714285714286",
                      "0.75",
                      "-0.4553734061930783",
                      "-1.9607142857142856",
                      "0.8928571428571429",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "1",
                      "-0.23871482224931695",
                      "-3.411714285714286",
                      "0.75",
                      "-0.4553734061930783",
                      "-1.9607142857142856",
                      "0.8928571428571429",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "1",
                      "-0.23871482224931695",
                      "-3.411714285714286",
                      "0.75",
                      "-0.4553734061930783",
                      "-1.9607142857142856",
                      "0.8928571428571429",
                      "",
                      "",
                      "",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegressionOperandKindsQuery().Result).TextValue
        );
    }
}
