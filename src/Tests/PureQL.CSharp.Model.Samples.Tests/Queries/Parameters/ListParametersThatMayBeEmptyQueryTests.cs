using PureQL.CSharp.Model.Samples.Queries.Parameters;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Parameters;

public sealed record ListParametersThatMayBeEmptyQueryTests
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
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "or",
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
                              "param": "use_integer",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "integers",
                              "type": {
                                "name": "integerList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_decimal",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "decimals",
                              "type": {
                                "name": "decimalList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_string",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "strings",
                              "type": {
                                "name": "stringList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_boolean",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "booleans",
                              "type": {
                                "name": "booleanList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_date",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "dates",
                              "type": {
                                "name": "dateList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_time",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "times",
                              "type": {
                                "name": "timeList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_datetime",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "datetimes",
                              "type": {
                                "name": "datetimeList"
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
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
                              "param": "use_uuid",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
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
                              "param": "uuids",
                              "type": {
                                "name": "uuidList"
                              }
                            }
                          }
                        ]
                      }
                    ]
                  },
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
                      "alias": "integer_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -3
                        },
                        "list": {
                          "param": "integers",
                          "type": {
                            "name": "integerList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.1"
                        },
                        "list": {
                          "param": "decimals",
                          "type": {
                            "name": "decimalList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "string_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "b"
                        },
                        "list": {
                          "param": "strings",
                          "type": {
                            "name": "stringList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "boolean_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": false
                        },
                        "list": {
                          "param": "booleans",
                          "type": {
                            "name": "booleanList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "date_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-03-01"
                        },
                        "list": {
                          "param": "dates",
                          "type": {
                            "name": "dateList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "time_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "time"
                          },
                          "value": "17:45:30.5"
                        },
                        "list": {
                          "param": "times",
                          "type": {
                            "name": "timeList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "datetime_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-03-01T17:45:30.5Z"
                        },
                        "list": {
                          "param": "datetimes",
                          "type": {
                            "name": "datetimeList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "uuid_is_chosen",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "type": {
                            "name": "uuid"
                          },
                          "value": "00000002-0000-0000-0000-000000000000"
                        },
                        "list": {
                          "param": "uuids",
                          "type": {
                            "name": "uuidList"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ListParametersThatMayBeEmptyQuery().Value).TextValue
        );
    }
}
