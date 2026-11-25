using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record RoundedAverageAndQuotientPerTenantQueryTests
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
                      "alias": "tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "price_band",
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
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
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
                  ],
                  "having": {
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
                            "operator": "subtract",
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
                                "value": 1
                              }
                            ]
                          }
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 1
                    }
                  },
                  "select": [
                    {
                      "alias": "tenant_id",
                      "expression": {
                        "key": "tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "price_band",
                      "expression": {
                        "key": "price_band",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "average_quantity",
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
                              "operator": "subtract",
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
                                  "value": 1
                                }
                              ]
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "total_floor",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
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
                                  "operator": "subtract",
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
                                      "value": 1
                                    }
                                  ]
                                }
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "units_per_line_rounded_up",
                      "expression": {
                        "operator": "ceiling",
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
                                  "operator": "subtract",
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
                                      "value": 1
                                    }
                                  ]
                                }
                              }
                            },
                            {
                              "operator": "count",
                              "type": {
                                "name": "integer"
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
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "subtract",
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
                                "value": 1
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "units_in_tens",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
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
                              "operator": "subtract",
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
                                  "value": 1
                                }
                              ]
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
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
                              "operator": "subtract",
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
                                  "value": 1
                                }
                              ]
                            }
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "price_band",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundedAverageAndQuotientPerTenantQuery().Value).TextValue
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
                      "name": "tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "price_band",
                      "type": "long"
                    },
                    {
                      "name": "average_quantity",
                      "type": "long"
                    },
                    {
                      "name": "total_floor",
                      "type": "long"
                    },
                    {
                      "name": "units_per_line_rounded_up",
                      "type": "long"
                    },
                    {
                      "name": "units",
                      "type": "long"
                    },
                    {
                      "name": "units_in_tens",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "0",
                      "2",
                      "19",
                      "2",
                      "2",
                      "0"
                    ],
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "0",
                      "3",
                      "27",
                      "3",
                      "5",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new RoundedAverageAndQuotientPerTenantQuery().Result
            ).TextValue
        );
    }
}
