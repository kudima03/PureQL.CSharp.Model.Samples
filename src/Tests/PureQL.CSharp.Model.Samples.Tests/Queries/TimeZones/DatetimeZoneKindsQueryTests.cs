using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record DatetimeZoneKindsQueryTests
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
                  "select": [
                    {
                      "alias": "literal_zone",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
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
                      "alias": "fixed_offset",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Etc/GMT-3"
                        }
                      }
                    },
                    {
                      "alias": "parameter_zone",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
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
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "zone": {
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
                      }
                    },
                    {
                      "alias": "computed_zone",
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
                        },
                        "zone": {
                          "operator": "coalesce",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "operator": "if",
                              "type": {
                                "name": "string",
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
                                "type": {
                                  "name": "string"
                                },
                                "value": "Europe/Paris"
                              },
                              "else": {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              }
                            },
                            {
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "nullable_zone",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string",
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
                            "type": {
                              "name": "string"
                            },
                            "value": "Europe/Paris"
                          },
                          "else": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "null_zone",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
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
                      "alias": "nullable_date",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "if",
                          "type": {
                            "name": "date",
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
                            "field": "signup_date",
                            "type": {
                              "name": "date"
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
                        "zone": {
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
                      }
                    },
                    {
                      "alias": "nullable_date_part",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "operator": "if",
                          "type": {
                            "name": "date",
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
                            "field": "signup_date",
                            "type": {
                              "name": "date"
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
                        "time": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "zone": {
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
                      }
                    },
                    {
                      "alias": "nullable_time_part",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "time": {
                          "operator": "if",
                          "type": {
                            "name": "time",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "time",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "zone": {
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
                      }
                    },
                    {
                      "alias": "everything_nullable",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "operator": "if",
                          "type": {
                            "name": "date",
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
                            "field": "signup_date",
                            "type": {
                              "name": "date"
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
                        "time": {
                          "operator": "if",
                          "type": {
                            "name": "time",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "time",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string",
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
                            "type": {
                              "name": "string"
                            },
                            "value": "Europe/Paris"
                          },
                          "else": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "literal_parts",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-10"
                        },
                        "time": {
                          "type": {
                            "name": "time"
                          },
                          "value": "02:30:00"
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "America/New_York"
                        }
                      }
                    },
                    {
                      "alias": "parts_from_parts",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
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
                            "value": 1
                          },
                          "day": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 31
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
                      "alias": "without_a_zone",
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
                      "alias": "of_a_string",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-01-31T18:30:00Z"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DatetimeZoneKindsQuery().Value).TextValue
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
                      "name": "literal_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "fixed_offset",
                      "type": "datetime"
                    },
                    {
                      "name": "parameter_zone",
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
                      "name": "nullable_date",
                      "type": "datetime"
                    },
                    {
                      "name": "nullable_date_part",
                      "type": "datetime"
                    },
                    {
                      "name": "nullable_time_part",
                      "type": "datetime"
                    },
                    {
                      "name": "everything_nullable",
                      "type": "datetime"
                    },
                    {
                      "name": "literal_parts",
                      "type": "datetime"
                    },
                    {
                      "name": "parts_from_parts",
                      "type": "datetime"
                    },
                    {
                      "name": "without_a_zone",
                      "type": "datetime"
                    },
                    {
                      "name": "of_a_string",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2019-07-09T21:00:00Z",
                      "2019-07-09T21:00:00Z",
                      "2019-07-09T12:00:00Z",
                      "2019-07-10T04:00:00Z",
                      "2019-07-10T07:00:00Z",
                      "2019-07-09T22:00:00Z",
                      "",
                      "2019-07-10T04:00:00Z",
                      "2019-07-10T13:00:00Z",
                      "2019-07-10T13:00:00Z",
                      "2019-07-10T07:00:00Z",
                      "2024-06-10T06:30:00Z",
                      "2024-01-31T15:30:00Z",
                      "2019-07-10T09:00:00Z",
                      "2024-01-31T18:30:00Z"
                    ],
                    [
                      "2020-01-14T21:00:00Z",
                      "2020-01-14T21:00:00Z",
                      "2020-01-14T11:00:00Z",
                      "2020-01-14T15:00:00Z",
                      "2020-01-15T08:00:00Z",
                      "2020-01-14T23:00:00Z",
                      "",
                      "2020-01-14T15:00:00Z",
                      "2020-01-15T00:00:00Z",
                      "",
                      "",
                      "2024-06-10T06:30:00Z",
                      "2024-01-31T15:30:00Z",
                      "2020-01-15T09:00:00Z",
                      "2024-01-31T18:30:00Z"
                    ],
                    [
                      "2020-01-14T21:00:00Z",
                      "2020-01-14T21:00:00Z",
                      "2020-01-14T11:00:00Z",
                      "2020-01-15T05:00:00Z",
                      "2020-01-15T08:00:00Z",
                      "2020-01-14T23:00:00Z",
                      "",
                      "2020-01-15T05:00:00Z",
                      "2020-01-15T14:00:00Z",
                      "2020-01-15T14:00:00Z",
                      "2020-01-15T08:00:00Z",
                      "2024-06-10T06:30:00Z",
                      "2024-01-31T15:30:00Z",
                      "2020-01-15T09:00:00Z",
                      "2024-01-31T18:30:00Z"
                    ],
                    [
                      "2021-03-19T21:00:00Z",
                      "2021-03-19T21:00:00Z",
                      "2021-03-19T11:00:00Z",
                      "2021-03-20T04:00:00Z",
                      "2021-03-20T14:00:00Z",
                      "",
                      "",
                      "",
                      "",
                      "2021-03-20T14:00:00Z",
                      "",
                      "2024-06-10T06:30:00Z",
                      "2024-01-31T15:30:00Z",
                      "2021-03-20T10:00:00Z",
                      "2024-01-31T18:30:00Z"
                    ],
                    [
                      "2022-11-04T21:00:00Z",
                      "2022-11-04T21:00:00Z",
                      "2022-11-04T11:00:00Z",
                      "2022-11-04T15:00:00Z",
                      "2022-11-05T10:30:00Z",
                      "2022-11-04T23:00:00Z",
                      "",
                      "2022-11-04T15:00:00Z",
                      "2022-11-05T02:30:00Z",
                      "",
                      "",
                      "2024-06-10T06:30:00Z",
                      "2024-01-31T15:30:00Z",
                      "2022-11-05T11:30:00Z",
                      "2024-01-31T18:30:00Z"
                    ],
                    [
                      "2023-02-27T21:00:00Z",
                      "2023-02-27T21:00:00Z",
                      "2023-02-27T11:00:00Z",
                      "2023-02-27T15:00:00Z",
                      "2023-02-27T23:00:00Z",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "2024-06-10T06:30:00Z",
                      "2024-01-31T15:30:00Z",
                      "2023-02-28T08:00:00Z",
                      "2024-01-31T18:30:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DatetimeZoneKindsQuery().Result).TextValue
        );
    }
}
