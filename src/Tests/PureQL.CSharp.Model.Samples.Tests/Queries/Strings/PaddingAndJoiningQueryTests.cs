using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record PaddingAndJoiningQueryTests
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
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "e",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "e",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
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
                    },
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "m",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "m",
                          "field": "employee_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "e",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "concatWith",
                      "type": {
                        "name": "string"
                      },
                      "values": [
                        {
                          "source": "e",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        {
                          "source": "m",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      ],
                      "separator": {
                        "type": {
                          "name": "string"
                        },
                        "value": " / "
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": ""
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
                      "alias": "full_name",
                      "expression": {
                        "operator": "concatWith",
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
                            "source": "e",
                            "field": "employee_name",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "source": "m",
                            "field": "employee_name",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    },
                    {
                      "alias": "reporting_line",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "e",
                            "field": "employee_name",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "source": "m",
                            "field": "employee_name",
                            "type": {
                              "name": "string",
                              "nullable": true
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
                      "alias": "score_in_three_digits",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string",
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
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
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
                      "alias": "name_column",
                      "expression": {
                        "operator": "padEnd",
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
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 8
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "."
                        }
                      }
                    },
                    {
                      "alias": "age_stars",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "*"
                        },
                        "count": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
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
                                "value": 10
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "reversed_name",
                      "expression": {
                        "operator": "reverse",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
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
            new QueryJson(new PaddingAndJoiningQuery().Value).TextValue
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
                      "name": "full_name",
                      "type": "string"
                    },
                    {
                      "name": "reporting_line",
                      "type": "string"
                    },
                    {
                      "name": "score_in_three_digits",
                      "type": "string"
                    },
                    {
                      "name": "name_column",
                      "type": "string"
                    },
                    {
                      "name": "age_stars",
                      "type": "string"
                    },
                    {
                      "name": "reversed_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann Grace",
                      "Grace",
                      "030",
                      "Ann.....",
                      "***",
                      "nnA"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Bob Hank Grace",
                      "Hank / Grace",
                      "",
                      "Bob.....",
                      "**",
                      "boB"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara Iris Grace",
                      "Iris / Grace",
                      "030",
                      "Cara....",
                      "***",
                      "araC"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan Jack Hank",
                      "Jack / Hank",
                      "",
                      "Dan.....",
                      "****",
                      "naD"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PaddingAndJoiningQuery().Result).TextValue
        );
    }
}
