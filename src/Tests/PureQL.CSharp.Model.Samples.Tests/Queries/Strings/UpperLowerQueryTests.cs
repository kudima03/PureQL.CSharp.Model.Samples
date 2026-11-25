using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record UpperLowerQueryTests
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
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.employees",
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
                    }
                  ],
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "lower",
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
                    },
                    "right": {
                      "operator": "lower",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "CARA"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "user_name_upper",
                      "expression": {
                        "operator": "upper",
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
                    },
                    {
                      "alias": "employee_name_lower",
                      "expression": {
                        "operator": "lower",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "greeting_upper",
                      "expression": {
                        "operator": "upper",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "Gr\u00fc\u00dfe, "
                            },
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UpperLowerQuery().Value).TextValue
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
                      "name": "user_name_upper",
                      "type": "string"
                    },
                    {
                      "name": "employee_name_lower",
                      "type": "string"
                    },
                    {
                      "name": "greeting_upper",
                      "type": "string"
                    },
                    {
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "CARA",
                      "iris",
                      "GR\u00dcSSE, CARA",
                      "Cara"
                    ],
                    [
                      "DAN",
                      "jack",
                      "GR\u00dcSSE, DAN",
                      "Dan"
                    ],
                    [
                      "EVE",
                      "",
                      "GR\u00dcSSE, EVE",
                      "Eve"
                    ],
                    [
                      "FAY",
                      "",
                      "GR\u00dcSSE, FAY",
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UpperLowerQuery().Result).TextValue
        );
    }
}
