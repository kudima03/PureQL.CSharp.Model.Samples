using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record PairStatisticsOperandKindsQueryTests
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
                        "operator": "covariancePopulation",
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
                        "operator": "covariancePopulation",
                        "type": {
                          "name": "decimal"
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
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_integers",
                      "expression": {
                        "operator": "covariancePopulation",
                        "type": {
                          "name": "decimal"
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
                            "value": 3
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_nullable_left",
                      "expression": {
                        "operator": "covariancePopulation",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "decimal",
                            "nullable": true
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
                              "name": "integer"
                            },
                            "value": 2
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_nullable_right",
                      "expression": {
                        "operator": "covariancePopulation",
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
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
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
                      "alias": "with_a_predicate",
                      "expression": {
                        "operator": "covariancePopulation",
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
                        "operator": "covariancePopulation",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        }
                      }
                    },
                    {
                      "alias": "sample_of_non_null_operands",
                      "expression": {
                        "operator": "covarianceSample",
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
                      "alias": "correlation_of_a_column_with_itself",
                      "expression": {
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
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_parameter",
                      "expression": {
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
            new QueryJson(new PairStatisticsOperandKindsQuery().Value).TextValue
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
                      "name": "of_integers",
                      "type": "double"
                    },
                    {
                      "name": "of_a_nullable_left",
                      "type": "double"
                    },
                    {
                      "name": "of_a_nullable_right",
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
                      "name": "sample_of_non_null_operands",
                      "type": "double"
                    },
                    {
                      "name": "correlation_of_a_column_with_itself",
                      "type": "double"
                    },
                    {
                      "name": "of_a_parameter",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "31.26376875",
                      "-7.463125",
                      "0.625",
                      "-10.326666666666666",
                      "-10.326666666666666",
                      "-3.05",
                      "0",
                      "-9.950833333333334",
                      "1",
                      ""
                    ],
                    [
                      "31.26376875",
                      "-7.463125",
                      "0.625",
                      "-10.326666666666666",
                      "-10.326666666666666",
                      "-3.05",
                      "0",
                      "-9.950833333333334",
                      "1",
                      ""
                    ],
                    [
                      "31.26376875",
                      "-7.463125",
                      "0.625",
                      "-10.326666666666666",
                      "-10.326666666666666",
                      "-3.05",
                      "0",
                      "-9.950833333333334",
                      "1",
                      ""
                    ],
                    [
                      "31.26376875",
                      "-7.463125",
                      "0.625",
                      "-10.326666666666666",
                      "-10.326666666666666",
                      "-3.05",
                      "0",
                      "-9.950833333333334",
                      "1",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PairStatisticsOperandKindsQuery().Result).TextValue
        );
    }
}
