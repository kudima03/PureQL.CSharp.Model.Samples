using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record CombinedQueriesInASubqueryChainQueryTests
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
                      "name": "highlights",
                      "query": {
                        "union": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "user_id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "total",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "direction": "desc"
                              },
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ],
                            "pagination": {
                              "take": 2
                            }
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "user_id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "total",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "direction": "desc"
                              },
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ],
                            "pagination": {
                              "take": 2
                            }
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "user_id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "total",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                }
                              },
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ],
                            "distinctBy": [
                              "user_id"
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "name": "highlighted_users",
                      "query": {
                        "from": {
                          "subquery": "highlights"
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "highlights",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "key": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "orders",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "quiet_users",
                      "query": {
                        "except": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "user_id",
                                "expression": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "subquery": "highlighted_users"
                            },
                            "select": [
                              {
                                "alias": "user_id",
                                "expression": {
                                  "source": "highlighted_users",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "highlighted_users",
                      "on": {
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
                          "source": "highlighted_users",
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
                    "type": {
                      "name": "boolean"
                    },
                    "condition": {
                      "operator": "in",
                      "type": {
                        "name": "boolean"
                      },
                      "operand": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "list": {
                        "subquery": "quiet_users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "highlighted_orders",
                      "expression": {
                        "source": "highlighted_users",
                        "field": "orders",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
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
                """
            ).TextValue,
            new QueryJson(new CombinedQueriesInASubqueryChainQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "highlighted_orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "1"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "1"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CombinedQueriesInASubqueryChainQuery().Result).TextValue
        );
    }
}
