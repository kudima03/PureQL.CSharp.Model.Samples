using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record DailyRevenueTrendQueryTests
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
                      "name": "daily_revenue",
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
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-02"
                          }
                        },
                        "groupBy": [
                          {
                            "alias": "placed_on",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "day",
                            "expression": {
                              "operator": "dateDiffDays",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "key": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-02"
                              }
                            }
                          },
                          {
                            "alias": "revenue",
                            "expression": {
                              "operator": "sum",
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
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "daily_revenue"
                  },
                  "aggregate": [
                    {
                      "alias": "days",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "growth_per_day",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "daily_revenue",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "source": "daily_revenue",
                          "field": "day",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "revenue_on_the_first_day",
                      "expression": {
                        "operator": "regressionIntercept",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "daily_revenue",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "source": "daily_revenue",
                          "field": "day",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "share_of_variance_explained",
                      "expression": {
                        "operator": "regressionRSquared",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "daily_revenue",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "source": "daily_revenue",
                          "field": "day",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "forecast",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "regressionIntercept",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "y": {
                              "source": "daily_revenue",
                              "field": "revenue",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "x": {
                              "source": "daily_revenue",
                              "field": "day",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "values": [
                              {
                                "operator": "regressionSlope",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "y": {
                                  "source": "daily_revenue",
                                  "field": "revenue",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "x": {
                                  "source": "daily_revenue",
                                  "field": "day",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 7
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DailyRevenueTrendQuery().Value).TextValue
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
                      "name": "days",
                      "type": "long"
                    },
                    {
                      "name": "growth_per_day",
                      "type": "double"
                    },
                    {
                      "name": "revenue_on_the_first_day",
                      "type": "double"
                    },
                    {
                      "name": "share_of_variance_explained",
                      "type": "double"
                    },
                    {
                      "name": "forecast",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "5",
                      "20.1",
                      "104.95",
                      "0.0941305026537621",
                      "245.65"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DailyRevenueTrendQuery().Result).TextValue
        );
    }
}
