using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record RegressionInAGroupedQueryTests
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
                      "alias": "item_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_tenant_id",
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
                      "operator": "coalesce",
                      "type": {
                        "name": "decimal"
                      },
                      "values": [
                        {
                          "operator": "regressionRSquared",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "y": {
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
                          "x": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "0.3"
                    }
                  },
                  "select": [
                    {
                      "alias": "item_tenant_id",
                      "expression": {
                        "key": "item_tenant_id",
                        "type": {
                          "name": "uuid"
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
                      "alias": "quantity_per_unit_of_price",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
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
                        "x": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "quantity_at_no_price",
                      "expression": {
                        "operator": "regressionIntercept",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
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
                        "x": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "fit",
                      "expression": {
                        "operator": "regressionRSquared",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
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
                        "x": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "slope_over_all_tenants",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
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
                        "x": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": "all"
                      }
                    },
                    {
                      "alias": "steeper_than_all",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "regressionSlope",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "y": {
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
                              "x": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          ]
                        },
                        "right": {
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "regressionSlope",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "y": {
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
                              "x": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "over": "all"
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
                      "alias": "of_computed_values",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
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
                        },
                        "x": {
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
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            },
                            {
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
                                "value": 2
                              }
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "fit",
                      "direction": "desc"
                    },
                    {
                      "column": "item_tenant_id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RegressionInAGroupedQuery().Value).TextValue
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
                      "name": "item_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "lines",
                      "type": "long"
                    },
                    {
                      "name": "quantity_per_unit_of_price",
                      "type": "double"
                    },
                    {
                      "name": "quantity_at_no_price",
                      "type": "double"
                    },
                    {
                      "name": "fit",
                      "type": "double"
                    },
                    {
                      "name": "slope_over_all_tenants",
                      "type": "double"
                    },
                    {
                      "name": "steeper_than_all",
                      "type": "bool"
                    },
                    {
                      "name": "of_computed_values",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "3",
                      "-0.23894820636175618",
                      "5.412978051784451",
                      "0.8126996110988499",
                      "-0.23871482224931695",
                      "False",
                      "1.2575"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegressionInAGroupedQuery().Result).TextValue
        );
    }
}
