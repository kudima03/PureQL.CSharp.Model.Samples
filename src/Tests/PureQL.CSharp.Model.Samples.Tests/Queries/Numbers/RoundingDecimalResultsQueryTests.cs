using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundingDecimalResultsQueryTests
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
                      "alias": "of_a_quotient",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
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
                              "value": 3
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "of_a_product",
                      "expression": {
                        "operator": "ceiling",
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
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
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
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "of_a_power",
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
                            "value": "0.5"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_square_root",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
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
                      }
                    },
                    {
                      "alias": "of_a_decimal_coalesce",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "nullIfEqual",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "9.99"
                              }
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
                      "alias": "of_a_decimal_if",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "if",
                          "type": {
                            "name": "decimal"
                          },
                          "condition": {
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
                              "value": 2
                            }
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_nullable_decimal",
                      "expression": {
                        "operator": "ceiling",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "nullIfEqual",
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
                            "type": {
                              "name": "decimal"
                            },
                            "value": "9.99"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_decimal_literal",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "of_an_average_over_all",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                      }
                    },
                    {
                      "alias": "of_a_decimal_abs",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
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
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "9.99"
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_string_read_as_decimal",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "-3.5"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundingDecimalResultsQuery().Value).TextValue
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
                      "name": "of_a_quotient",
                      "type": "long"
                    },
                    {
                      "name": "of_a_product",
                      "type": "long"
                    },
                    {
                      "name": "of_a_power",
                      "type": "long"
                    },
                    {
                      "name": "of_a_square_root",
                      "type": "long"
                    },
                    {
                      "name": "of_a_decimal_coalesce",
                      "type": "long"
                    },
                    {
                      "name": "of_a_decimal_if",
                      "type": "long"
                    },
                    {
                      "name": "of_a_nullable_decimal",
                      "type": "long"
                    },
                    {
                      "name": "of_a_decimal_literal",
                      "type": "long"
                    },
                    {
                      "name": "of_an_average_over_all",
                      "type": "long"
                    },
                    {
                      "name": "of_a_decimal_abs",
                      "type": "long"
                    },
                    {
                      "name": "of_a_string_read_as_decimal",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0",
                      "20",
                      "1",
                      "1",
                      "0",
                      "0",
                      "",
                      "2",
                      "3",
                      "0",
                      "-4"
                    ],
                    [
                      "0",
                      "20",
                      "1",
                      "1",
                      "20",
                      "0",
                      "20",
                      "2",
                      "3",
                      "10",
                      "-4"
                    ],
                    [
                      "1",
                      "23",
                      "2",
                      "2",
                      "5",
                      "4",
                      "5",
                      "2",
                      "3",
                      "5",
                      "-4"
                    ],
                    [
                      "1",
                      "30",
                      "2",
                      "1",
                      "0",
                      "9",
                      "",
                      "2",
                      "3",
                      "0",
                      "-4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundingDecimalResultsQuery().Result).TextValue
        );
    }
}
