using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record StatisticsPerStatusQueryTests
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
                  "joins": [
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
                  "groupBy": [
                    {
                      "alias": "order_status",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "order_status",
                      "expression": {
                        "key": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "median_total",
                      "expression": {
                        "operator": "median",
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
                      "alias": "ninth_decile_total",
                      "expression": {
                        "operator": "percentileContinuous",
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
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.9"
                        }
                      }
                    },
                    {
                      "alias": "largest_total_of_the_lower_half",
                      "expression": {
                        "operator": "percentileDiscrete",
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
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    },
                    {
                      "alias": "usual_shift_start",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "time"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "median_placed_on",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "median_active_user_total",
                      "expression": {
                        "operator": "median",
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
                        "predicate": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "spread",
                      "expression": {
                        "operator": "standardDeviationPopulation",
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
                      "alias": "spread_estimate",
                      "expression": {
                        "operator": "standardDeviationSample",
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
                        }
                      }
                    },
                    {
                      "alias": "variance",
                      "expression": {
                        "operator": "variancePopulation",
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
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "median",
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
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new StatisticsPerStatusQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "median_total",
                      "type": "double"
                    },
                    {
                      "name": "ninth_decile_total",
                      "type": "double"
                    },
                    {
                      "name": "largest_total_of_the_lower_half",
                      "type": "double"
                    },
                    {
                      "name": "usual_shift_start",
                      "type": "time"
                    },
                    {
                      "name": "median_placed_on",
                      "type": "date"
                    },
                    {
                      "name": "median_active_user_total",
                      "type": "double"
                    },
                    {
                      "name": "spread",
                      "type": "double"
                    },
                    {
                      "name": "spread_estimate",
                      "type": "double"
                    },
                    {
                      "name": "variance",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped",
                      "200",
                      "280",
                      "200",
                      "09:00:00",
                      "2024-06-03",
                      "200.25",
                      "81.44561921238544",
                      "99.75010442768135",
                      "6633.388888888889"
                    ],
                    [
                      "cancelled",
                      "75.25",
                      "75.25",
                      "75.25",
                      "09:00:00",
                      "2024-06-04",
                      "75.25",
                      "0",
                      "",
                      "0"
                    ],
                    [
                      "pending",
                      "75.25",
                      "95.45",
                      "50",
                      "09:00:00",
                      "2024-06-04",
                      "75.25",
                      "25.25",
                      "35.70889244992065",
                      "637.5625"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new StatisticsPerStatusQuery().Result).TextValue
        );
    }
}
