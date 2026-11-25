using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record PaddingAndJoiningInATotalQueryTests
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
                  "aggregate": [
                    {
                      "alias": "bar",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "="
                        },
                        "count": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "summary",
                      "expression": {
                        "operator": "concatWith",
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
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              },
                              "predicate": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              }
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " / "
                        }
                      }
                    },
                    {
                      "alias": "first_and_last_name",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "min",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          {
                            "operator": "max",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " … "
                        }
                      }
                    },
                    {
                      "alias": "first_name_backwards",
                      "expression": {
                        "operator": "reverse",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "count_in_five_digits",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 5
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0"
                        }
                      }
                    },
                    {
                      "alias": "widest_name",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "max",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PaddingAndJoiningInATotalQuery().Value).TextValue
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
                      "name": "bar",
                      "type": "string"
                    },
                    {
                      "name": "summary",
                      "type": "string"
                    },
                    {
                      "name": "first_and_last_name",
                      "type": "string"
                    },
                    {
                      "name": "first_name_backwards",
                      "type": "string"
                    },
                    {
                      "name": "count_in_five_digits",
                      "type": "string"
                    },
                    {
                      "name": "widest_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "======",
                      "6 / 4",
                      "Ann … Fay",
                      "nnA",
                      "00006",
                      "Fay                           "
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PaddingAndJoiningInATotalQuery().Result).TextValue
        );
    }
}
