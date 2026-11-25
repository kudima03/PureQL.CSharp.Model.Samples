using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record AddedAmountsInAGroupedQueryTests
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
                      "alias": "due_month",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
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
                      "alias": "window_end",
                      "expression": {
                        "operator": "timeAddSeconds",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "seconds": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7200
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "lessThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "dateAddMonths",
                      "type": {
                        "name": "date"
                      },
                      "operand": {
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
                      "months": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 1
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-07-06"
                    }
                  },
                  "select": [
                    {
                      "alias": "due_month",
                      "expression": {
                        "key": "due_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "window_end",
                      "expression": {
                        "key": "window_end",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "last_due",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
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
                        "days": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
                        }
                      }
                    },
                    {
                      "alias": "by_as_many_days_as_orders",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "key": "due_month",
                          "type": {
                            "name": "date"
                          }
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
                      "alias": "first_reminder",
                      "expression": {
                        "operator": "datetimeAddSeconds",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "operator": "min",
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
                        },
                        "seconds": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    },
                    {
                      "alias": "next_month",
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "key": "due_month",
                          "type": {
                            "name": "date"
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "late",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
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
                                "operator": "integerDivide",
                                "type": {
                                  "name": "integer"
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
                                    "name": "integer"
                                  },
                                  "value": 50
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
                              "value": 3
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "key": "due_month",
                          "type": {
                            "name": "date"
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "window_end",
                        "type": {
                          "name": "time"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AddedAmountsInAGroupedQuery().Value).TextValue
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
                      "name": "due_month",
                      "type": "date"
                    },
                    {
                      "name": "window_end",
                      "type": "time"
                    },
                    {
                      "name": "last_due",
                      "type": "date"
                    },
                    {
                      "name": "by_as_many_days_as_orders",
                      "type": "date"
                    },
                    {
                      "name": "first_reminder",
                      "type": "datetime"
                    },
                    {
                      "name": "next_month",
                      "type": "date"
                    },
                    {
                      "name": "late",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-07-01",
                      "11:00:00",
                      "2024-07-05",
                      "2024-07-05",
                      "2024-06-01T10:00:00.5Z",
                      "2024-08-01",
                      "1"
                    ],
                    [
                      "2024-07-01",
                      "12:00:00",
                      "2024-07-03",
                      "2024-07-02",
                      "2024-06-03T12:00:00.5Z",
                      "2024-08-01",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AddedAmountsInAGroupedQuery().Result).TextValue
        );
    }
}
