using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record RegularExpressionsQueryTests
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
                    "operator": "matchesRegex",
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
                      "value": "^[A-Z][a-z]{2}$"
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
                      "alias": "initial",
                      "expression": {
                        "operator": "extractRegex",
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
                          "type": {
                            "name": "string"
                          },
                          "value": "^([A-Z])"
                        },
                        "group": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "tenant_number",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
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
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[1-9][0-9]*"
                        }
                      }
                    },
                    {
                      "alias": "double_letter",
                      "expression": {
                        "operator": "extractRegex",
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
                          "type": {
                            "name": "string"
                          },
                          "value": "nn"
                        }
                      }
                    },
                    {
                      "alias": "employee_consonants",
                      "expression": {
                        "operator": "replaceRegex",
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
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[aeiou]"
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
                      "alias": "label_with_single_spaces",
                      "expression": {
                        "operator": "replaceRegex",
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
                              "value": "  "
                            },
                            {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ]
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\s+"
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    },
                    {
                      "alias": "employee_name_ends_with_k",
                      "expression": {
                        "operator": "matchesRegex",
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
                          "value": "k$"
                        }
                      }
                    },
                    {
                      "alias": "name_matches_search",
                      "expression": {
                        "operator": "matchesRegex",
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
                          "value": "a|e"
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
            new QueryJson(new RegularExpressionsQuery().Value).TextValue
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
                      "name": "initial",
                      "type": "string"
                    },
                    {
                      "name": "tenant_number",
                      "type": "string"
                    },
                    {
                      "name": "double_letter",
                      "type": "string"
                    },
                    {
                      "name": "employee_consonants",
                      "type": "string"
                    },
                    {
                      "name": "label_with_single_spaces",
                      "type": "string"
                    },
                    {
                      "name": "employee_name_ends_with_k",
                      "type": "bool"
                    },
                    {
                      "name": "name_matches_search",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "A",
                      "385",
                      "nn",
                      "Grc",
                      "Ann 2020-01-15",
                      "False",
                      "False"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "B",
                      "385",
                      "",
                      "Hnk",
                      "Bob 2021-03-20",
                      "True",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "D",
                      "386",
                      "",
                      "Jck",
                      "Dan 2022-11-05",
                      "True",
                      "True"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "E",
                      "386",
                      "",
                      "",
                      "Eve 2023-02-28",
                      "False",
                      "True"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "F",
                      "386",
                      "",
                      "",
                      "Fay 2020-01-15",
                      "False",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegularExpressionsQuery().Result).TextValue
        );
    }
}
