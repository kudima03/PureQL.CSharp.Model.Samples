using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record AbsentAndNullableOperandsQueryTests
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
                      "alias": "round_without_digits",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "round_with_nullable_digits",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "digits": {
                          "operator": "modulo",
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
                            "value": 3
                          }
                        }
                      }
                    },
                    {
                      "alias": "round_nullable_half_even",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "digits": {
                          "operator": "modulo",
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
                            "value": 3
                          }
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "truncate_without_digits",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "substring_to_the_end",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "substring_nullable_length",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "length": {
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
                    },
                    {
                      "alias": "substring_nullable_start",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "start": {
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
                    },
                    {
                      "alias": "position_from_the_start",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a"
                        }
                      }
                    },
                    {
                      "alias": "position_nullable_start",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a"
                        },
                        "start": {
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
                    },
                    {
                      "alias": "everything_nullable",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
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
                        },
                        "start": {
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
                        },
                        "length": {
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
                    },
                    {
                      "alias": "only_operand_nullable",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AbsentAndNullableOperandsQuery().Value).TextValue
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
                      "name": "round_without_digits",
                      "type": "long"
                    },
                    {
                      "name": "round_with_nullable_digits",
                      "type": "double"
                    },
                    {
                      "name": "round_nullable_half_even",
                      "type": "double"
                    },
                    {
                      "name": "truncate_without_digits",
                      "type": "long"
                    },
                    {
                      "name": "substring_to_the_end",
                      "type": "string"
                    },
                    {
                      "name": "substring_nullable_length",
                      "type": "string"
                    },
                    {
                      "name": "substring_nullable_start",
                      "type": "string"
                    },
                    {
                      "name": "position_from_the_start",
                      "type": "long"
                    },
                    {
                      "name": "position_nullable_start",
                      "type": "long"
                    },
                    {
                      "name": "everything_nullable",
                      "type": "string"
                    },
                    {
                      "name": "only_operand_nullable",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "3",
                      "3",
                      "2",
                      "2",
                      "nn",
                      "nn",
                      "n",
                      "0",
                      "0",
                      "n",
                      "An"
                    ],
                    [
                      "3",
                      "",
                      "",
                      "2",
                      "ob",
                      "",
                      "",
                      "0",
                      "",
                      "",
                      "Bo"
                    ],
                    [
                      "3",
                      "3",
                      "2",
                      "2",
                      "ara",
                      "ara",
                      "ra",
                      "2",
                      "4",
                      "",
                      ""
                    ],
                    [
                      "3",
                      "",
                      "",
                      "2",
                      "an",
                      "",
                      "",
                      "2",
                      "",
                      "",
                      "Da"
                    ],
                    [
                      "3",
                      "2.5",
                      "2.5",
                      "2",
                      "ve",
                      "v",
                      "Eve",
                      "0",
                      "0",
                      "E",
                      "Ev"
                    ],
                    [
                      "3",
                      "2.5",
                      "2.5",
                      "2",
                      "ay",
                      "ay",
                      "ay",
                      "2",
                      "2",
                      "ay",
                      "Fa"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AbsentAndNullableOperandsQuery().Result).TextValue
        );
    }
}
