using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalPeriodStartsInGroupedAndTotalQueriesQueryTests
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
                      "name": "by_hour",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "ordered_at",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "alias": "status",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "having": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "key": "ordered_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "operator": "startOfMonth",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-07-01T09:00:00Z"
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Pacific/Pago_Pago"
                            }
                          }
                        },
                        "select": [
                          {
                            "alias": "status",
                            "expression": {
                              "key": "status",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          {
                            "alias": "local_hour_start_of_the_key",
                            "expression": {
                              "operator": "startOfHour",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "key": "ordered_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          },
                          {
                            "alias": "local_week_of_the_last_order",
                            "expression": {
                              "operator": "startOfWeek",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
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
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Europe/Moscow"
                              }
                            }
                          },
                          {
                            "alias": "local_days",
                            "expression": {
                              "operator": "countDistinct",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "startOfDay",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Pago_Pago"
                                }
                              }
                            }
                          },
                          {
                            "alias": "latest_local_month",
                            "expression": {
                              "operator": "max",
                              "type": {
                                "name": "datetime"
                              },
                              "selector": {
                                "operator": "startOfMonth",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Pago_Pago"
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "totals",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "operator": "startOfQuarter",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-07-01T09:00:00Z"
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Pacific/Pago_Pago"
                            }
                          }
                        },
                        "aggregate": [
                          {
                            "alias": "orders_this_local_quarter",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "local_days_with_orders",
                            "expression": {
                              "operator": "countDistinct",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "startOfDay",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Pago_Pago"
                                }
                              }
                            }
                          },
                          {
                            "alias": "local_month_of_the_first_order",
                            "expression": {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "operand": {
                                "operator": "min",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                },
                                "selector": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          },
                          {
                            "alias": "busiest_local_day",
                            "expression": {
                              "operator": "mode",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "selector": {
                                "operator": "startOfDay",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Pago_Pago"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "by_hour"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "totals",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "status",
                      "expression": {
                        "source": "by_hour",
                        "field": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "hour_start",
                      "expression": {
                        "source": "by_hour",
                        "field": "local_hour_start_of_the_key",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "orders_this_local_quarter",
                      "expression": {
                        "source": "totals",
                        "field": "orders_this_local_quarter",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new LocalPeriodStartsInGroupedAndTotalQueriesQuery().Value
            ).TextValue
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "hour_start",
                      "type": "datetime"
                    },
                    {
                      "name": "orders_this_local_quarter",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "2024-06-04T13:00:00Z",
                      "6"
                    ],
                    [
                      "pending",
                      "2024-06-02T11:00:00Z",
                      "6"
                    ],
                    [
                      "pending",
                      "2024-06-06T15:00:00Z",
                      "6"
                    ],
                    [
                      "shipped",
                      "2024-06-03T12:00:00Z",
                      "6"
                    ],
                    [
                      "shipped",
                      "2024-06-05T14:00:00Z",
                      "6"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new LocalPeriodStartsInGroupedAndTotalQueriesQuery().Result
            ).TextValue
        );
    }
}
