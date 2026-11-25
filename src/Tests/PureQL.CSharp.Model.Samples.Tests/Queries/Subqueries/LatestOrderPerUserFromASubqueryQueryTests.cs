using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record LatestOrderPerUserFromASubqueryQueryTests
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
                      "name": "latest",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
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
                          },
                          {
                            "alias": "last_order_id",
                            "expression": {
                              "operator": "last",
                              "type": {
                                "name": "uuid"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
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
                              ]
                            }
                          },
                          {
                            "alias": "last_total",
                            "expression": {
                              "operator": "last",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
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
                              ]
                            }
                          },
                          {
                            "alias": "last_status",
                            "expression": {
                              "operator": "last",
                              "type": {
                                "name": "string"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              },
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
                              ]
                            }
                          },
                          {
                            "alias": "last_shipped_date",
                            "expression": {
                              "operator": "last",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "selector": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "condition": {
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
                                "then": {
                                  "operator": "dateAddDays",
                                  "type": {
                                    "name": "date"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_on",
                                    "type": {
                                      "name": "date"
                                    }
                                  },
                                  "days": {
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
                                          "value": "100"
                                        }
                                      ]
                                    }
                                  }
                                },
                                "else": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                }
                              },
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
                              ]
                            }
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
                      "subquery": "latest",
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
                          "source": "latest",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
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
                      "alias": "name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "source": "latest",
                            "field": "orders",
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
                      "alias": "last_order_id",
                      "expression": {
                        "source": "latest",
                        "field": "last_order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "last_total",
                      "expression": {
                        "source": "latest",
                        "field": "last_total",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "last_status",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "latest",
                            "field": "last_status",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "no orders"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "last_shipped_date",
                      "expression": {
                        "source": "latest",
                        "field": "last_shipped_date",
                        "type": {
                          "name": "date",
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
            new QueryJson(new LatestOrderPerUserFromASubqueryQuery().Value).TextValue
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
                      "name": "name",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "last_order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "last_total",
                      "type": "double"
                    },
                    {
                      "name": "last_status",
                      "type": "string"
                    },
                    {
                      "name": "last_shipped_date",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "2",
                      "00000066-0000-0000-0000-000000000000",
                      "50",
                      "pending",
                      ""
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Bob",
                      "1",
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      "shipped",
                      "2024-06-05"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara",
                      "2",
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      "shipped",
                      "2024-06-08"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "1",
                      "0000006a-0000-0000-0000-000000000000",
                      "100.5",
                      "pending",
                      ""
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "Eve",
                      "0",
                      "",
                      "",
                      "no orders",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay",
                      "0",
                      "",
                      "",
                      "no orders",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LatestOrderPerUserFromASubqueryQuery().Result).TextValue
        );
    }
}
