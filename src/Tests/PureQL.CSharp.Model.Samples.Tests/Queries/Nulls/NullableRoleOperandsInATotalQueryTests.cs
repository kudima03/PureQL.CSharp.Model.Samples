using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullableRoleOperandsInATotalQueryTests
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
                      "alias": "log_of_count",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "base": {
                          "operator": "max",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "oldest_as_stars",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "*"
                        },
                        "count": {
                          "operator": "max",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_name_cut",
                      "expression": {
                        "operator": "substring",
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
                    },
                    {
                      "alias": "without_first_nickname",
                      "expression": {
                        "operator": "replace",
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
                        },
                        "pattern": {
                          "operator": "min",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          }
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        }
                      }
                    },
                    {
                      "alias": "part_of_the_longest",
                      "expression": {
                        "operator": "splitPart",
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
                            "operator": "concat",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "type": {
                                  "name": "string"
                                },
                                "value": "@mail.test"
                              }
                            ]
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": "@"
                        },
                        "index": {
                          "operator": "min",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "integerDivide",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "operator": "round",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_score",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "still_non_null",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullableRoleOperandsInATotalQuery().Value).TextValue
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
                      "name": "log_of_count",
                      "type": "double"
                    },
                    {
                      "name": "oldest_as_stars",
                      "type": "string"
                    },
                    {
                      "name": "first_name_cut",
                      "type": "string"
                    },
                    {
                      "name": "without_first_nickname",
                      "type": "string"
                    },
                    {
                      "name": "part_of_the_longest",
                      "type": "string"
                    },
                    {
                      "name": "still_non_null",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0.5268025545616607",
                      "******************************",
                      "Ann",
                      "-",
                      "Fay",
                      "10"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullableRoleOperandsInATotalQuery().Result).TextValue
        );
    }
}
