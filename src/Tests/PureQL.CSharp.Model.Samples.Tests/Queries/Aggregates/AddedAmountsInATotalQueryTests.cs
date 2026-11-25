using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AddedAmountsInATotalQueryTests
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
                        "type": {
                          "name": "datetime"
                        },
                        "value": "2024-09-04T00:00:00Z"
                      },
                      "months": {
                        "type": {
                          "name": "integer"
                        },
                        "value": -3
                      }
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "latest_due",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "max",
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
                        },
                        "days": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
                        }
                      }
                    },
                    {
                      "alias": "earliest_window_end",
                      "expression": {
                        "operator": "timeAddSeconds",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
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
                        },
                        "seconds": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7200
                        }
                      }
                    },
                    {
                      "alias": "a_year_after_the_first",
                      "expression": {
                        "operator": "datetimeAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        }
                      }
                    },
                    {
                      "alias": "shifted_by_the_count",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-07-06"
                        },
                        "days": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_due",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date",
                          "nullable": true
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AddedAmountsInATotalQuery().Value).TextValue
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
                      "name": "latest_due",
                      "type": "date"
                    },
                    {
                      "name": "earliest_window_end",
                      "type": "time"
                    },
                    {
                      "name": "a_year_after_the_first",
                      "type": "datetime"
                    },
                    {
                      "name": "shifted_by_the_count",
                      "type": "date"
                    },
                    {
                      "name": "average_due",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-07-06",
                      "11:00:00",
                      "2025-06-04T13:00:00Z",
                      "2024-07-09",
                      "2024-07-05"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AddedAmountsInATotalQuery().Result).TextValue
        );
    }
}
