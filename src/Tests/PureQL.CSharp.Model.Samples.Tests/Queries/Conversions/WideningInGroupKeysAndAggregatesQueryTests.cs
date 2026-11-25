using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record WideningInGroupKeysAndAggregatesQueryTests
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
                  "groupBy": [
                    {
                      "alias": "quantity",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
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
                                  "name": "decimal"
                                },
                                "value": "0.5"
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "optional_order_id",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
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
                      "operator": "asDecimal",
                      "type": {
                        "name": "decimal"
                      },
                      "operand": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "1.5"
                    }
                  },
                  "select": [
                    {
                      "alias": "quantity",
                      "expression": {
                        "key": "quantity",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "optional_order_id",
                      "expression": {
                        "key": "optional_order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "lines",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "units",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
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
                                    "name": "decimal"
                                  },
                                  "value": "0.5"
                                }
                              ]
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "largest_quantity",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
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
                                    "name": "decimal"
                                  },
                                  "value": "0.5"
                                }
                              ]
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "optional_units",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
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
                                    "name": "decimal"
                                  },
                                  "value": "0.5"
                                }
                              ]
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_price_or_nothing",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                      "alias": "lines_unless_one",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1"
                        }
                      }
                    },
                    {
                      "alias": "larger_of_lines_and_key",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "asDecimal",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "key": "quantity",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "quantity",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "optional_order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WideningInGroupKeysAndAggregatesQuery().Value).TextValue
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
                      "name": "quantity",
                      "type": "double"
                    },
                    {
                      "name": "optional_order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "lines",
                      "type": "double"
                    },
                    {
                      "name": "units",
                      "type": "double"
                    },
                    {
                      "name": "largest_quantity",
                      "type": "double"
                    },
                    {
                      "name": "optional_units",
                      "type": "long"
                    },
                    {
                      "name": "first_price_or_nothing",
                      "type": "double"
                    },
                    {
                      "name": "lines_unless_one",
                      "type": "double"
                    },
                    {
                      "name": "larger_of_lines_and_key",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "00000065-0000-0000-0000-000000000000",
                      "2",
                      "2",
                      "1",
                      "2",
                      "9.99",
                      "2",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WideningInGroupKeysAndAggregatesQuery().Result).TextValue
        );
    }
}
