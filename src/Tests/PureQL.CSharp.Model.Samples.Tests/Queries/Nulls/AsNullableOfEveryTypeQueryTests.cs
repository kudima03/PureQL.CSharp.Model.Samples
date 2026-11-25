using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record AsNullableOfEveryTypeQueryTests
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
                      "alias": "id",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "name",
                      "expression": {
                        "operator": "asNullable",
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
                        }
                      }
                    },
                    {
                      "alias": "active",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "born",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "call_time",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "created",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "one",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "half",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        }
                      }
                    },
                    {
                      "alias": "of_a_literal",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "guest"
                        }
                      }
                    },
                    {
                      "alias": "of_an_expression",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
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
                              "value": " "
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "Smith"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "of_a_widened_integer",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "is_never_null",
                      "expression": {
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "asNullable",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "in_a_coalesce",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "values": [
                          {
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
                              "value": "Bob"
                            }
                          },
                          {
                            "operator": "asNullable",
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
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "next_name",
                      "expression": {
                        "operator": "next",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
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
                          }
                        },
                        "default": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        },
                        "over": {
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
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AsNullableOfEveryTypeQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "name",
                      "type": "string"
                    },
                    {
                      "name": "active",
                      "type": "bool"
                    },
                    {
                      "name": "born",
                      "type": "date"
                    },
                    {
                      "name": "call_time",
                      "type": "time"
                    },
                    {
                      "name": "created",
                      "type": "datetime"
                    },
                    {
                      "name": "one",
                      "type": "long"
                    },
                    {
                      "name": "half",
                      "type": "double"
                    },
                    {
                      "name": "of_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "of_an_expression",
                      "type": "string"
                    },
                    {
                      "name": "of_a_widened_integer",
                      "type": "double"
                    },
                    {
                      "name": "is_never_null",
                      "type": "bool"
                    },
                    {
                      "name": "in_a_coalesce",
                      "type": "string"
                    },
                    {
                      "name": "next_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "True",
                      "2020-01-15",
                      "09:00:00",
                      "2024-06-01T08:30:00Z",
                      "1",
                      "0.5",
                      "guest",
                      "Ann Smith",
                      "1",
                      "True",
                      "Ann",
                      "Bob"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Bob",
                      "False",
                      "2021-03-20",
                      "10:00:00",
                      "2024-06-02T09:15:00Z",
                      "1",
                      "0.5",
                      "guest",
                      "Bob Smith",
                      "1",
                      "True",
                      "Bob",
                      "Cara"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara",
                      "True",
                      "2019-07-10",
                      "09:00:00",
                      "2024-05-30T14:00:00Z",
                      "1",
                      "0.5",
                      "guest",
                      "Cara Smith",
                      "1",
                      "True",
                      "Cara",
                      "Dan"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "True",
                      "2022-11-05",
                      "11:30:00",
                      "2024-06-03T18:45:00Z",
                      "1",
                      "0.5",
                      "guest",
                      "Dan Smith",
                      "1",
                      "True",
                      "Dan",
                      "Eve"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "Eve",
                      "False",
                      "2023-02-28",
                      "08:00:00",
                      "2024-06-04T07:05:00Z",
                      "1",
                      "0.5",
                      "guest",
                      "Eve Smith",
                      "1",
                      "True",
                      "Eve",
                      "Fay"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay",
                      "True",
                      "2020-01-15",
                      "09:00:00",
                      "2024-06-01T08:30:00Z",
                      "1",
                      "0.5",
                      "guest",
                      "Fay Smith",
                      "1",
                      "True",
                      "Fay",
                      "-"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AsNullableOfEveryTypeQuery().Result).TextValue
        );
    }
}
