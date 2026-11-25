using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record DecimalRemainderCornersQueryTests
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
                      "alias": "fractional_part",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
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
                    },
                    {
                      "alias": "of_a_negative",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            },
                            {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "by_a_negative",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "-2"
                        }
                      }
                    },
                    {
                      "alias": "by_a_step",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "guarded",
                      "expression": {
                        "operator": "modulo",
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
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "left": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "integers_stay_integer",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        }
                      }
                    },
                    {
                      "alias": "integer_by_decimal",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "decimal_by_integer_field",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
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
                      }
                    },
                    {
                      "alias": "nullable",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "steps",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "steps_of_an_integer",
                      "expression": {
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
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "guarded_steps",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer",
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
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "left": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "identity_holds",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "operator": "add",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.25"
                                },
                                {
                                  "operator": "integerDivide",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_price",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "0.25"
                                  }
                                }
                              ]
                            },
                            {
                              "operator": "modulo",
                              "type": {
                                "name": "decimal"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0.25"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "whole_part",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
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
                    },
                    {
                      "alias": "same_as_truncate",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "integerDivide",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
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
                        },
                        "right": {
                          "operator": "truncate",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "nested",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "operator": "modulo",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DecimalRemainderCornersQuery().Value).TextValue
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
                      "name": "fractional_part",
                      "type": "double"
                    },
                    {
                      "name": "of_a_negative",
                      "type": "double"
                    },
                    {
                      "name": "by_a_negative",
                      "type": "double"
                    },
                    {
                      "name": "by_a_step",
                      "type": "double"
                    },
                    {
                      "name": "guarded",
                      "type": "double"
                    },
                    {
                      "name": "integers_stay_integer",
                      "type": "long"
                    },
                    {
                      "name": "integer_by_decimal",
                      "type": "double"
                    },
                    {
                      "name": "decimal_by_integer_field",
                      "type": "double"
                    },
                    {
                      "name": "nullable",
                      "type": "double"
                    },
                    {
                      "name": "steps",
                      "type": "long"
                    },
                    {
                      "name": "steps_of_an_integer",
                      "type": "long"
                    },
                    {
                      "name": "guarded_steps",
                      "type": "long"
                    },
                    {
                      "name": "identity_holds",
                      "type": "bool"
                    },
                    {
                      "name": "whole_part",
                      "type": "long"
                    },
                    {
                      "name": "same_as_truncate",
                      "type": "bool"
                    },
                    {
                      "name": "nested",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0.99",
                      "-1.99",
                      "1.99",
                      "0.24",
                      "",
                      "6",
                      "1",
                      "3.99",
                      "",
                      "39",
                      "2",
                      "",
                      "True",
                      "9",
                      "True",
                      "0.24"
                    ],
                    [
                      "0.99",
                      "-1.99",
                      "1.99",
                      "0.24",
                      "",
                      "3",
                      "0.5",
                      "1.99",
                      "",
                      "79",
                      "1",
                      "",
                      "True",
                      "19",
                      "True",
                      "0.24"
                    ],
                    [
                      "0.5",
                      "-0.5",
                      "0.5",
                      "0",
                      "",
                      "3",
                      "0",
                      "4.5",
                      "",
                      "18",
                      "6",
                      "",
                      "True",
                      "4",
                      "True",
                      "0"
                    ],
                    [
                      "0.99",
                      "-1.99",
                      "1.99",
                      "0.24",
                      "",
                      "9",
                      "1.5",
                      "0.99",
                      "",
                      "39",
                      "3",
                      "",
                      "True",
                      "9",
                      "True",
                      "0.24"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DecimalRemainderCornersQuery().Result).TextValue
        );
    }
}
