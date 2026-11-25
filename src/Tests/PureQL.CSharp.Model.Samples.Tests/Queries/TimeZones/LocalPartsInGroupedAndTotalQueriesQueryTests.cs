using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalPartsInGroupedAndTotalQueriesQueryTests
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
                      "name": "night_orders",
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
                          "operator": "lessThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "hour",
                            "type": {
                              "name": "integer"
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
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 6
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
                            "alias": "local_hour_of_the_key",
                            "expression": {
                              "operator": "hour",
                              "type": {
                                "name": "integer"
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
                            "alias": "local_month_of_the_key",
                            "expression": {
                              "operator": "month",
                              "type": {
                                "name": "integer"
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
                                "value": "Europe/Moscow"
                              }
                            }
                          },
                          {
                            "alias": "local_hour_of_the_last_order",
                            "expression": {
                              "operator": "hour",
                              "type": {
                                "name": "integer"
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
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          },
                          {
                            "alias": "weekdays_ordered_on",
                            "expression": {
                              "operator": "countDistinct",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "dayOfWeek",
                                "type": {
                                  "name": "integer"
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
                            "alias": "latest_local_hour",
                            "expression": {
                              "operator": "max",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "hour",
                                "type": {
                                  "name": "integer"
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
                        "aggregate": [
                          {
                            "alias": "orders_at_night",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              },
                              "predicate": {
                                "operator": "lessThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "operator": "hour",
                                  "type": {
                                    "name": "integer"
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
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 6
                                }
                              }
                            }
                          },
                          {
                            "alias": "local_year_of_the_first_order",
                            "expression": {
                              "operator": "year",
                              "type": {
                                "name": "integer",
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
                            "alias": "busiest_local_hour",
                            "expression": {
                              "operator": "mode",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "selector": {
                                "operator": "hour",
                                "type": {
                                  "name": "integer"
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
                            "alias": "weeks_with_orders",
                            "expression": {
                              "operator": "countDistinct",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "operator": "multiply",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "values": [
                                      {
                                        "operator": "weekYear",
                                        "type": {
                                          "name": "integer"
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
                                      },
                                      {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 100
                                      }
                                    ]
                                  },
                                  {
                                    "operator": "week",
                                    "type": {
                                      "name": "integer"
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
                                ]
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "night_orders"
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
                        "source": "night_orders",
                        "field": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "local_hour",
                      "expression": {
                        "source": "night_orders",
                        "field": "local_hour_of_the_key",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "orders_at_night",
                      "expression": {
                        "source": "totals",
                        "field": "orders_at_night",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalPartsInGroupedAndTotalQueriesQuery().Value).TextValue
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
                      "name": "local_hour",
                      "type": "long"
                    },
                    {
                      "name": "orders_at_night",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "2",
                      "5"
                    ],
                    [
                      "pending",
                      "0",
                      "5"
                    ],
                    [
                      "pending",
                      "4",
                      "5"
                    ],
                    [
                      "shipped",
                      "1",
                      "5"
                    ],
                    [
                      "shipped",
                      "3",
                      "5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new LocalPartsInGroupedAndTotalQueriesQuery().Result
            ).TextValue
        );
    }
}
