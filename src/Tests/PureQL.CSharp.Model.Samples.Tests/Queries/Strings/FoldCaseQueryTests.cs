using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record FoldCaseQueryTests
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
                      "operator": "foldCase",
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
                      "operator": "foldCase",
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
                      "alias": "name_key",
                      "expression": {
                        "operator": "foldCase",
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
                      "alias": "employee_name_key",
                      "expression": {
                        "operator": "foldCase",
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
                      "alias": "employee_is_grace",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "foldCase",
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
                        },
                        "right": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "GRACE"
                          }
                        }
                      }
                    },
                    {
                      "alias": "starts_with_prefix",
                      "expression": {
                        "operator": "startsWith",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "foldCase",
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
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "CA"
                          }
                        }
                      }
                    },
                    {
                      "alias": "greeting_key",
                      "expression": {
                        "operator": "foldCase",
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
                              "value": "Gr\u00fc\u00dfe "
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
                      "alias": "is_ann_or_fay",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "foldCase",
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
                          "type": {
                            "name": "string"
                          },
                          "value": "^(ann|fay)$"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "foldCase",
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
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
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
            new QueryJson(new FoldCaseQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "name_key",
                      "type": "string"
                    },
                    {
                      "name": "employee_name_key",
                      "type": "string"
                    },
                    {
                      "name": "employee_is_grace",
                      "type": "bool"
                    },
                    {
                      "name": "starts_with_prefix",
                      "type": "bool"
                    },
                    {
                      "name": "greeting_key",
                      "type": "string"
                    },
                    {
                      "name": "is_ann_or_fay",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "ann",
                      "grace",
                      "True",
                      "False",
                      "gr\u00fcsse ann",
                      "True"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara",
                      "cara",
                      "iris",
                      "False",
                      "True",
                      "gr\u00fcsse cara",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "dan",
                      "jack",
                      "False",
                      "False",
                      "gr\u00fcsse dan",
                      "False"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay",
                      "fay",
                      "",
                      "False",
                      "False",
                      "gr\u00fcsse fay",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FoldCaseQuery().Result).TextValue
        );
    }
}
