using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record PriceDeviationPerProductQueryTests
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
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "product_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_product_id",
                        "type": {
                          "name": "uuid"
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
                            "operator": "average",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
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
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "40"
                          }
                        ]
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "0.5"
                    }
                  },
                  "select": [
                    {
                      "alias": "product_id",
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "average_gap",
                      "expression": {
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
                              "operator": "average",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "40"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "largest_gap",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
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
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
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
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "40"
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "net_units",
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
                            },
                            {
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
                                  "operator": "divide",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_total",
                                      "type": {
                                        "name": "decimal"
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "value": "50"
                                    }
                                  ]
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "against_plan",
                      "expression": {
                        "operator": "sign",
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
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "average_price_to_cents",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "operator": "divide",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
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
                      "alias": "whole_average_price",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "operator": "divide",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
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
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
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
                              "operator": "average",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "40"
                            }
                          ]
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PriceDeviationPerProductQuery().Value).TextValue
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
                      "name": "product_id",
                      "type": "uuid"
                    },
                    {
                      "name": "average_gap",
                      "type": "double"
                    },
                    {
                      "name": "largest_gap",
                      "type": "double"
                    },
                    {
                      "name": "net_units",
                      "type": "long"
                    },
                    {
                      "name": "against_plan",
                      "type": "long"
                    },
                    {
                      "name": "average_price_to_cents",
                      "type": "double"
                    },
                    {
                      "name": "whole_average_price",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000000ca-0000-0000-0000-000000000000",
                      "60.5",
                      "60.5",
                      "1",
                      "-1",
                      "100.5",
                      "100"
                    ],
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "35.125",
                      "60",
                      "3",
                      "0",
                      "75.12",
                      "75"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PriceDeviationPerProductQuery().Result).TextValue
        );
    }
}
