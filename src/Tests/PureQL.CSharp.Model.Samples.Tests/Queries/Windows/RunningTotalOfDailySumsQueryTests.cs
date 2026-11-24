using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RunningTotalOfDailySumsQueryTests
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
                      "name": "daily",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "day",
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
                              "key": "day",
                              "type": {
                                "name": "date"
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
                    }
                  ],
                  "from": {
                    "subquery": "daily"
                  },
                  "select": [
                    {
                      "alias": "day",
                      "expression": {
                        "source": "daily",
                        "field": "day",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "source": "daily",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "source": "daily",
                        "field": "orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "cumulative_revenue",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "daily",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "daily",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "revenue_week_to_date",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "daily",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "startOfWeek",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "daily",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "daily",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_of_3_days",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "daily",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "daily",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "frame": {
                            "preceding": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "previous_day_revenue",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "daily",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "daily",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "change",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "daily",
                            "field": "revenue",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "previous",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "operand": {
                              "source": "daily",
                              "field": "revenue",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "over": {
                              "orderBy": [
                                {
                                  "expression": {
                                    "source": "daily",
                                    "field": "day",
                                    "type": {
                                      "name": "date"
                                    }
                                  }
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "share_of_week",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "daily",
                            "field": "revenue",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "daily",
                                "field": "revenue",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "operator": "startOfWeek",
                                    "type": {
                                      "name": "date"
                                    },
                                    "operand": {
                                      "source": "daily",
                                      "field": "day",
                                      "type": {
                                        "name": "date"
                                      }
                                    }
                                  }
                                ]
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "rank_in_week",
                      "expression": {
                        "operator": "rank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "startOfWeek",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "daily",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "daily",
                                "field": "revenue",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "daily",
                        "field": "day",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RunningTotalOfDailySumsQuery().Value).TextValue
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
                      "name": "day",
                      "type": "date"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "cumulative_revenue",
                      "type": "double"
                    },
                    {
                      "name": "revenue_week_to_date",
                      "type": "double"
                    },
                    {
                      "name": "average_of_3_days",
                      "type": "double"
                    },
                    {
                      "name": "previous_day_revenue",
                      "type": "double"
                    },
                    {
                      "name": "change",
                      "type": "double"
                    },
                    {
                      "name": "share_of_week",
                      "type": "double"
                    },
                    {
                      "name": "rank_in_week",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "100.5",
                      "1",
                      "100.5",
                      "100.5",
                      "100.5",
                      "",
                      "",
                      "0.6677740863787376",
                      "1"
                    ],
                    [
                      "2024-06-02",
                      "50",
                      "1",
                      "150.5",
                      "150.5",
                      "75.25",
                      "100.5",
                      "-50.5",
                      "0.33222591362126247",
                      "2"
                    ],
                    [
                      "2024-06-03",
                      "200",
                      "1",
                      "350.5",
                      "200",
                      "116.83333333333333",
                      "50",
                      "150",
                      "0.2959674435812061",
                      "2"
                    ],
                    [
                      "2024-06-04",
                      "75.25",
                      "1",
                      "425.75",
                      "275.25",
                      "108.41666666666667",
                      "200",
                      "-124.75",
                      "0.11135775064742878",
                      "4"
                    ],
                    [
                      "2024-06-05",
                      "300",
                      "1",
                      "725.75",
                      "575.25",
                      "191.75",
                      "75.25",
                      "224.75",
                      "0.4439511653718091",
                      "1"
                    ],
                    [
                      "2024-06-06",
                      "100.5",
                      "1",
                      "826.25",
                      "675.75",
                      "158.58333333333334",
                      "300",
                      "-199.5",
                      "0.14872364039955605",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RunningTotalOfDailySumsQuery().Result).TextValue
        );
    }
}
