using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record DatePartsQueryTests
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
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
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
                          },
                          {
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
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "dayOfWeek",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "signup_date",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 6
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
                      "alias": "signup_year",
                      "expression": {
                        "operator": "year",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signup_quarter",
                      "expression": {
                        "operator": "quarter",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signup_month",
                      "expression": {
                        "operator": "month",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signup_day",
                      "expression": {
                        "operator": "day",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signup_week",
                      "expression": {
                        "operator": "week",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signup_week_year",
                      "expression": {
                        "operator": "weekYear",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "edge_year",
                      "expression": {
                        "operator": "year",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_edge_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "edge_week",
                      "expression": {
                        "operator": "week",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_edge_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "edge_week_year",
                      "expression": {
                        "operator": "weekYear",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_edge_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_month",
                      "expression": {
                        "operator": "month",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "last_login_hour_utc",
                      "expression": {
                        "operator": "hour",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "shift_start_minute",
                      "expression": {
                        "operator": "minute",
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
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "dayOfYear",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DatePartsQuery().Value).TextValue
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
                      "name": "signup_year",
                      "type": "long"
                    },
                    {
                      "name": "signup_quarter",
                      "type": "long"
                    },
                    {
                      "name": "signup_month",
                      "type": "long"
                    },
                    {
                      "name": "signup_day",
                      "type": "long"
                    },
                    {
                      "name": "signup_week",
                      "type": "long"
                    },
                    {
                      "name": "signup_week_year",
                      "type": "long"
                    },
                    {
                      "name": "edge_year",
                      "type": "long"
                    },
                    {
                      "name": "edge_week",
                      "type": "long"
                    },
                    {
                      "name": "edge_week_year",
                      "type": "long"
                    },
                    {
                      "name": "shipped_month",
                      "type": "long"
                    },
                    {
                      "name": "last_login_hour_utc",
                      "type": "long"
                    },
                    {
                      "name": "shift_start_minute",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "2021",
                      "1",
                      "3",
                      "20",
                      "11",
                      "2021",
                      "2024",
                      "1",
                      "2025",
                      "6",
                      "9",
                      "0"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2022",
                      "4",
                      "11",
                      "5",
                      "44",
                      "2022",
                      "2024",
                      "44",
                      "2024",
                      "",
                      "18",
                      "30"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DatePartsQuery().Result).TextValue
        );
    }
}
