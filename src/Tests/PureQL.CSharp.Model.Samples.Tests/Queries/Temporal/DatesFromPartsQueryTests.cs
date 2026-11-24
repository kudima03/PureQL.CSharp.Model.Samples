using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record DatesFromPartsQueryTests
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
                      "alias": "fiscal_year_start",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
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
                        },
                        "month": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 4
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "mid_month",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
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
                        },
                        "month": {
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
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 15
                        }
                      }
                    },
                    {
                      "alias": "fixed_month",
                      "expression": {
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
                          "value": 7
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "cut_off",
                      "expression": {
                        "operator": "timeFromParts",
                        "type": {
                          "name": "time"
                        },
                        "hour": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 18
                        },
                        "minute": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
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
                      "alias": "first_shift_at",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "time": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_at_noon",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        },
                        "time": {
                          "operator": "timeFromParts",
                          "type": {
                            "name": "time"
                          },
                          "hour": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 12
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DatesFromPartsQuery().Value).TextValue
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
                      "name": "fiscal_year_start",
                      "type": "date"
                    },
                    {
                      "name": "mid_month",
                      "type": "date"
                    },
                    {
                      "name": "fixed_month",
                      "type": "date"
                    },
                    {
                      "name": "cut_off",
                      "type": "time"
                    },
                    {
                      "name": "first_shift_at",
                      "type": "datetime"
                    },
                    {
                      "name": "shipped_at_noon",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2020-04-01",
                      "2020-01-15",
                      "2024-07-01",
                      "18:30:00",
                      "2020-01-15T09:00:00Z",
                      "2024-06-01T12:00:00Z"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "2021-04-01",
                      "2021-03-15",
                      "2024-07-01",
                      "18:30:00",
                      "2021-03-20T10:00:00Z",
                      "2024-06-03T12:00:00Z"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2019-04-01",
                      "2019-07-15",
                      "2024-07-01",
                      "18:30:00",
                      "2019-07-10T09:00:00Z",
                      "2024-06-05T12:00:00Z"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2022-04-01",
                      "2022-11-15",
                      "2024-07-01",
                      "18:30:00",
                      "2022-11-05T11:30:00Z",
                      ""
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "2023-04-01",
                      "2023-02-15",
                      "2024-07-01",
                      "18:30:00",
                      "2023-02-28T08:00:00Z",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "2020-04-01",
                      "2020-01-15",
                      "2024-07-01",
                      "18:30:00",
                      "2020-01-15T09:00:00Z",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DatesFromPartsQuery().Result).TextValue
        );
    }
}
