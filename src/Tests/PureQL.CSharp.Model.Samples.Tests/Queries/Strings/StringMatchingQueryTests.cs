using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record StringMatchingQueryTests
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
                    "operator": "contains",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
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
                    "pattern": {
                      "operator": "lower",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "A"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "employee_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "has_double_n",
                      "expression": {
                        "operator": "contains",
                        "type": {
                          "name": "boolean"
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
                          "value": "nn"
                        }
                      }
                    },
                    {
                      "alias": "ends_with_n",
                      "expression": {
                        "operator": "endsWith",
                        "type": {
                          "name": "boolean"
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
                          "value": "n"
                        }
                      }
                    },
                    {
                      "alias": "employee_starts_with_g",
                      "expression": {
                        "operator": "startsWith",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "G"
                        }
                      }
                    },
                    {
                      "alias": "employee_contains_initial",
                      "expression": {
                        "operator": "contains",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "operator": "lower",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
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
                              "value": 1
                            },
                            "length": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
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
            new QueryJson(new StringMatchingQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "employee_name",
                      "type": "string"
                    },
                    {
                      "name": "has_double_n",
                      "type": "bool"
                    },
                    {
                      "name": "ends_with_n",
                      "type": "bool"
                    },
                    {
                      "name": "employee_starts_with_g",
                      "type": "bool"
                    },
                    {
                      "name": "employee_contains_initial",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann",
                      "Grace",
                      "True",
                      "True",
                      "True",
                      "True"
                    ],
                    [
                      "Cara",
                      "Iris",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "Dan",
                      "Jack",
                      "False",
                      "True",
                      "False",
                      "False"
                    ],
                    [
                      "Fay",
                      "",
                      "False",
                      "False",
                      "False",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new StringMatchingQuery().Result).TextValue
        );
    }
}
