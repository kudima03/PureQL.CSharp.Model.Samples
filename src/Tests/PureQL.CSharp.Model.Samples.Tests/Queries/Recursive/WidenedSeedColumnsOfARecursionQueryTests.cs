using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record WidenedSeedColumnsOfARecursionQueryTests
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
                      "name": "tree",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "entity": "schema_with_foreign_keys.users",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_user_id",
                                  "type": {
                                    "name": "uuid"
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
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_manager_id",
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "parent_id",
                              "expression": {
                                "operator": "asNullable",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            },
                            {
                              "alias": "seen_at",
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
                              "alias": "score",
                              "expression": {
                                "operator": "asDecimal",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "operand": {
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
                            },
                            {
                              "alias": "share",
                              "expression": {
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
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "operator": "asNullable",
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_name",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "tree",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_manager_id",
                                  "type": {
                                    "name": "uuid",
                                    "nullable": true
                                  }
                                },
                                "right": {
                                  "source": "tree",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            },
                            {
                              "kind": "left",
                              "entity": "audit.logins",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "audit.logins",
                                  "field": "login_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            }
                          ],
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "parent_id",
                              "expression": {
                                "source": "tree",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "seen_at",
                              "expression": {
                                "source": "audit.logins",
                                "field": "login_at",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "alias": "score",
                              "expression": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "values": [
                                  {
                                    "source": "tree",
                                    "field": "score",
                                    "type": {
                                      "name": "decimal",
                                      "nullable": true
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "0.5"
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "share",
                              "expression": {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "tree",
                                    "field": "share",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 2
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "operator": "nullIfEqual",
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_name",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Hank"
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 20
                      }
                    }
                  ],
                  "from": {
                    "subquery": "tree"
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "tree",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "parent_id",
                      "expression": {
                        "source": "tree",
                        "field": "parent_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "seen_at",
                      "expression": {
                        "source": "tree",
                        "field": "seen_at",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "score",
                      "expression": {
                        "source": "tree",
                        "field": "score",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "share",
                      "expression": {
                        "source": "tree",
                        "field": "share",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "source": "tree",
                        "field": "label",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "tree",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WidenedSeedColumnsOfARecursionQuery().Value).TextValue
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
                      "name": "parent_id",
                      "type": "uuid"
                    },
                    {
                      "name": "seen_at",
                      "type": "datetime"
                    },
                    {
                      "name": "score",
                      "type": "double"
                    },
                    {
                      "name": "share",
                      "type": "double"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "2024-06-01T08:30:00Z",
                      "30",
                      "1",
                      "Grace"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "2024-06-03T08:00:00Z",
                      "15",
                      "0.5",
                      ""
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "",
                      "15",
                      "0.5",
                      "Iris"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "000002be-0000-0000-0000-000000000000",
                      "",
                      "7.5",
                      "0.25",
                      "Jack"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WidenedSeedColumnsOfARecursionQuery().Result).TextValue
        );
    }
}
