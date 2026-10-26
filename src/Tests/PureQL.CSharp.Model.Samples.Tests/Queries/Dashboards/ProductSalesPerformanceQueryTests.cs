using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record ProductSalesPerformanceQueryTests
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
                      "name": "sales_performance",
                      "query": {
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
                            "entity": "schema_with_foreign_keys.users",
                            "on": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        },
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
                            "alias": "item_lines",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "early_pct",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "decimal"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 0
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0"
                              },
                              "else": {
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
                                          "operator": "count",
                                          "type": {
                                            "name": "integer"
                                          },
                                          "predicate": {
                                            "operator": "lessThanOrEqual",
                                            "type": {
                                              "name": "boolean"
                                            },
                                            "left": {
                                              "source": "schema_with_foreign_keys.orders",
                                              "field": "placed_on",
                                              "type": {
                                                "name": "date"
                                              }
                                            },
                                            "right": {
                                              "type": {
                                                "name": "date"
                                              },
                                              "value": "2024-06-03"
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
                          },
                          {
                            "alias": "avg_days_after_last_login",
                            "expression": {
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
                                    "operator": "datetimeDiffSeconds",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_at",
                                      "type": {
                                        "name": "datetime"
                                      }
                                    },
                                    "right": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "last_login",
                                      "type": {
                                        "name": "datetime"
                                      }
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "86400"
                                  }
                                ]
                              }
                            }
                          },
                          {
                            "alias": "max_days_since_signup",
                            "expression": {
                              "operator": "max",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "dateDiffDays",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "right": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "signup_date",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              }
                            }
                          },
                          {
                            "alias": "order_total_sum",
                            "expression": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "alias": "units",
                            "expression": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "inactive_user_lines",
                      "query": {
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
                            "entity": "schema_with_foreign_keys.users",
                            "on": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "boolean"
                            },
                            "value": false
                          }
                        },
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
                            "alias": "item_lines",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.products"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "sales_performance",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "sales_performance",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "inactive_user_lines",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "inactive_user_lines",
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
                      "alias": "product_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.products",
                        "field": "product_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "product_in_stock",
                      "expression": {
                        "source": "schema_with_foreign_keys.products",
                        "field": "product_in_stock",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "item_lines",
                      "expression": {
                        "source": "sales_performance",
                        "field": "item_lines",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "early_pct",
                      "expression": {
                        "source": "sales_performance",
                        "field": "early_pct",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "avg_days_after_last_login",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "sales_performance",
                          "field": "avg_days_after_last_login",
                          "type": {
                            "name": "decimal"
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
                      "alias": "max_days_since_signup",
                      "expression": {
                        "source": "sales_performance",
                        "field": "max_days_since_signup",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "order_total_per_unit",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "sales_performance",
                              "field": "order_total_sum",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "source": "sales_performance",
                              "field": "units",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        }
                      }
                    },
                    {
                      "alias": "inactive_user_lines",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "source": "inactive_user_lines",
                            "field": "item_lines",
                            "type": {
                              "name": "integer",
                              "nullable": true
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
                    },
                    {
                      "alias": "inactive_per_100_lines",
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
                                  "operator": "coalesce",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "values": [
                                    {
                                      "source": "inactive_user_lines",
                                      "field": "item_lines",
                                      "type": {
                                        "name": "integer",
                                        "nullable": true
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
                                {
                                  "source": "sales_performance",
                                  "field": "item_lines",
                                  "type": {
                                    "name": "integer"
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
                          "value": 2
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "sales_performance",
                        "field": "early_pct",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.products",
                        "field": "product_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ProductSalesPerformanceQuery().Value).TextValue
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
                      "name": "product_name",
                      "type": "string"
                    },
                    {
                      "name": "product_in_stock",
                      "type": "bool"
                    },
                    {
                      "name": "item_lines",
                      "type": "long"
                    },
                    {
                      "name": "early_pct",
                      "type": "double"
                    },
                    {
                      "name": "avg_days_after_last_login",
                      "type": "double"
                    },
                    {
                      "name": "max_days_since_signup",
                      "type": "long"
                    },
                    {
                      "name": "order_total_per_unit",
                      "type": "double"
                    },
                    {
                      "name": "inactive_user_lines",
                      "type": "long"
                    },
                    {
                      "name": "inactive_per_100_lines",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Gadget",
                      "False",
                      "1",
                      "100",
                      "0.06",
                      "1599",
                      "100.5",
                      "0",
                      "0"
                    ],
                    [
                      "Gizmo",
                      "True",
                      "1",
                      "100",
                      "1.11",
                      "1171",
                      "40",
                      "1",
                      "100"
                    ],
                    [
                      "Widget",
                      "True",
                      "2",
                      "50",
                      "3.03",
                      "1792",
                      "80.1",
                      "0",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ProductSalesPerformanceQuery().Result).TextValue
        );
    }
}
