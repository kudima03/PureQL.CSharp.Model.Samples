using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record StatisticsForEveryTypeQueryTests
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
                  "aggregate": [
                    {
                      "alias": "median_decimal",
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
                        }
                      }
                    },
                    {
                      "alias": "median_date",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "date",
                          "nullable": true
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
                      "alias": "median_time",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "time",
                          "nullable": true
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
                      "alias": "median_datetime",
                      "expression": {
                        "operator": "median",
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
                      }
                    },
                    {
                      "alias": "percentile_continuous_decimal",
                      "expression": {
                        "operator": "percentileContinuous",
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
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "percentile_continuous_date",
                      "expression": {
                        "operator": "percentileContinuous",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "percentile_continuous_time",
                      "expression": {
                        "operator": "percentileContinuous",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "percentile_continuous_datetime",
                      "expression": {
                        "operator": "percentileContinuous",
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
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_decimal",
                      "expression": {
                        "operator": "percentileDiscrete",
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
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_string",
                      "expression": {
                        "operator": "percentileDiscrete",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_boolean",
                      "expression": {
                        "operator": "percentileDiscrete",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_date",
                      "expression": {
                        "operator": "percentileDiscrete",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_time",
                      "expression": {
                        "operator": "percentileDiscrete",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_datetime",
                      "expression": {
                        "operator": "percentileDiscrete",
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
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "percentile_discrete_uuid",
                      "expression": {
                        "operator": "percentileDiscrete",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "mode_decimal",
                      "expression": {
                        "operator": "mode",
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
                      "alias": "mode_string",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "mode_boolean",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "mode_date",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "date",
                          "nullable": true
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
                      "alias": "mode_time",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "time",
                          "nullable": true
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
                      "alias": "mode_datetime",
                      "expression": {
                        "operator": "mode",
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
                      }
                    },
                    {
                      "alias": "mode_uuid",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "median_of_a_nullable_date",
                      "expression": {
                        "operator": "median",
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
                              "operator": "ceiling",
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
                                    "value": "150"
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
                        }
                      }
                    },
                    {
                      "alias": "median_days_to_ship",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer",
                            "nullable": true
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
                                "operator": "ceiling",
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
                                      "value": "150"
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
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "usual_days_to_ship",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer",
                            "nullable": true
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
                                "operator": "ceiling",
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
                                      "value": "150"
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
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "smallest_total",
                      "expression": {
                        "operator": "percentileDiscrete",
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
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0"
                        }
                      }
                    },
                    {
                      "alias": "largest_total",
                      "expression": {
                        "operator": "percentileContinuous",
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
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1"
                        }
                      }
                    },
                    {
                      "alias": "variance_population",
                      "expression": {
                        "operator": "variancePopulation",
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
                      "alias": "variance_sample",
                      "expression": {
                        "operator": "varianceSample",
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
                      "alias": "standard_deviation_population",
                      "expression": {
                        "operator": "standardDeviationPopulation",
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
                      "alias": "standard_deviation_sample",
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new StatisticsForEveryTypeQuery().Value).TextValue
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
                      "name": "median_decimal",
                      "type": "double"
                    },
                    {
                      "name": "median_date",
                      "type": "date"
                    },
                    {
                      "name": "median_time",
                      "type": "time"
                    },
                    {
                      "name": "median_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "percentile_continuous_decimal",
                      "type": "double"
                    },
                    {
                      "name": "percentile_continuous_date",
                      "type": "date"
                    },
                    {
                      "name": "percentile_continuous_time",
                      "type": "time"
                    },
                    {
                      "name": "percentile_continuous_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "percentile_discrete_decimal",
                      "type": "double"
                    },
                    {
                      "name": "percentile_discrete_string",
                      "type": "string"
                    },
                    {
                      "name": "percentile_discrete_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "percentile_discrete_date",
                      "type": "date"
                    },
                    {
                      "name": "percentile_discrete_time",
                      "type": "time"
                    },
                    {
                      "name": "percentile_discrete_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "percentile_discrete_uuid",
                      "type": "uuid"
                    },
                    {
                      "name": "mode_decimal",
                      "type": "double"
                    },
                    {
                      "name": "mode_string",
                      "type": "string"
                    },
                    {
                      "name": "mode_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "mode_date",
                      "type": "date"
                    },
                    {
                      "name": "mode_time",
                      "type": "time"
                    },
                    {
                      "name": "mode_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "mode_uuid",
                      "type": "uuid"
                    },
                    {
                      "name": "median_of_a_nullable_date",
                      "type": "date"
                    },
                    {
                      "name": "median_days_to_ship",
                      "type": "double"
                    },
                    {
                      "name": "usual_days_to_ship",
                      "type": "long"
                    },
                    {
                      "name": "smallest_total",
                      "type": "double"
                    },
                    {
                      "name": "largest_total",
                      "type": "double"
                    },
                    {
                      "name": "variance_population",
                      "type": "double"
                    },
                    {
                      "name": "variance_sample",
                      "type": "double"
                    },
                    {
                      "name": "standard_deviation_population",
                      "type": "double"
                    },
                    {
                      "name": "standard_deviation_sample",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "100.5",
                      "2024-06-04",
                      "09:00:00",
                      "2024-06-04T00:30:00Z",
                      "81.5625",
                      "2024-06-02",
                      "09:00:00",
                      "2024-06-02T17:15:00Z",
                      "200",
                      "shipped",
                      "True",
                      "2024-06-05",
                      "10:00:00",
                      "2024-06-05T14:00:00Z",
                      "00000003-0000-0000-0000-000000000000",
                      "100.5",
                      "shipped",
                      "True",
                      "2024-06-01",
                      "09:00:00",
                      "2024-06-01T10:00:00Z",
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-05",
                      "2",
                      "2",
                      "50",
                      "300",
                      "7430.258680555556",
                      "8916.310416666667",
                      "86.19894825666701",
                      "94.42621678679426"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new StatisticsForEveryTypeQuery().Result).TextValue
        );
    }
}
