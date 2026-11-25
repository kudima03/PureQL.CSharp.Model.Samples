using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record AmountsOfEveryKindQueryTests
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
                  "select": [
                    {
                      "alias": "dateAddDays_by_a_literal",
                      "expression": {
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
                    },
                    {
                      "alias": "dateAddDays_by_a_negative",
                      "expression": {
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
                          "value": -3
                        }
                      }
                    },
                    {
                      "alias": "dateAddDays_by_a_parameter",
                      "expression": {
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
                          "value": 7
                        }
                      }
                    },
                    {
                      "alias": "dateAddDays_by_a_nullable_parameter",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "days": {
                          "operator": "round",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "dateAddDays_by_a_computed_amount",
                      "expression": {
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
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "dateAddMonths_by_a_literal",
                      "expression": {
                        "operator": "dateAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        }
                      }
                    },
                    {
                      "alias": "dateAddMonths_by_a_negative",
                      "expression": {
                        "operator": "dateAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -3
                        }
                      }
                    },
                    {
                      "alias": "dateAddMonths_by_a_parameter",
                      "expression": {
                        "operator": "dateAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 5
                        }
                      }
                    },
                    {
                      "alias": "dateAddMonths_by_a_nullable_parameter",
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "months": {
                          "operator": "round",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "dateAddMonths_by_a_computed_amount",
                      "expression": {
                        "operator": "dateAddMonths",
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
                        "months": {
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddMonths_by_a_literal",
                      "expression": {
                        "operator": "datetimeAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddMonths_by_a_negative",
                      "expression": {
                        "operator": "datetimeAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -3
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddMonths_by_a_parameter",
                      "expression": {
                        "operator": "datetimeAddMonths",
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
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 5
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddMonths_by_a_nullable_parameter",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "months": {
                          "operator": "round",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddMonths_by_a_computed_amount",
                      "expression": {
                        "operator": "datetimeAddMonths",
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
                        "months": {
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "timeAddSeconds_by_a_literal",
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
                            "name": "decimal"
                          },
                          "value": "1.5"
                        }
                      }
                    },
                    {
                      "alias": "timeAddSeconds_by_a_negative",
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
                          "value": -3
                        }
                      }
                    },
                    {
                      "alias": "timeAddSeconds_by_a_parameter",
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
                            "name": "decimal"
                          },
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "timeAddSeconds_by_a_nullable_parameter",
                      "expression": {
                        "operator": "timeAddSeconds",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "seconds": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "timeAddSeconds_by_a_computed_amount",
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
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddSeconds_by_a_literal",
                      "expression": {
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
                          "value": "1.5"
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddSeconds_by_a_negative",
                      "expression": {
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
                            "name": "integer"
                          },
                          "value": -3
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddSeconds_by_a_parameter",
                      "expression": {
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
                          "value": "0.25"
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddSeconds_by_a_nullable_parameter",
                      "expression": {
                        "operator": "datetimeAddSeconds",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "seconds": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "datetimeAddSeconds_by_a_computed_amount",
                      "expression": {
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
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AmountsOfEveryKindQuery().Value).TextValue
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
                      "name": "dateAddDays_by_a_literal",
                      "type": "date"
                    },
                    {
                      "name": "dateAddDays_by_a_negative",
                      "type": "date"
                    },
                    {
                      "name": "dateAddDays_by_a_parameter",
                      "type": "date"
                    },
                    {
                      "name": "dateAddDays_by_a_nullable_parameter",
                      "type": "date"
                    },
                    {
                      "name": "dateAddDays_by_a_computed_amount",
                      "type": "date"
                    },
                    {
                      "name": "dateAddMonths_by_a_literal",
                      "type": "date"
                    },
                    {
                      "name": "dateAddMonths_by_a_negative",
                      "type": "date"
                    },
                    {
                      "name": "dateAddMonths_by_a_parameter",
                      "type": "date"
                    },
                    {
                      "name": "dateAddMonths_by_a_nullable_parameter",
                      "type": "date"
                    },
                    {
                      "name": "dateAddMonths_by_a_computed_amount",
                      "type": "date"
                    },
                    {
                      "name": "datetimeAddMonths_by_a_literal",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddMonths_by_a_negative",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddMonths_by_a_parameter",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddMonths_by_a_nullable_parameter",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddMonths_by_a_computed_amount",
                      "type": "datetime"
                    },
                    {
                      "name": "timeAddSeconds_by_a_literal",
                      "type": "time"
                    },
                    {
                      "name": "timeAddSeconds_by_a_negative",
                      "type": "time"
                    },
                    {
                      "name": "timeAddSeconds_by_a_parameter",
                      "type": "time"
                    },
                    {
                      "name": "timeAddSeconds_by_a_nullable_parameter",
                      "type": "time"
                    },
                    {
                      "name": "timeAddSeconds_by_a_computed_amount",
                      "type": "time"
                    },
                    {
                      "name": "datetimeAddSeconds_by_a_literal",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddSeconds_by_a_negative",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddSeconds_by_a_parameter",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddSeconds_by_a_nullable_parameter",
                      "type": "datetime"
                    },
                    {
                      "name": "datetimeAddSeconds_by_a_computed_amount",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-04",
                      "2024-05-29",
                      "2024-06-08",
                      "2024-07-01",
                      "2024-12-20",
                      "2024-09-01",
                      "2024-03-01",
                      "2024-11-01",
                      "2026-12-01",
                      "2041-04-01",
                      "2024-09-01T10:00:00Z",
                      "2024-03-01T10:00:00Z",
                      "2024-11-01T10:00:00Z",
                      "2026-12-01T10:00:00Z",
                      "2041-04-01T10:00:00Z",
                      "09:00:01.5",
                      "08:59:57",
                      "09:00:00.25",
                      "09:00:30",
                      "09:03:22",
                      "2024-06-01T10:00:01.5Z",
                      "2024-06-01T09:59:57Z",
                      "2024-06-01T10:00:00.25Z",
                      "2024-06-01T10:00:30Z",
                      "2024-06-01T10:03:22Z"
                    ],
                    [
                      "2024-06-05",
                      "2024-05-30",
                      "2024-06-09",
                      "2024-07-02",
                      "2024-09-10",
                      "2024-09-02",
                      "2024-03-02",
                      "2024-11-02",
                      "2026-12-02",
                      "2032-10-02",
                      "2024-09-02T11:00:00Z",
                      "2024-03-02T11:00:00Z",
                      "2024-11-02T11:00:00Z",
                      "2026-12-02T11:00:00Z",
                      "2032-10-02T11:00:00Z",
                      "09:00:01.5",
                      "08:59:57",
                      "09:00:00.25",
                      "09:00:30",
                      "09:01:40",
                      "2024-06-02T11:00:01.5Z",
                      "2024-06-02T10:59:57Z",
                      "2024-06-02T11:00:00.25Z",
                      "2024-06-02T11:00:30Z",
                      "2024-06-02T11:01:40Z"
                    ],
                    [
                      "2024-06-06",
                      "2024-05-31",
                      "2024-06-10",
                      "",
                      "2025-07-08",
                      "2024-09-03",
                      "2024-03-03",
                      "2024-11-03",
                      "",
                      "2057-10-03",
                      "2024-09-03T12:00:00Z",
                      "2024-03-03T12:00:00Z",
                      "2024-11-03T12:00:00Z",
                      "",
                      "2057-10-03T12:00:00Z",
                      "10:00:01.5",
                      "09:59:57",
                      "10:00:00.25",
                      "",
                      "10:06:40",
                      "2024-06-03T12:00:01.5Z",
                      "2024-06-03T11:59:57Z",
                      "2024-06-03T12:00:00.25Z",
                      "",
                      "2024-06-03T12:06:40Z"
                    ],
                    [
                      "2024-06-07",
                      "2024-06-01",
                      "2024-06-11",
                      "2024-07-04",
                      "2024-11-01",
                      "2024-09-04",
                      "2024-03-04",
                      "2024-11-04",
                      "2026-12-04",
                      "2036-12-04",
                      "2024-09-04T13:00:00Z",
                      "2024-03-04T13:00:00Z",
                      "2024-11-04T13:00:00Z",
                      "2026-12-04T13:00:00Z",
                      "2036-12-04T13:00:00Z",
                      "09:00:01.5",
                      "08:59:57",
                      "09:00:00.25",
                      "09:00:30",
                      "09:02:30",
                      "2024-06-04T13:00:01.5Z",
                      "2024-06-04T12:59:57Z",
                      "2024-06-04T13:00:00.25Z",
                      "2024-06-04T13:00:30Z",
                      "2024-06-04T13:02:30Z"
                    ],
                    [
                      "2024-06-08",
                      "2024-06-02",
                      "2024-06-12",
                      "2024-07-05",
                      "2026-01-26",
                      "2024-09-05",
                      "2024-03-05",
                      "2024-11-05",
                      "2026-12-05",
                      "2074-06-05",
                      "2024-09-05T14:00:00Z",
                      "2024-03-05T14:00:00Z",
                      "2024-11-05T14:00:00Z",
                      "2026-12-05T14:00:00Z",
                      "2074-06-05T14:00:00Z",
                      "09:00:01.5",
                      "08:59:57",
                      "09:00:00.25",
                      "09:00:30",
                      "09:10:00",
                      "2024-06-05T14:00:01.5Z",
                      "2024-06-05T13:59:57Z",
                      "2024-06-05T14:00:00.25Z",
                      "2024-06-05T14:00:30Z",
                      "2024-06-05T14:10:00Z"
                    ],
                    [
                      "2024-06-09",
                      "2024-06-03",
                      "2024-06-13",
                      "",
                      "2024-12-25",
                      "2024-09-06",
                      "2024-03-06",
                      "2024-11-06",
                      "",
                      "2041-04-06",
                      "2024-09-06T15:00:00Z",
                      "2024-03-06T15:00:00Z",
                      "2024-11-06T15:00:00Z",
                      "",
                      "2041-04-06T15:00:00Z",
                      "11:30:01.5",
                      "11:29:57",
                      "11:30:00.25",
                      "",
                      "11:33:22",
                      "2024-06-06T15:00:01.5Z",
                      "2024-06-06T14:59:57Z",
                      "2024-06-06T15:00:00.25Z",
                      "",
                      "2024-06-06T15:03:22Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AmountsOfEveryKindQuery().Result).TextValue
        );
    }
}
