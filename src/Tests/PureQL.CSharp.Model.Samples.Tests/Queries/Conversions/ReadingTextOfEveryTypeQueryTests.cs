using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record ReadingTextOfEveryTypeQueryTests
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
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "placed_at",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    "right": {
                      "operator": "asDatetime",
                      "type": {
                        "name": "datetime"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "2024-06-04T00:00:00Z"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "integer_parsed",
                      "expression": {
                        "operator": "asInteger",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "42"
                        }
                      }
                    },
                    {
                      "alias": "decimal_parsed",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "19.9"
                        }
                      }
                    },
                    {
                      "alias": "boolean_parsed",
                      "expression": {
                        "operator": "asBoolean",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "true"
                        }
                      }
                    },
                    {
                      "alias": "date_parsed",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-06-15"
                        }
                      }
                    },
                    {
                      "alias": "time_parsed",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "13:45:30"
                        }
                      }
                    },
                    {
                      "alias": "datetime_parsed",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-06-15T13:45:30Z"
                        }
                      }
                    },
                    {
                      "alias": "uuid_parsed",
                      "expression": {
                        "operator": "asUuid",
                        "type": {
                          "name": "uuid"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0000007b-0000-0000-0000-000000000000"
                        }
                      }
                    },
                    {
                      "alias": "integer_or_null",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "42"
                        }
                      }
                    },
                    {
                      "alias": "decimal_or_null",
                      "expression": {
                        "operator": "tryAsDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "19.9"
                        }
                      }
                    },
                    {
                      "alias": "boolean_or_null",
                      "expression": {
                        "operator": "tryAsBoolean",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "true"
                        }
                      }
                    },
                    {
                      "alias": "date_or_null",
                      "expression": {
                        "operator": "tryAsDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-06-15"
                        }
                      }
                    },
                    {
                      "alias": "time_or_null",
                      "expression": {
                        "operator": "tryAsTime",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "13:45:30"
                        }
                      }
                    },
                    {
                      "alias": "datetime_or_null",
                      "expression": {
                        "operator": "tryAsDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-06-15T13:45:30Z"
                        }
                      }
                    },
                    {
                      "alias": "uuid_or_null",
                      "expression": {
                        "operator": "tryAsUuid",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0000007b-0000-0000-0000-000000000000"
                        }
                      }
                    },
                    {
                      "alias": "integer_lifted",
                      "expression": {
                        "operator": "asInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "7"
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_lifted",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "boolean_lifted",
                      "expression": {
                        "operator": "asBoolean",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "false"
                          }
                        }
                      }
                    },
                    {
                      "alias": "date_lifted",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "2024-02-29"
                          }
                        }
                      }
                    },
                    {
                      "alias": "time_lifted",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "datetime_lifted",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "2024-01-31T23:30:00Z"
                          }
                        }
                      }
                    },
                    {
                      "alias": "uuid_lifted",
                      "expression": {
                        "operator": "asUuid",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "integer_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "7"
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "boolean_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsBoolean",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "false"
                          }
                        }
                      }
                    },
                    {
                      "alias": "date_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "2024-02-29"
                          }
                        }
                      }
                    },
                    {
                      "alias": "time_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsTime",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "datetime_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "2024-01-31T23:30:00Z"
                          }
                        }
                      }
                    },
                    {
                      "alias": "uuid_of_nullable_or_null",
                      "expression": {
                        "operator": "tryAsUuid",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "round_trip",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "same_text",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "asString",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "operator": "tryAsDecimal",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "operand": {
                              "type": {
                                "name": "string"
                              },
                              "value": "19.9"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "19.9"
                        }
                      }
                    },
                    {
                      "alias": "first_of_year",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "year",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "-01-01"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "status_code",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "substring",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "start": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "length": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 3
                          }
                        }
                      }
                    },
                    {
                      "alias": "total_prefix",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "substring",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          "start": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "length": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "day_of_a_datetime_text",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "operator": "asDatetime",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "2024-06-15T13:45:30Z"
                          }
                        }
                      }
                    },
                    {
                      "alias": "day_of_a_datetime",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "day_of_a_date_text",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-06-15"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ReadingTextOfEveryTypeQuery().Value).TextValue
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
                      "name": "integer_parsed",
                      "type": "long"
                    },
                    {
                      "name": "decimal_parsed",
                      "type": "double"
                    },
                    {
                      "name": "boolean_parsed",
                      "type": "bool"
                    },
                    {
                      "name": "date_parsed",
                      "type": "date"
                    },
                    {
                      "name": "time_parsed",
                      "type": "time"
                    },
                    {
                      "name": "datetime_parsed",
                      "type": "datetime"
                    },
                    {
                      "name": "uuid_parsed",
                      "type": "uuid"
                    },
                    {
                      "name": "integer_or_null",
                      "type": "long"
                    },
                    {
                      "name": "decimal_or_null",
                      "type": "double"
                    },
                    {
                      "name": "boolean_or_null",
                      "type": "bool"
                    },
                    {
                      "name": "date_or_null",
                      "type": "date"
                    },
                    {
                      "name": "time_or_null",
                      "type": "time"
                    },
                    {
                      "name": "datetime_or_null",
                      "type": "datetime"
                    },
                    {
                      "name": "uuid_or_null",
                      "type": "uuid"
                    },
                    {
                      "name": "integer_lifted",
                      "type": "long"
                    },
                    {
                      "name": "decimal_lifted",
                      "type": "double"
                    },
                    {
                      "name": "boolean_lifted",
                      "type": "bool"
                    },
                    {
                      "name": "date_lifted",
                      "type": "date"
                    },
                    {
                      "name": "time_lifted",
                      "type": "time"
                    },
                    {
                      "name": "datetime_lifted",
                      "type": "datetime"
                    },
                    {
                      "name": "uuid_lifted",
                      "type": "uuid"
                    },
                    {
                      "name": "integer_of_nullable_or_null",
                      "type": "long"
                    },
                    {
                      "name": "decimal_of_nullable_or_null",
                      "type": "double"
                    },
                    {
                      "name": "boolean_of_nullable_or_null",
                      "type": "bool"
                    },
                    {
                      "name": "date_of_nullable_or_null",
                      "type": "date"
                    },
                    {
                      "name": "time_of_nullable_or_null",
                      "type": "time"
                    },
                    {
                      "name": "datetime_of_nullable_or_null",
                      "type": "datetime"
                    },
                    {
                      "name": "uuid_of_nullable_or_null",
                      "type": "uuid"
                    },
                    {
                      "name": "round_trip",
                      "type": "bool"
                    },
                    {
                      "name": "same_text",
                      "type": "bool"
                    },
                    {
                      "name": "first_of_year",
                      "type": "date"
                    },
                    {
                      "name": "status_code",
                      "type": "long"
                    },
                    {
                      "name": "total_prefix",
                      "type": "long"
                    },
                    {
                      "name": "day_of_a_datetime_text",
                      "type": "date"
                    },
                    {
                      "name": "day_of_a_datetime",
                      "type": "date"
                    },
                    {
                      "name": "day_of_a_date_text",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "42",
                      "19.9",
                      "True",
                      "2024-06-15",
                      "13:45:30",
                      "2024-06-15T13:45:30Z",
                      "0000007b-0000-0000-0000-000000000000",
                      "42",
                      "19.9",
                      "True",
                      "2024-06-15",
                      "13:45:30",
                      "2024-06-15T13:45:30Z",
                      "0000007b-0000-0000-0000-000000000000",
                      "7",
                      "",
                      "False",
                      "2024-02-29",
                      "",
                      "2024-01-31T23:30:00Z",
                      "",
                      "7",
                      "",
                      "False",
                      "2024-02-29",
                      "",
                      "2024-01-31T23:30:00Z",
                      "",
                      "True",
                      "True",
                      "2024-01-01",
                      "",
                      "75",
                      "2024-06-15",
                      "2024-06-04",
                      "2024-06-15"
                    ],
                    [
                      "42",
                      "19.9",
                      "True",
                      "2024-06-15",
                      "13:45:30",
                      "2024-06-15T13:45:30Z",
                      "0000007b-0000-0000-0000-000000000000",
                      "42",
                      "19.9",
                      "True",
                      "2024-06-15",
                      "13:45:30",
                      "2024-06-15T13:45:30Z",
                      "0000007b-0000-0000-0000-000000000000",
                      "7",
                      "",
                      "False",
                      "2024-02-29",
                      "",
                      "2024-01-31T23:30:00Z",
                      "",
                      "7",
                      "",
                      "False",
                      "2024-02-29",
                      "",
                      "2024-01-31T23:30:00Z",
                      "",
                      "True",
                      "True",
                      "2024-01-01",
                      "",
                      "30",
                      "2024-06-15",
                      "2024-06-05",
                      "2024-06-15"
                    ],
                    [
                      "42",
                      "19.9",
                      "True",
                      "2024-06-15",
                      "13:45:30",
                      "2024-06-15T13:45:30Z",
                      "0000007b-0000-0000-0000-000000000000",
                      "42",
                      "19.9",
                      "True",
                      "2024-06-15",
                      "13:45:30",
                      "2024-06-15T13:45:30Z",
                      "0000007b-0000-0000-0000-000000000000",
                      "7",
                      "",
                      "False",
                      "2024-02-29",
                      "",
                      "2024-01-31T23:30:00Z",
                      "",
                      "7",
                      "",
                      "False",
                      "2024-02-29",
                      "",
                      "2024-01-31T23:30:00Z",
                      "",
                      "True",
                      "True",
                      "2024-01-01",
                      "",
                      "10",
                      "2024-06-15",
                      "2024-06-06",
                      "2024-06-15"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ReadingTextOfEveryTypeQuery().Result).TextValue
        );
    }
}
