using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalPartKindsQueryTests
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
                      "alias": "year_in_a_literal_zone",
                      "expression": {
                        "operator": "year",
                        "type": {
                          "name": "integer"
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
                      "alias": "quarter_in_a_literal_zone",
                      "expression": {
                        "operator": "quarter",
                        "type": {
                          "name": "integer"
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
                      "alias": "month_in_a_literal_zone",
                      "expression": {
                        "operator": "month",
                        "type": {
                          "name": "integer"
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
                      "alias": "day_in_a_literal_zone",
                      "expression": {
                        "operator": "day",
                        "type": {
                          "name": "integer"
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
                      "alias": "dayOfWeek_in_a_literal_zone",
                      "expression": {
                        "operator": "dayOfWeek",
                        "type": {
                          "name": "integer"
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
                      "alias": "dayOfYear_in_a_literal_zone",
                      "expression": {
                        "operator": "dayOfYear",
                        "type": {
                          "name": "integer"
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
                      "alias": "week_in_a_literal_zone",
                      "expression": {
                        "operator": "week",
                        "type": {
                          "name": "integer"
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
                      "alias": "weekYear_in_a_literal_zone",
                      "expression": {
                        "operator": "weekYear",
                        "type": {
                          "name": "integer"
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
                      "alias": "hour_in_a_literal_zone",
                      "expression": {
                        "operator": "hour",
                        "type": {
                          "name": "integer"
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
                      "alias": "minute_in_a_literal_zone",
                      "expression": {
                        "operator": "minute",
                        "type": {
                          "name": "integer"
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
                      "alias": "second_in_a_literal_zone",
                      "expression": {
                        "operator": "second",
                        "type": {
                          "name": "integer"
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
                        "operator": "hour",
                        "type": {
                          "name": "integer"
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
                        "operator": "dayOfWeek",
                        "type": {
                          "name": "integer"
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
                        "operator": "month",
                        "type": {
                          "name": "integer"
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
                        "operator": "year",
                        "type": {
                          "name": "integer",
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
                        "operator": "minute",
                        "type": {
                          "name": "integer",
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
                        "operator": "quarter",
                        "type": {
                          "name": "integer",
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
                        "operator": "second",
                        "type": {
                          "name": "integer",
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
                        "operator": "hour",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-03-31T22:30:00Z"
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
                      "alias": "same_as_the_part_of_the_local_date",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "day",
                          "type": {
                            "name": "integer"
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
                        "right": {
                          "operator": "day",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
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
                        }
                      }
                    },
                    {
                      "alias": "microsecond_needs_no_zone",
                      "expression": {
                        "operator": "microsecond",
                        "type": {
                          "name": "integer"
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
                        "operator": "year",
                        "type": {
                          "name": "integer"
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
                        "operator": "hour",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "members",
                          "field": "preferred_call_time",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalPartKindsQuery().Value).TextValue
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
                      "name": "year_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "quarter_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "month_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "day_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "dayOfWeek_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "dayOfYear_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "week_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "weekYear_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "hour_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "minute_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "second_in_a_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "other_literal_zone",
                      "type": "long"
                    },
                    {
                      "name": "column_zone",
                      "type": "long"
                    },
                    {
                      "name": "computed_zone",
                      "type": "long"
                    },
                    {
                      "name": "nullable_zone",
                      "type": "long"
                    },
                    {
                      "name": "null_zone",
                      "type": "long"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "long"
                    },
                    {
                      "name": "nullable_operand_and_zone",
                      "type": "long"
                    },
                    {
                      "name": "literal_operand",
                      "type": "long"
                    },
                    {
                      "name": "same_as_the_part_of_the_local_date",
                      "type": "bool"
                    },
                    {
                      "name": "microsecond_needs_no_zone",
                      "type": "long"
                    },
                    {
                      "name": "of_a_date",
                      "type": "long"
                    },
                    {
                      "name": "of_a_time",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2019",
                      "3",
                      "7",
                      "10",
                      "3",
                      "191",
                      "28",
                      "2019",
                      "12",
                      "0",
                      "0",
                      "21",
                      "3",
                      "7",
                      "2019",
                      "",
                      "2",
                      "0",
                      "4",
                      "True",
                      "0",
                      "2019",
                      "9"
                    ],
                    [
                      "2020",
                      "1",
                      "1",
                      "15",
                      "3",
                      "15",
                      "3",
                      "2020",
                      "12",
                      "0",
                      "0",
                      "22",
                      "3",
                      "1",
                      "",
                      "",
                      "2",
                      "",
                      "4",
                      "True",
                      "0",
                      "2020",
                      "9"
                    ],
                    [
                      "2020",
                      "1",
                      "1",
                      "15",
                      "3",
                      "15",
                      "3",
                      "2020",
                      "12",
                      "0",
                      "0",
                      "22",
                      "3",
                      "1",
                      "2020",
                      "",
                      "2",
                      "0",
                      "4",
                      "True",
                      "0",
                      "2020",
                      "9"
                    ],
                    [
                      "2021",
                      "1",
                      "3",
                      "20",
                      "6",
                      "79",
                      "11",
                      "2021",
                      "13",
                      "0",
                      "0",
                      "23",
                      "6",
                      "3",
                      "2021",
                      "",
                      "",
                      "",
                      "4",
                      "True",
                      "0",
                      "2021",
                      "10"
                    ],
                    [
                      "2022",
                      "4",
                      "11",
                      "5",
                      "6",
                      "309",
                      "44",
                      "2022",
                      "14",
                      "30",
                      "0",
                      "0",
                      "6",
                      "11",
                      "",
                      "",
                      "2",
                      "",
                      "4",
                      "True",
                      "0",
                      "2022",
                      "11"
                    ],
                    [
                      "2023",
                      "1",
                      "2",
                      "28",
                      "2",
                      "59",
                      "9",
                      "2023",
                      "11",
                      "0",
                      "0",
                      "21",
                      "2",
                      "2",
                      "",
                      "",
                      "",
                      "",
                      "4",
                      "True",
                      "0",
                      "2023",
                      "8"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalPartKindsQuery().Result).TextValue
        );
    }
}
