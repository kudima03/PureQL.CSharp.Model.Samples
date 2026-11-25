using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record SubstringQueryTests
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
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "substring",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_tenant_id",
                          "type": {
                            "name": "uuid"
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
                        "value": 8
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": "00000386"
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
                      "alias": "name_prefix",
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
                          "value": 1
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "name_rest",
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
                      "alias": "employee_name_preview",
                      "expression": {
                        "operator": "substring",
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
            new QueryJson(new SubstringQuery().Value).TextValue
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
                      "name": "name_prefix",
                      "type": "string"
                    },
                    {
                      "name": "name_rest",
                      "type": "string"
                    },
                    {
                      "name": "employee_name_preview",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Dan",
                      "Da",
                      "an",
                      "Jac"
                    ],
                    [
                      "Eve",
                      "Ev",
                      "ve",
                      ""
                    ],
                    [
                      "Fay",
                      "Fa",
                      "ay",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SubstringQuery().Result).TextValue
        );
    }
}
