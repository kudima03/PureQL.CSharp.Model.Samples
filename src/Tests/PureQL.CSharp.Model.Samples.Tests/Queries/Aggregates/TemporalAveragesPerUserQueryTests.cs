using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record TemporalAveragesPerUserQueryTests
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
                      "alias": "average_placed_on",
                      "expression": {
                        "operator": "average",
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
                      "alias": "median_placed_at",
                      "expression": {
                        "operator": "median",
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
                      }
                    },
                    {
                      "alias": "average_time_of_day",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "time"
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
                        }
                      }
                    },
                    {
                      "alias": "ninth_decile_placed_at",
                      "expression": {
                        "operator": "percentileContinuous",
                        "type": {
                          "name": "datetime"
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
                          "value": "0.9"
                        }
                      }
                    },
                    {
                      "alias": "average_shipped_on",
                      "expression": {
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
                        }
                      }
                    },
                    {
                      "alias": "days_from_average_to_latest",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "max",
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
                        },
                        "right": {
                          "operator": "average",
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
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "average",
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
            new QueryJson(new TemporalAveragesPerUserQuery().Value).TextValue
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
                      "name": "average_placed_on",
                      "type": "date"
                    },
                    {
                      "name": "median_placed_on",
                      "type": "date"
                    },
                    {
                      "name": "median_placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "average_time_of_day",
                      "type": "time"
                    },
                    {
                      "name": "ninth_decile_placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "average_shipped_on",
                      "type": "date"
                    },
                    {
                      "name": "days_from_average_to_latest",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-01T22:30:00Z",
                      "10:30:00",
                      "2024-06-02T08:30:00Z",
                      "2024-06-01",
                      "0"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03T12:00:00Z",
                      "12:00:00",
                      "2024-06-03T12:00:00Z",
                      "2024-06-03",
                      "0"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05T01:30:00Z",
                      "13:30:00",
                      "2024-06-05T11:30:00Z",
                      "2024-06-05",
                      "0"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06T15:00:00Z",
                      "15:00:00",
                      "2024-06-06T15:00:00Z",
                      "",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TemporalAveragesPerUserQuery().Result).TextValue
        );
    }
}
