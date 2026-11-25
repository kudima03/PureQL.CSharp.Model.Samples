using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AggregatesOverExactValuesQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "subqueries": [
                    {
                      "name": "by_order",
                      "query": {
                        "groupBy": [
                          {
                            "alias": "order_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "order_id",
                            "expression": {
                              "key": "order_id",
                              "type": {
                                "name": "uuid"
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
                            "alias": "all_units",
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
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_qty",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              },
                              "over": "all"
                            }
                          },
                          {
                            "alias": "mean",
                            "expression": {
                              "operator": "average",
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
                          },
                          {
                            "alias": "spread",
                            "expression": {
                              "operator": "variancePopulation",
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
                          },
                          {
                            "alias": "filtered",
                            "expression": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
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
                          }
                        ],
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
                        ]
                      }
                    },
                    {
                      "name": "overall",
                      "query": {
                        "aggregate": [
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
                            "alias": "mean",
                            "expression": {
                              "operator": "average",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "alias": "deviation",
                            "expression": {
                              "operator": "standardDeviationSample",
                              "type": {
                                "name": "decimal",
                                "nullable": true
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
                        ],
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
                        ]
                      }
                    }
                  ],
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
                      "alias": "units_so_far",
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
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "mean_of_window",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.2"
                            }
                          ]
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
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
                            }
                          ]
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AggregatesOverExactValuesQuery().Value).TextValue
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
                      "name": "units_so_far",
                      "type": "long"
                    },
                    {
                      "name": "mean_of_window",
                      "type": "double"
                    },
                    {
                      "name": "units",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2",
                      "3.997",
                      "18"
                    ],
                    [
                      "3",
                      "3.997",
                      "18"
                    ],
                    [
                      "8",
                      "4.5",
                      "18"
                    ],
                    [
                      "11",
                      "5.994",
                      "18"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AggregatesOverExactValuesQuery().Result).TextValue
        );
    }
}
