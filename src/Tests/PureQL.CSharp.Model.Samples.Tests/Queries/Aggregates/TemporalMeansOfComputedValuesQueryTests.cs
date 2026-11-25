using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record TemporalMeansOfComputedValuesQueryTests
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
                      "alias": "signup_month",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "signup_month",
                      "expression": {
                        "key": "signup_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "average_day_placed",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
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
                      "alias": "median_time_of_day",
                      "expression": {
                        "operator": "median",
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
                      "alias": "average_due_date",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
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
                            "type": {
                              "name": "integer"
                            },
                            "value": 30
                          }
                        }
                      }
                    },
                    {
                      "alias": "median_week",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "operator": "startOfWeek",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
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
                      "alias": "average_arrival",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "operator": "datetimeAddSeconds",
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
                          "seconds": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "90"
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_is_early_in_month",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "day",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
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
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 4
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "signup_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TemporalMeansOfComputedValuesQuery().Value).TextValue
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
                      "name": "signup_month",
                      "type": "date"
                    },
                    {
                      "name": "average_day_placed",
                      "type": "date"
                    },
                    {
                      "name": "median_time_of_day",
                      "type": "time"
                    },
                    {
                      "name": "average_due_date",
                      "type": "date"
                    },
                    {
                      "name": "median_week",
                      "type": "date"
                    },
                    {
                      "name": "average_arrival",
                      "type": "datetime"
                    },
                    {
                      "name": "average_is_early_in_month",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2019-07-01",
                      "2024-06-05",
                      "13:30:00",
                      "2024-07-05",
                      "2024-06-03",
                      "2024-06-05T01:31:30Z",
                      "False"
                    ],
                    [
                      "2020-01-01",
                      "2024-06-02",
                      "10:30:00",
                      "2024-07-02",
                      "2024-05-27",
                      "2024-06-01T22:31:30Z",
                      "True"
                    ],
                    [
                      "2021-03-01",
                      "2024-06-03",
                      "12:00:00",
                      "2024-07-03",
                      "2024-06-03",
                      "2024-06-03T12:01:30Z",
                      "True"
                    ],
                    [
                      "2022-11-01",
                      "2024-06-06",
                      "15:00:00",
                      "2024-07-06",
                      "2024-06-03",
                      "2024-06-06T15:01:30Z",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TemporalMeansOfComputedValuesQuery().Result).TextValue
        );
    }
}
