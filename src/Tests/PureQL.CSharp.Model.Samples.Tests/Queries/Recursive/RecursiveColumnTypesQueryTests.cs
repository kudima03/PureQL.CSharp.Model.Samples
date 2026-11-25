using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record RecursiveColumnTypesQueryTests
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
                      "name": "rows",
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
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_id",
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
                            },
                            {
                              "kind": "left",
                              "entity": "audit.logins",
                              "on": {
                                "operator": "and",
                                "type": {
                                  "name": "boolean"
                                },
                                "conditions": [
                                  {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "audit.logins",
                                      "field": "login_user_id",
                                      "type": {
                                        "name": "uuid",
                                        "nullable": true
                                      }
                                    },
                                    "right": {
                                      "source": "schema_with_foreign_keys.employees",
                                      "field": "employee_user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  },
                                  {
                                    "operator": "lessThan",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "audit.logins",
                                      "field": "login_at",
                                      "type": {
                                        "name": "datetime",
                                        "nullable": true
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "datetime"
                                      },
                                      "value": "2024-06-02T00:00:00Z"
                                    }
                                  }
                                ]
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
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bd-0000-0000-0000-000000000000"
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
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                },
                                "value": null
                              }
                            },
                            {
                              "alias": "level",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "weight",
                              "expression": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "1"
                              }
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              }
                            },
                            {
                              "alias": "is_root",
                              "expression": {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": true
                              }
                            },
                            {
                              "alias": "since",
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            {
                              "alias": "call_time",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_shift_start",
                                "type": {
                                  "name": "time"
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
                              "subquery": "rows",
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
                                  "source": "rows",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            },
                            {
                              "kind": "inner",
                              "entity": "schema_with_foreign_keys.users",
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
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_user_id",
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
                                    "name": "uuid",
                                    "nullable": true
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
                                "source": "rows",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "level",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "rows",
                                    "field": "level",
                                    "type": {
                                      "name": "integer"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 1
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "weight",
                              "expression": {
                                "operator": "floor",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "operator": "multiply",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "source": "rows",
                                      "field": "weight",
                                      "type": {
                                        "name": "decimal"
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "value": "2.5"
                                    }
                                  ]
                                }
                              }
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "alias": "is_root",
                              "expression": {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": false
                              }
                            },
                            {
                              "alias": "since",
                              "expression": {
                                "operator": "dateAddDays",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "rows",
                                  "field": "since",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "days": {
                                  "source": "rows",
                                  "field": "level",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            },
                            {
                              "alias": "call_time",
                              "expression": {
                                "source": "rows",
                                "field": "call_time",
                                "type": {
                                  "name": "time"
                                }
                              }
                            },
                            {
                              "alias": "seen_at",
                              "expression": {
                                "operator": "coalesce",
                                "type": {
                                  "name": "datetime"
                                },
                                "values": [
                                  {
                                    "source": "audit.logins",
                                    "field": "login_at",
                                    "type": {
                                      "name": "datetime",
                                      "nullable": true
                                    }
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "last_login",
                                    "type": {
                                      "name": "datetime"
                                    }
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "maxDepth": 12
                      }
                    }
                  ],
                  "from": {
                    "subquery": "rows"
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "rows",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "parent_id",
                      "expression": {
                        "source": "rows",
                        "field": "parent_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "level",
                      "expression": {
                        "source": "rows",
                        "field": "level",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "weight",
                      "expression": {
                        "source": "rows",
                        "field": "weight",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "source": "rows",
                        "field": "label",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "is_root",
                      "expression": {
                        "source": "rows",
                        "field": "is_root",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "since",
                      "expression": {
                        "source": "rows",
                        "field": "since",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "call_time",
                      "expression": {
                        "source": "rows",
                        "field": "call_time",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "seen_at",
                      "expression": {
                        "source": "rows",
                        "field": "seen_at",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "rows",
                        "field": "level",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "rows",
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
            new QueryJson(new RecursiveColumnTypesQuery().Value).TextValue
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
                      "name": "level",
                      "type": "long"
                    },
                    {
                      "name": "weight",
                      "type": "double"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "is_root",
                      "type": "bool"
                    },
                    {
                      "name": "since",
                      "type": "date"
                    },
                    {
                      "name": "call_time",
                      "type": "time"
                    },
                    {
                      "name": "seen_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "",
                      "0",
                      "1",
                      "",
                      "True",
                      "2020-01-15",
                      "09:00:00",
                      "2024-06-01T07:00:00Z"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "1",
                      "2",
                      "Hank",
                      "False",
                      "2020-01-15",
                      "09:00:00",
                      "2024-06-03T08:00:00Z"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "1",
                      "2",
                      "Iris",
                      "False",
                      "2020-01-15",
                      "09:00:00",
                      "2024-05-30T14:00:00Z"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "000002be-0000-0000-0000-000000000000",
                      "2",
                      "5",
                      "Jack",
                      "False",
                      "2020-01-16",
                      "09:00:00",
                      "2024-06-03T18:45:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RecursiveColumnTypesQuery().Result).TextValue
        );
    }
}
