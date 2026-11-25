using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record OperatorsWithAndWithoutAZoneQueryTests
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
                      "name": "user_clocks",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.users"
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
                            "alias": "signed_up_at",
                            "expression": {
                              "operator": "datetimeAddSeconds",
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
                              "seconds": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "45"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "user_clocks",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "user_clocks",
                          "field": "user_id",
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
                    "source": "schema_with_foreign_keys.users",
                    "field": "user_active",
                    "type": {
                      "name": "boolean"
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
                      "alias": "signed_up_at",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signed_up_local_second",
                      "expression": {
                        "operator": "second",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "user_clocks",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signed_up_microsecond",
                      "expression": {
                        "operator": "microsecond",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signed_up_local_minute",
                      "expression": {
                        "operator": "startOfMinute",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "user_clocks",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signed_up_second",
                      "expression": {
                        "operator": "startOfSecond",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "signed_up_local_date",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "user_clocks",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "local_half_year_later",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "user_clocks",
                          "field": "signed_up_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        },
                        "zone": {
                          "source": "user_clocks",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "half_year_later_date",
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "user_clocks",
                            "field": "signed_up_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "user_clocks",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        }
                      }
                    },
                    {
                      "alias": "age_in_months",
                      "expression": {
                        "operator": "dateDiffMonths",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-06"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "signed_up_at"
                    },
                    {
                      "column": "user_id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OperatorsWithAndWithoutAZoneQuery().Value).TextValue
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
                      "name": "signed_up_at",
                      "type": "string"
                    },
                    {
                      "name": "signed_up_local_second",
                      "type": "long"
                    },
                    {
                      "name": "signed_up_microsecond",
                      "type": "long"
                    },
                    {
                      "name": "signed_up_local_minute",
                      "type": "datetime"
                    },
                    {
                      "name": "signed_up_second",
                      "type": "datetime"
                    },
                    {
                      "name": "signed_up_local_date",
                      "type": "date"
                    },
                    {
                      "name": "local_half_year_later",
                      "type": "datetime"
                    },
                    {
                      "name": "half_year_later_date",
                      "type": "date"
                    },
                    {
                      "name": "age_in_months",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2024-05-30T14:00:45Z",
                      "45",
                      "0",
                      "2024-05-30T14:00:00Z",
                      "2024-05-30T14:00:45Z",
                      "2024-05-30",
                      "2024-11-30T15:00:45Z",
                      "2024-11-30",
                      "58"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-01T08:30:45Z",
                      "45",
                      "0",
                      "2024-06-01T08:30:00Z",
                      "2024-06-01T08:30:45Z",
                      "2024-06-01",
                      "2024-12-01T09:30:45Z",
                      "2024-12-01",
                      "52"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "2024-06-01T08:30:45Z",
                      "45",
                      "0",
                      "2024-06-01T08:30:00Z",
                      "2024-06-01T08:30:45Z",
                      "2024-06-01",
                      "2024-12-01T08:30:45Z",
                      "2024-12-01",
                      "52"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2024-06-03T18:45:45Z",
                      "45",
                      "0",
                      "2024-06-03T18:45:00Z",
                      "2024-06-03T18:45:45Z",
                      "2024-06-04",
                      "2024-12-03T18:45:45Z",
                      "2024-12-04",
                      "19"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OperatorsWithAndWithoutAZoneQuery().Result).TextValue
        );
    }
}
