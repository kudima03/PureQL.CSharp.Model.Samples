using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record UnbilledOrderAgingQueryTests
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
                      "name": "item_value_by_order",
                      "query": {
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
                            "alias": "item_value",
                            "expression": {
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "item_value_by_order",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "item_value_by_order",
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
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "subtract",
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
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "item_value_by_order",
                              "field": "item_value",
                              "type": {
                                "name": "decimal",
                                "nullable": true
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
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "100"
                    }
                  },
                  "select": [
                    {
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "user_active",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_active",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "order_status",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "placed_on",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "order_total",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "unbilled",
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
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "operator": "coalesce",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "item_value_by_order",
                                  "field": "item_value",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
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
                      "alias": "days_open",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-10"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "aging_bucket",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "lessThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "dateDiffDays",
                            "type": {
                              "name": "integer"
                            },
                            "left": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-10"
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
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
                            "name": "string"
                          },
                          "value": "new"
                        },
                        "else": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "lessThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "dateDiffDays",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-10"
                              },
                              "right": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "1-4"
                          },
                          "else": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "lessThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "dateDiffDays",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-10"
                                },
                                "right": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 6
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "5-6"
                            },
                            "else": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "lessThanOrEqual",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "operator": "dateDiffDays",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "left": {
                                    "type": {
                                      "name": "date"
                                    },
                                    "value": "2024-06-10"
                                  },
                                  "right": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_on",
                                    "type": {
                                      "name": "date"
                                    }
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 8
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "7-8"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "9+"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "item_lines",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "source": "item_value_by_order",
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
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-10"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "operator": "subtract",
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
                            "operator": "coalesce",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "item_value_by_order",
                                "field": "item_value",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
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
                        ]
                      },
                      "direction": "desc"
                    }
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 100
                  }
                }
                """
            ).TextValue,
            new QueryJson(new UnbilledOrderAgingQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "placed_on",
                      "type": "date"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "unbilled",
                      "type": "double"
                    },
                    {
                      "name": "days_open",
                      "type": "long"
                    },
                    {
                      "name": "aging_bucket",
                      "type": "string"
                    },
                    {
                      "name": "item_lines",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Bob",
                      "False",
                      "shipped",
                      "2024-06-03",
                      "200",
                      "177.5",
                      "7",
                      "7-8",
                      "1"
                    ],
                    [
                      "Cara",
                      "True",
                      "shipped",
                      "2024-06-05",
                      "300",
                      "270.03",
                      "5",
                      "5-6",
                      "1"
                    ],
                    [
                      "Dan",
                      "True",
                      "pending",
                      "2024-06-06",
                      "100.5",
                      "100.5",
                      "4",
                      "1-4",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UnbilledOrderAgingQuery().Result).TextValue
        );
    }
}
