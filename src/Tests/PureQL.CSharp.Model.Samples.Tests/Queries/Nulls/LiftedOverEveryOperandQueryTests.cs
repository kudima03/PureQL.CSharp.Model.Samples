using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record LiftedOverEveryOperandQueryTests
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
                      "name": "nicknames",
                      "rows": [
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000001-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "An"
                            }
                          },
                          {
                            "alias": "letters",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000003-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ra"
                            }
                          },
                          {
                            "alias": "letters",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000004-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Da"
                            }
                          },
                          {
                            "alias": "letters",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000006-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ay"
                            }
                          },
                          {
                            "alias": "letters",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "nicknames",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "nicknames",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
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
                      "alias": "growth",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1.1"
                        },
                        "exponent": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "age_in_weeks_rounded",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
                            }
                          ]
                        },
                        "digits": {
                          "source": "nicknames",
                          "field": "letters",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "stars",
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
                          "source": "nicknames",
                          "field": "letters",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "name_without_nickname",
                      "expression": {
                        "operator": "replace",
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
                        "pattern": {
                          "source": "nicknames",
                          "field": "nickname",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    },
                    {
                      "alias": "nickname_starts_at",
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
                          "source": "nicknames",
                          "field": "nickname",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_letters",
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
                          "value": 1
                        },
                        "length": {
                          "source": "nicknames",
                          "field": "letters",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "padded_name",
                      "expression": {
                        "operator": "padEnd",
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
                        "length": {
                          "operator": "add",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "values": [
                            {
                              "source": "nicknames",
                              "field": "letters",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          ]
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "."
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LiftedOverEveryOperandQuery().Value).TextValue
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
                      "name": "growth",
                      "type": "double"
                    },
                    {
                      "name": "age_in_weeks_rounded",
                      "type": "double"
                    },
                    {
                      "name": "stars",
                      "type": "string"
                    },
                    {
                      "name": "name_without_nickname",
                      "type": "string"
                    },
                    {
                      "name": "nickname_starts_at",
                      "type": "long"
                    },
                    {
                      "name": "first_letters",
                      "type": "string"
                    },
                    {
                      "name": "padded_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "17.449402268886406",
                      "4.29",
                      "**",
                      "n",
                      "1",
                      "An",
                      "Ann..."
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "17.449402268886406",
                      "4.286",
                      "***",
                      "Ca",
                      "3",
                      "Car",
                      "Cara..."
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "",
                      "6",
                      "*",
                      "n",
                      "1",
                      "D",
                      "Dan.."
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "2.5937424601",
                      "",
                      "",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "14.420993610649923",
                      "4",
                      "**",
                      "F",
                      "2",
                      "Fa",
                      "Fay..."
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LiftedOverEveryOperandQuery().Result).TextValue
        );
    }
}
