using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record AddMonthsQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
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
                      "source": "schema_with_foreign_keys.users",
                      "field": "signup_date",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "operator": "dateAddMonths",
                      "type": {
                        "name": "date"
                      },
                      "operand": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-06-30"
                      },
                      "months": {
                        "type": {
                          "name": "integer"
                        },
                        "value": -48
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "trial_ends_on",
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
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
                      "alias": "loyalty_until",
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
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
                              "type": {
                                "name": "integer"
                              },
                              "value": 12
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "payment_due",
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
                            "name": "date",
                            "nullable": true
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
                      "alias": "login_reminder_at",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AddMonthsQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "trial_ends_on",
                      "type": "date"
                    },
                    {
                      "name": "loyalty_until",
                      "type": "date"
                    },
                    {
                      "name": "payment_due",
                      "type": "date"
                    },
                    {
                      "name": "login_reminder_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "00000067-0000-0000-0000-000000000000",
                      "2021-04-20",
                      "2023-03-20",
                      "2024-07-03",
                      "2024-07-02T09:15:00Z"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "0000006a-0000-0000-0000-000000000000",
                      "2022-12-05",
                      "2024-11-05",
                      "2024-07-06",
                      "2024-07-03T18:45:00Z"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "",
                      "2023-03-28",
                      "2025-02-28",
                      "",
                      "2024-07-04T07:05:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AddMonthsQuery().Result).TextValue
        );
    }
}
