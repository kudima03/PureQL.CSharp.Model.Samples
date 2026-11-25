using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record FirstAndLastPerUserQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "groupBy": [
                    {
                      "alias": "order_user_id",
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
                      "alias": "order_user_id",
                      "expression": {
                        "key": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "first_order_total",
                      "expression": {
                        "operator": "first",
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
                      "alias": "latest_order_id",
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
                      "alias": "largest_order_date",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
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
                        ]
                      }
                    },
                    {
                      "alias": "latest_shipped_on",
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
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
                    },
                    {
                      "alias": "first_shipped_on",
                      "expression": {
                        "operator": "first",
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
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
                        ],
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
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
                          "right": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "latest_pending_total",
                      "expression": {
                        "operator": "last",
                        "type": {
                          "name": "decimal",
                          "nullable": true
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
                        ],
                        "predicate": {
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
                            "value": "pending"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FirstAndLastPerUserQuery().Value).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "first_order_total",
                      "type": "double"
                    },
                    {
                      "name": "latest_order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "largest_order_date",
                      "type": "date"
                    },
                    {
                      "name": "latest_shipped_on",
                      "type": "date"
                    },
                    {
                      "name": "first_shipped_on",
                      "type": "date"
                    },
                    {
                      "name": "latest_pending_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "100.5",
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-06",
                      "",
                      "",
                      "100.5"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "75.25",
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      ""
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "200",
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      ""
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "100.5",
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "",
                      "2024-06-01",
                      "50"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FirstAndLastPerUserQuery().Result).TextValue
        );
    }
}
