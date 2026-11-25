using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record PowersAndLogarithmsCornersQueryTests
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
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
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
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "integer_operands",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "exponent": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        }
                      }
                    },
                    {
                      "alias": "as_an_integer",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "power",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          },
                          "exponent": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        }
                      }
                    },
                    {
                      "alias": "negative_exponent",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "exponent": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -2
                        }
                      }
                    },
                    {
                      "alias": "fractional_exponent",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "exponent": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    },
                    {
                      "alias": "cube_root",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "exponent": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "parameter_exponent",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
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
                        "exponent": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1.5"
                        }
                      }
                    },
                    {
                      "alias": "nullable_operand",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
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
                      "alias": "power_of_a_power",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "power",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
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
                        "exponent": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    },
                    {
                      "alias": "sqrt_of_integer",
                      "expression": {
                        "operator": "sqrt",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
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
                      "alias": "sqrt_nullable",
                      "expression": {
                        "operator": "sqrt",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "exp_of_integer",
                      "expression": {
                        "operator": "exp",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
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
                      "alias": "exp_of_ln",
                      "expression": {
                        "operator": "exp",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "ln",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "ln_of_integer_literal",
                      "expression": {
                        "operator": "ln",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        }
                      }
                    },
                    {
                      "alias": "log10_of_decimal_literal",
                      "expression": {
                        "operator": "log10",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.001"
                        }
                      }
                    },
                    {
                      "alias": "log_to_a_parameter_base",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "base": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "4"
                        }
                      }
                    },
                    {
                      "alias": "log_to_a_computed_base",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "base": {
                          "operator": "add",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
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
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "log_to_a_fractional_base",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
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
                        "base": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PowersAndLogarithmsCornersQuery().Value).TextValue
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
                      "name": "integer_operands",
                      "type": "double"
                    },
                    {
                      "name": "as_an_integer",
                      "type": "long"
                    },
                    {
                      "name": "negative_exponent",
                      "type": "double"
                    },
                    {
                      "name": "fractional_exponent",
                      "type": "double"
                    },
                    {
                      "name": "cube_root",
                      "type": "double"
                    },
                    {
                      "name": "parameter_exponent",
                      "type": "double"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "double"
                    },
                    {
                      "name": "power_of_a_power",
                      "type": "double"
                    },
                    {
                      "name": "sqrt_of_integer",
                      "type": "double"
                    },
                    {
                      "name": "sqrt_nullable",
                      "type": "double"
                    },
                    {
                      "name": "exp_of_integer",
                      "type": "double"
                    },
                    {
                      "name": "exp_of_ln",
                      "type": "double"
                    },
                    {
                      "name": "ln_of_integer_literal",
                      "type": "double"
                    },
                    {
                      "name": "log10_of_decimal_literal",
                      "type": "double"
                    },
                    {
                      "name": "log_to_a_parameter_base",
                      "type": "double"
                    },
                    {
                      "name": "log_to_a_computed_base",
                      "type": "double"
                    },
                    {
                      "name": "log_to_a_fractional_base",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1024",
                      "1024",
                      "0.01002003004005006",
                      "3.1606961258558215",
                      "2.1537163056204958",
                      "2.8284271247461903",
                      "900",
                      "9.99",
                      "1.4142135623730951",
                      "5.477225575051661",
                      "7.38905609893065",
                      "30",
                      "2.302585092994046",
                      "-3",
                      "1.6602423390088468",
                      "3.0959032742893844",
                      "-1"
                    ],
                    [
                      "1024",
                      "1024",
                      "0.002502501876250782",
                      "4.471017781221631",
                      "2.7139651382373664",
                      "1",
                      "900",
                      "19.99",
                      "1",
                      "5.477225575051661",
                      "2.718281828459045",
                      "30",
                      "2.302585092994046",
                      "-3",
                      "2.1606032834849516",
                      "4.906890595608519",
                      "0"
                    ],
                    [
                      "1024",
                      "1024",
                      "0.04938271604938271",
                      "2.1213203435596424",
                      "1.6509636244473134",
                      "11.180339887498949",
                      "",
                      "4.5",
                      "2.23606797749979",
                      "",
                      "148.4131591025766",
                      "",
                      "2.302585092994046",
                      "-3",
                      "1.084962500721156",
                      "",
                      "-2.321928094887362"
                    ],
                    [
                      "1024",
                      "1024",
                      "0.01002003004005006",
                      "3.1606961258558215",
                      "2.1537163056204958",
                      "5.196152422706632",
                      "900",
                      "9.99",
                      "1.7320508075688772",
                      "5.477225575051661",
                      "20.085536923187668",
                      "30",
                      "2.302585092994046",
                      "-3",
                      "1.6602423390088468",
                      "2.4534452978042594",
                      "-1.584962500721156"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PowersAndLogarithmsCornersQuery().Result).TextValue
        );
    }
}
