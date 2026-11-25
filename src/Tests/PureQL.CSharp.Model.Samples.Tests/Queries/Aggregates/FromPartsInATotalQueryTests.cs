using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record FromPartsInATotalQueryTests
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
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "operator": "dateFromParts",
                      "type": {
                        "name": "date"
                      },
                      "year": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 2024
                      },
                      "month": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 6
                      },
                      "day": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 3
                      }
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "year_end",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "year": {
                          "operator": "max",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "year",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          }
                        },
                        "month": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 31
                        }
                      }
                    },
                    {
                      "alias": "latest_start_hour",
                      "expression": {
                        "operator": "timeFromParts",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "hour": {
                          "operator": "max",
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
                              "source": "schema_with_foreign_keys.users",
                              "field": "shift_start",
                              "type": {
                                "name": "time"
                              }
                            }
                          }
                        },
                        "minute": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "second": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "first_window",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "operator": "min",
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
                        "time": {
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
                        }
                      }
                    },
                    {
                      "alias": "orders_this_year",
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "operator": "dateFromParts",
                            "type": {
                              "name": "date"
                            },
                            "year": {
                              "operator": "add",
                              "type": {
                                "name": "integer"
                              },
                              "values": [
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2023
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 1
                                }
                              ]
                            },
                            "month": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 6
                            },
                            "day": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "fixed_instant",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-01-31"
                        },
                        "time": {
                          "type": {
                            "name": "time"
                          },
                          "value": "18:30:00"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FromPartsInATotalQuery().Value).TextValue
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
                      "name": "year_end",
                      "type": "date"
                    },
                    {
                      "name": "latest_start_hour",
                      "type": "time"
                    },
                    {
                      "name": "first_window",
                      "type": "datetime"
                    },
                    {
                      "name": "orders_this_year",
                      "type": "long"
                    },
                    {
                      "name": "fixed_instant",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-12-31",
                      "11:00:00",
                      "2024-06-03T09:00:00Z",
                      "2",
                      "2024-01-31T18:30:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FromPartsInATotalQuery().Result).TextValue
        );
    }
}
