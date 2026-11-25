using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record LongOperandListsQueryTests
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
                      "kind": "left",
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
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "integers",
                      "expression": {
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
                            "value": -2
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 5
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
                    },
                    {
                      "alias": "decimal_first",
                      "expression": {
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
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "decimal_last",
                      "expression": {
                        "operator": "add",
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
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.5"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "decimal_inside",
                      "expression": {
                        "operator": "multiply",
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
                              "name": "decimal"
                            },
                            "value": "1.5"
                          },
                          {
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
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 3
                          }
                        ]
                      }
                    },
                    {
                      "alias": "nullable_last",
                      "expression": {
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
                              "name": "decimal"
                            },
                            "value": "0.2"
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
                      }
                    },
                    {
                      "alias": "nullable_first",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
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
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
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
                    {
                      "alias": "integer_param",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "integer",
                          "nullable": true
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
                          {
                            "operator": "round",
                            "type": {
                              "name": "integer",
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
                        ]
                      }
                    },
                    {
                      "alias": "divide_integers",
                      "expression": {
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
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        ]
                      }
                    },
                    {
                      "alias": "smallest_step",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "1.5"
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.0000000000000000000000000001"
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
                    {
                      "alias": "largest_integers",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 9223372036854775807
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LongOperandListsQuery().Value).TextValue
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
                      "name": "integers",
                      "type": "long"
                    },
                    {
                      "name": "decimal_first",
                      "type": "double"
                    },
                    {
                      "name": "decimal_last",
                      "type": "double"
                    },
                    {
                      "name": "decimal_inside",
                      "type": "double"
                    },
                    {
                      "name": "nullable_last",
                      "type": "double"
                    },
                    {
                      "name": "nullable_first",
                      "type": "double"
                    },
                    {
                      "name": "integer_param",
                      "type": "long"
                    },
                    {
                      "name": "divide_integers",
                      "type": "double"
                    },
                    {
                      "name": "smallest_step",
                      "type": "double"
                    },
                    {
                      "name": "largest_integers",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "8",
                      "6.99",
                      "3.5",
                      "0",
                      "119.88",
                      "2.5",
                      "-28",
                      "0.3333333333333333",
                      "6E-28",
                      "9223372036854775807"
                    ],
                    [
                      "7",
                      "16.99",
                      "3.5",
                      "4.5",
                      "119.94",
                      "5",
                      "-30",
                      "0.16666666666666666",
                      "6E-28",
                      "9223372036854775807"
                    ],
                    [
                      "15",
                      "-2.5",
                      "7.5",
                      "22.5",
                      "",
                      "",
                      "",
                      "0.8333333333333334",
                      "6E-28",
                      "9223372036854775807"
                    ],
                    [
                      "11",
                      "4.99",
                      "5.5",
                      "13.5",
                      "179.82",
                      "1.6666666666666667",
                      "-28",
                      "0.5",
                      "6E-28",
                      "9223372036854775807"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LongOperandListsQuery().Result).TextValue
        );
    }
}
