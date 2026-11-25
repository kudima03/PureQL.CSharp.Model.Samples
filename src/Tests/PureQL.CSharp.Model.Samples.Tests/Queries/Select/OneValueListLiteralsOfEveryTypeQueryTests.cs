using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record OneValueListLiteralsOfEveryTypeQueryTests
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
                  "select": [
                    {
                      "alias": "one_integer",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        },
                        "list": {
                          "type": {
                            "name": "integerList"
                          },
                          "value": [
                            7
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_decimal",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "list": {
                          "type": {
                            "name": "decimalList"
                          },
                          "value": [
                            "2.5"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_string",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a"
                        },
                        "list": {
                          "type": {
                            "name": "stringList"
                          },
                          "value": [
                            "a"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_boolean",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": true
                        },
                        "list": {
                          "type": {
                            "name": "booleanList"
                          },
                          "value": [
                            true
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_date",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-01-31"
                        },
                        "list": {
                          "type": {
                            "name": "dateList"
                          },
                          "value": [
                            "2024-01-31"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_time",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "time"
                          },
                          "value": "08:30:00"
                        },
                        "list": {
                          "type": {
                            "name": "timeList"
                          },
                          "value": [
                            "08:30:00"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_datetime",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-01-31T08:30:00Z"
                        },
                        "list": {
                          "type": {
                            "name": "datetimeList"
                          },
                          "value": [
                            "2024-01-31T08:30:00Z"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "one_uuid",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "uuid"
                          },
                          "value": "00000001-0000-0000-0000-000000000000"
                        },
                        "list": {
                          "type": {
                            "name": "uuidList"
                          },
                          "value": [
                            "00000001-0000-0000-0000-000000000000"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "integer_in_one_decimal",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        },
                        "list": {
                          "type": {
                            "name": "decimalList"
                          },
                          "value": [
                            "7"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "null_in_one_string",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "list": {
                          "type": {
                            "name": "stringList"
                          },
                          "value": [
                            ""
                          ]
                        }
                      }
                    },
                    {
                      "alias": "not_in_one",
                      "expression": {
                        "operator": "not",
                        "type": {
                          "name": "boolean"
                        },
                        "condition": {
                          "operator": "in",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "list": {
                            "type": {
                              "name": "stringList"
                            },
                            "value": [
                              "cancelled"
                            ]
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OneValueListLiteralsOfEveryTypeQuery().Value).TextValue
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
                      "name": "one_integer",
                      "type": "bool"
                    },
                    {
                      "name": "one_decimal",
                      "type": "bool"
                    },
                    {
                      "name": "one_string",
                      "type": "bool"
                    },
                    {
                      "name": "one_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "one_date",
                      "type": "bool"
                    },
                    {
                      "name": "one_time",
                      "type": "bool"
                    },
                    {
                      "name": "one_datetime",
                      "type": "bool"
                    },
                    {
                      "name": "one_uuid",
                      "type": "bool"
                    },
                    {
                      "name": "integer_in_one_decimal",
                      "type": "bool"
                    },
                    {
                      "name": "null_in_one_string",
                      "type": "bool"
                    },
                    {
                      "name": "not_in_one",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OneValueListLiteralsOfEveryTypeQuery().Result).TextValue
        );
    }
}
