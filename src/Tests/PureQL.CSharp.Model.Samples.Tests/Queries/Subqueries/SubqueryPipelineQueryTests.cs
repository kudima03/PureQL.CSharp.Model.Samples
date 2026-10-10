using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record SubqueryPipelineQueryTests
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
                      "name": "shipped_lines",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.order_items"
                        },
                        "joins": [
                          {
                            "type": "inner",
                            "entity": "schema_with_foreign_keys.orders",
                            "on": {
                              "operator": "equal",
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
                            "type": "inner",
                            "entity": "schema_with_foreign_keys.products",
                            "on": {
                              "operator": "equal",
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
                        "select": [
                          {
                            "alias": "user_id",
                            "type": {
                              "name": "uuid"
                            },
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "line_total",
                            "type": {
                              "name": "decimal"
                            },
                            "expression": {
                              "operator": "multiply",
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
                        ]
                      }
                    },
                    {
                      "name": "user_revenue",
                      "query": {
                        "from": {
                          "subquery": "shipped_lines"
                        },
                        "groupBy": [
                          {
                            "type": {
                              "name": "uuid"
                            },
                            "expression": {
                              "source": "shipped_lines",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "having": {
                          "operator": "greaterThan",
                          "left": {
                            "operator": "sum",
                            "selector": {
                              "source": "shipped_lines",
                              "field": "line_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 20
                          }
                        },
                        "select": [
                          {
                            "alias": "user_id",
                            "type": {
                              "name": "uuid"
                            },
                            "expression": {
                              "key": 0,
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "revenue",
                            "type": {
                              "name": "decimal"
                            },
                            "expression": {
                              "operator": "sum",
                              "selector": {
                                "source": "shipped_lines",
                                "field": "line_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "alias": "lines",
                            "type": {
                              "name": "integer"
                            },
                            "expression": {
                              "operator": "count"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "dormant",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.users"
                        },
                        "where": {
                          "operator": "lessThan",
                          "left": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-05-31T00:00:00Z"
                          }
                        },
                        "select": [
                          {
                            "alias": "id",
                            "type": {
                              "name": "uuid"
                            },
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users",
                    "alias": "u"
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "subquery": "user_revenue",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "u",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "user_revenue",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "not",
                    "condition": {
                      "operator": "in",
                      "value": {
                        "source": "u",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "list": {
                        "subquery": "dormant",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "u",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "user_revenue",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "average_line",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "divide",
                        "values": [
                          {
                            "source": "user_revenue",
                            "field": "revenue",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "source": "user_revenue",
                            "field": "lines",
                            "type": {
                              "name": "integer"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "user_revenue",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "u",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 50
                  }
                }
                """
            ).TextValue,
            new QueryJson(new SubqueryPipelineQuery().Value).TextValue
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
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "average_line",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann",
                      "39.97",
                      "19.985"
                    ],
                    [
                      "Bob",
                      "22.5",
                      "22.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SubqueryPipelineQuery().Result).TextValue
        );
    }
}
