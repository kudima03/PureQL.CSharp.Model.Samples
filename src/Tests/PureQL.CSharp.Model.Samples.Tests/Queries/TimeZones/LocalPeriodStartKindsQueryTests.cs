using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalPeriodStartKindsQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "subqueries": [
                    {
                      "name": "members",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.users"
                        },
                        "select": [
                          {
                            "alias": "created_at",
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
                            "alias": "last_login_at",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "last_login",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                },
                                "value": null
                              }
                            }
                          },
                          {
                            "alias": "time_zone",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          {
                            "alias": "optional_zone",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Auckland"
                              },
                              "else": {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              }
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "alias": "preferred_call_time",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "shift_start",
                              "type": {
                                "name": "time"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "members"
                  },
                  "select": [
                    {
                      "alias": "start_of_year_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfYear",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "start_of_quarter_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfQuarter",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "start_of_month_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "start_of_week_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfWeek",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "start_of_day_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfDay",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "start_of_hour_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfHour",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "start_of_minute_in_a_literal_zone",
                      "expression": {
                        "operator": "startOfMinute",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "other_literal_zone",
                      "expression": {
                        "operator": "startOfDay",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Pacific/Auckland"
                        }
                      }
                    },
                    {
                      "alias": "column_zone",
                      "expression": {
                        "operator": "startOfWeek",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "computed_zone",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "operator": "coalesce",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "source": "members",
                              "field": "optional_zone",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            },
                            {
                              "source": "members",
                              "field": "time_zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "nullable_zone",
                      "expression": {
                        "operator": "startOfYear",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "optional_zone",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "null_zone",
                      "expression": {
                        "operator": "startOfHour",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "nullable_operand",
                      "expression": {
                        "operator": "startOfQuarter",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "members",
                          "field": "last_login_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "nullable_operand_and_zone",
                      "expression": {
                        "operator": "startOfMinute",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "members",
                          "field": "last_login_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "optional_zone",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "literal_operand",
                      "expression": {
                        "operator": "startOfHour",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-08-15T18:30:15.25Z"
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Asia/Kathmandu"
                        }
                      }
                    },
                    {
                      "alias": "second_needs_no_zone",
                      "expression": {
                        "operator": "startOfSecond",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "created_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_date",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "members",
                          "field": "birth_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_time",
                      "expression": {
                        "operator": "startOfHour",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "members",
                          "field": "preferred_call_time",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "local_day_of_the_start",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "operator": "startOfMonth",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "members",
                            "field": "created_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "nested",
                      "expression": {
                        "operator": "startOfDay",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "operator": "startOfMonth",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "members",
                            "field": "created_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalPeriodStartKindsQuery().Value).TextValue
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
                      "name": "start_of_year_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_quarter_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_month_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_week_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_day_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_hour_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_minute_in_a_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "other_literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "column_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "computed_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "nullable_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "null_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "datetime"
                    },
                    {
                      "name": "nullable_operand_and_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "literal_operand",
                      "type": "datetime"
                    },
                    {
                      "name": "second_needs_no_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "of_a_date",
                      "type": "date"
                    },
                    {
                      "name": "of_a_time",
                      "type": "time"
                    },
                    {
                      "name": "local_day_of_the_start",
                      "type": "date"
                    },
                    {
                      "name": "nested",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2018-12-31T21:00:00Z",
                      "2019-06-30T21:00:00Z",
                      "2019-06-30T21:00:00Z",
                      "2019-07-07T21:00:00Z",
                      "2019-07-09T21:00:00Z",
                      "2019-07-10T09:00:00Z",
                      "2019-07-10T09:00:00Z",
                      "2019-07-09T12:00:00Z",
                      "2019-07-08T04:00:00Z",
                      "2019-06-30T12:00:00Z",
                      "2018-12-31T11:00:00Z",
                      "",
                      "2024-04-01T04:00:00Z",
                      "2024-05-30T14:00:00Z",
                      "2024-08-15T18:15:00Z",
                      "2019-07-10T09:00:00Z",
                      "2019-07-01",
                      "09:00:00",
                      "2019-07-01",
                      "2019-07-01T04:00:00Z"
                    ],
                    [
                      "2019-12-31T21:00:00Z",
                      "2019-12-31T21:00:00Z",
                      "2019-12-31T21:00:00Z",
                      "2020-01-12T21:00:00Z",
                      "2020-01-14T21:00:00Z",
                      "2020-01-15T09:00:00Z",
                      "2020-01-15T09:00:00Z",
                      "2020-01-14T11:00:00Z",
                      "2020-01-12T15:00:00Z",
                      "2019-12-31T15:00:00Z",
                      "",
                      "",
                      "2024-03-31T15:00:00Z",
                      "",
                      "2024-08-15T18:15:00Z",
                      "2020-01-15T09:00:00Z",
                      "2020-01-01",
                      "09:00:00",
                      "2020-01-01",
                      "2019-12-31T15:00:00Z"
                    ],
                    [
                      "2019-12-31T21:00:00Z",
                      "2019-12-31T21:00:00Z",
                      "2019-12-31T21:00:00Z",
                      "2020-01-12T21:00:00Z",
                      "2020-01-14T21:00:00Z",
                      "2020-01-15T09:00:00Z",
                      "2020-01-15T09:00:00Z",
                      "2020-01-14T11:00:00Z",
                      "2020-01-13T05:00:00Z",
                      "2019-12-31T11:00:00Z",
                      "2019-12-31T11:00:00Z",
                      "",
                      "2024-04-01T04:00:00Z",
                      "2024-06-01T08:30:00Z",
                      "2024-08-15T18:15:00Z",
                      "2020-01-15T09:00:00Z",
                      "2020-01-01",
                      "09:00:00",
                      "2020-01-01",
                      "2020-01-01T05:00:00Z"
                    ],
                    [
                      "2020-12-31T21:00:00Z",
                      "2020-12-31T21:00:00Z",
                      "2021-02-28T21:00:00Z",
                      "2021-03-14T21:00:00Z",
                      "2021-03-19T21:00:00Z",
                      "2021-03-20T10:00:00Z",
                      "2021-03-20T10:00:00Z",
                      "2021-03-19T11:00:00Z",
                      "2021-03-15T04:00:00Z",
                      "2021-02-28T11:00:00Z",
                      "2020-12-31T11:00:00Z",
                      "",
                      "",
                      "",
                      "2024-08-15T18:15:00Z",
                      "2021-03-20T10:00:00Z",
                      "2021-03-01",
                      "10:00:00",
                      "2021-03-01",
                      "2021-03-01T05:00:00Z"
                    ],
                    [
                      "2021-12-31T21:00:00Z",
                      "2022-09-30T21:00:00Z",
                      "2022-10-31T21:00:00Z",
                      "2022-10-30T21:00:00Z",
                      "2022-11-04T21:00:00Z",
                      "2022-11-05T11:00:00Z",
                      "2022-11-05T11:30:00Z",
                      "2022-11-05T11:00:00Z",
                      "2022-10-30T15:00:00Z",
                      "2022-10-31T15:00:00Z",
                      "",
                      "",
                      "2024-03-31T15:00:00Z",
                      "",
                      "2024-08-15T18:15:00Z",
                      "2022-11-05T11:30:00Z",
                      "2022-11-01",
                      "11:00:00",
                      "2022-11-01",
                      "2022-10-31T15:00:00Z"
                    ],
                    [
                      "2022-12-31T21:00:00Z",
                      "2022-12-31T21:00:00Z",
                      "2023-01-31T21:00:00Z",
                      "2023-02-26T21:00:00Z",
                      "2023-02-27T21:00:00Z",
                      "2023-02-28T08:00:00Z",
                      "2023-02-28T08:00:00Z",
                      "2023-02-27T11:00:00Z",
                      "2023-02-26T15:00:00Z",
                      "2023-01-31T15:00:00Z",
                      "",
                      "",
                      "",
                      "",
                      "2024-08-15T18:15:00Z",
                      "2023-02-28T08:00:00Z",
                      "2023-02-01",
                      "08:00:00",
                      "2023-02-01",
                      "2023-01-31T15:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalPeriodStartKindsQuery().Result).TextValue
        );
    }
}
