using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record AmountsNamedByTheirUnitQueryTests
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
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "due_date",
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
                          "value": 30
                        }
                      }
                    },
                    {
                      "alias": "a_year_later",
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
                          "value": 12
                        }
                      }
                    },
                    {
                      "alias": "a_month_before",
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
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "reminder_at",
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
                              "value": 48
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3600
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "shift_end",
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
                          "value": 54000
                        }
                      }
                    },
                    {
                      "alias": "return_until",
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
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AmountsNamedByTheirUnitQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "due_date",
                      "type": "date"
                    },
                    {
                      "name": "a_year_later",
                      "type": "date"
                    },
                    {
                      "name": "a_month_before",
                      "type": "datetime"
                    },
                    {
                      "name": "reminder_at",
                      "type": "datetime"
                    },
                    {
                      "name": "shift_end",
                      "type": "time"
                    },
                    {
                      "name": "return_until",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-07-01",
                      "2025-06-01",
                      "2024-05-01T10:00:00Z",
                      "2024-06-03T10:00:00Z",
                      "00:00:00",
                      "2024-07-01"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-07-02",
                      "2025-06-02",
                      "2024-05-02T11:00:00Z",
                      "2024-06-04T11:00:00Z",
                      "00:00:00",
                      "2024-07-02"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-07-03",
                      "2025-06-03",
                      "2024-05-03T12:00:00Z",
                      "2024-06-05T12:00:00Z",
                      "01:00:00",
                      ""
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-07-04",
                      "2025-06-04",
                      "2024-05-04T13:00:00Z",
                      "2024-06-06T13:00:00Z",
                      "00:00:00",
                      "2024-07-04"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-07-05",
                      "2025-06-05",
                      "2024-05-05T14:00:00Z",
                      "2024-06-07T14:00:00Z",
                      "00:00:00",
                      "2024-07-05"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-07-06",
                      "2025-06-06",
                      "2024-05-06T15:00:00Z",
                      "2024-06-08T15:00:00Z",
                      "02:30:00",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AmountsNamedByTheirUnitQuery().Result).TextValue
        );
    }
}
