using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record IntegersUsedAsTheyAreQueryTests
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
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
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
                            "value": 10
                          }
                        ]
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
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "quantity",
                      "expression": {
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
                              "value": 10
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "kept",
                      "expression": {
                        "operator": "subtract",
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
                                  "value": 10
                                }
                              ]
                            }
                          },
                          {
                            "operator": "round",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
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
                                      "value": 20
                                    }
                                  ]
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 55
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "dozens",
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
                                "value": 10
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
                      "alias": "left_over",
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
                                "value": 10
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
                      "alias": "lines",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "units",
                      "expression": {
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
                                  "name": "integer"
                                },
                                "value": 10
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "magnitude",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
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
                                    "value": 10
                                  }
                                ]
                              }
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
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
                                        "value": 20
                                      }
                                    ]
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 55
                                  }
                                ]
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "as_a_decimal",
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
                                  "value": 10
                                }
                              ]
                            }
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new IntegersUsedAsTheyAreQuery().Value).TextValue
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
                      "type": "long"
                    },
                    {
                      "name": "kept",
                      "type": "long"
                    },
                    {
                      "name": "dozens",
                      "type": "long"
                    },
                    {
                      "name": "left_over",
                      "type": "long"
                    },
                    {
                      "name": "lines",
                      "type": "long"
                    },
                    {
                      "name": "units",
                      "type": "long"
                    },
                    {
                      "name": "magnitude",
                      "type": "long"
                    },
                    {
                      "name": "as_a_decimal",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "20",
                      "-75",
                      "1",
                      "8",
                      "2",
                      "50",
                      "75",
                      "20"
                    ],
                    [
                      "30",
                      "-85",
                      "2",
                      "6",
                      "2",
                      "50",
                      "85",
                      "30"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new IntegersUsedAsTheyAreQuery().Result).TextValue
        );
    }
}
