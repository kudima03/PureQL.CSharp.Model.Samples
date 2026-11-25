using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record TemporalMeansInATotalQueryTests
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
                  "where": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "70"
                        }
                      },
                      {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "250"
                        }
                      }
                    ]
                  },
                  "aggregate": [
                    {
                      "alias": "average_order_date",
                      "expression": {
                        "operator": "average",
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
                      "alias": "median_ordered_at",
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
                      "alias": "quartile_window_start",
                      "expression": {
                        "operator": "percentileContinuous",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "asTime",
                          "type": {
                            "name": "time"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
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
                      "alias": "median_shipped_date",
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "days_between_means",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "average",
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
                            }
                          }
                        },
                        "right": {
                          "operator": "average",
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
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TemporalMeansInATotalQuery().Value).TextValue
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
                      "name": "average_order_date",
                      "type": "date"
                    },
                    {
                      "name": "median_ordered_at",
                      "type": "datetime"
                    },
                    {
                      "name": "quartile_window_start",
                      "type": "time"
                    },
                    {
                      "name": "median_shipped_date",
                      "type": "date"
                    },
                    {
                      "name": "days_between_means",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-04",
                      "2024-06-04T00:30:00Z",
                      "11:30:00",
                      "2024-06-02",
                      "-2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TemporalMeansInATotalQuery().Result).TextValue
        );
    }
}
