using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalMonthsInGroupedAndTotalQueriesQueryTests
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
                      "name": "recent",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "ordered_week",
                            "expression": {
                              "operator": "startOfWeek",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
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
                            "key": "ordered_week",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "operator": "datetimeAddMonths",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-12-01T12:00:00Z"
                            },
                            "months": {
                              "type": {
                                "name": "integer"
                              },
                              "value": -6
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
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
                            "alias": "a_month_after_the_key",
                            "expression": {
                              "operator": "datetimeAddMonths",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "key": "ordered_week",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "months": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              }
                            }
                          },
                          {
                            "alias": "months_since_the_key",
                            "expression": {
                              "operator": "datetimeDiffMonths",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2024-12-01T12:00:00Z"
                              },
                              "right": {
                                "key": "ordered_week",
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
                            "alias": "months_between_first_and_last_shipment",
                            "expression": {
                              "operator": "datetimeDiffMonths",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "operator": "max",
                                "type": {
                                  "name": "datetime"
                                },
                                "selector": {
                                  "operator": "datetimeAddMonths",
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
                                  "months": {
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
                                            "name": "integer"
                                          },
                                          "value": 100
                                        }
                                      ]
                                    }
                                  }
                                }
                              },
                              "right": {
                                "operator": "min",
                                "type": {
                                  "name": "datetime"
                                },
                                "selector": {
                                  "operator": "datetimeAddMonths",
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
                                  "months": {
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
                                            "name": "integer"
                                          },
                                          "value": 100
                                        }
                                      ]
                                    }
                                  }
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              }
                            }
                          },
                          {
                            "alias": "orders_in_the_last_six_local_months",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              },
                              "predicate": {
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
                                  "operator": "datetimeAddMonths",
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "operand": {
                                    "type": {
                                      "name": "datetime"
                                    },
                                    "value": "2024-12-01T12:00:00Z"
                                  },
                                  "months": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": -6
                                  },
                                  "zone": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "America/New_York"
                                  }
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "year",
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
                            "operator": "datetimeAddMonths",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2024-12-01T12:00:00Z"
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              }
                            },
                            "months": {
                              "type": {
                                "name": "integer"
                              },
                              "value": -12
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            }
                          }
                        },
                        "aggregate": [
                          {
                            "alias": "orders_of_twelve_local_months",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "months_covered",
                            "expression": {
                              "operator": "datetimeDiffMonths",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "left": {
                                "operator": "max",
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
                              "right": {
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
                                "value": "America/New_York"
                              }
                            }
                          },
                          {
                            "alias": "a_month_after_the_last_order",
                            "expression": {
                              "operator": "datetimeAddMonths",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "operand": {
                                "operator": "max",
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
                              "months": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              }
                            }
                          },
                          {
                            "alias": "most_common_age_in_months",
                            "expression": {
                              "operator": "mode",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "selector": {
                                "operator": "datetimeDiffMonths",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "value": "2024-12-01T12:00:00Z"
                                },
                                "right": {
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
                                  "value": "America/New_York"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "recent"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "year",
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
                        "source": "recent",
                        "field": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "months_since",
                      "expression": {
                        "source": "recent",
                        "field": "months_since_the_key",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "orders_of_twelve_local_months",
                      "expression": {
                        "source": "year",
                        "field": "orders_of_twelve_local_months",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalMonthsInGroupedAndTotalQueriesQuery().Value).TextValue
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
                      "name": "months_since",
                      "type": "long"
                    },
                    {
                      "name": "orders_of_twelve_local_months",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "5",
                      "6"
                    ],
                    [
                      "pending",
                      "5",
                      "6"
                    ],
                    [
                      "shipped",
                      "5",
                      "6"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new LocalMonthsInGroupedAndTotalQueriesQuery().Result
            ).TextValue
        );
    }
}
