using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record DailyInStockRevenueShareQueryTests
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
                      "alias": "placed_on",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "placed_on",
                      "expression": {
                        "key": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
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
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            ]
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "in_stock_revenue",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
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
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
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
                          "predicate": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_in_stock",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "out_of_stock_revenue",
                      "expression": {
                        "operator": "round",
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
                              "operator": "sum",
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
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_price",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                ]
                              }
                            },
                            {
                              "operator": "sum",
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
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
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
                              "predicate": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_in_stock",
                                "type": {
                                  "name": "boolean"
                                }
                              }
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "in_stock_pct",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            },
                            {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "operator": "sum",
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
                                        "source": "schema_with_foreign_keys.order_items",
                                        "field": "item_qty",
                                        "type": {
                                          "name": "decimal"
                                        }
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
                                  "predicate": {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_in_stock",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  }
                                },
                                {
                                  "operator": "sum",
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
                                        "source": "schema_with_foreign_keys.order_items",
                                        "field": "item_qty",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      {
                                        "source": "schema_with_foreign_keys.products",
                                        "field": "product_price",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      }
                                    ]
                                  }
                                }
                              ]
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DailyInStockRevenueShareQuery().Value).TextValue
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
                      "name": "placed_on",
                      "type": "date"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "in_stock_revenue",
                      "type": "double"
                    },
                    {
                      "name": "out_of_stock_revenue",
                      "type": "double"
                    },
                    {
                      "name": "in_stock_pct",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "39.97",
                      "19.98",
                      "19.99",
                      "50"
                    ],
                    [
                      "2024-06-03",
                      "22.5",
                      "22.5",
                      "0",
                      "100"
                    ],
                    [
                      "2024-06-05",
                      "29.97",
                      "29.97",
                      "0",
                      "100"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DailyInStockRevenueShareQuery().Result).TextValue
        );
    }
}
