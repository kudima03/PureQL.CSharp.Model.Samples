using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record DirectReportsPerManagerQueryTests
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
                    "entity": "schema_with_foreign_keys.employees",
                    "alias": "employee"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "manager",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "employee",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "manager",
                          "field": "employee_id",
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
                          "source": "employee",
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
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "employee",
                      "field": "employee_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    "right": {
                      "source": "employee",
                      "field": "employee_manager_id",
                      "type": {
                        "name": "uuid",
                        "nullable": true
                      }
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "manager",
                      "expression": {
                        "source": "manager",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "manager_shift_start",
                      "expression": {
                        "source": "manager",
                        "field": "employee_shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "manager_id",
                      "expression": {
                        "source": "manager",
                        "field": "employee_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "manager",
                      "expression": {
                        "key": "manager",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "manager_shift_start",
                      "expression": {
                        "key": "manager_shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "direct_reports",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "avg_report_score",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "average",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_score",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0"
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "active_share_pct",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        },
                        "then": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0"
                        },
                        "else": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              },
                              {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "predicate": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_active",
                                      "type": {
                                        "name": "boolean"
                                      }
                                    }
                                  },
                                  {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
                                    }
                                  }
                                ]
                              }
                            ]
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "earliest_shift",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "time"
                        },
                        "selector": {
                          "source": "employee",
                          "field": "employee_shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "earliest_signup",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "manager",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DirectReportsPerManagerQuery().Value).TextValue
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
                      "name": "manager",
                      "type": "string"
                    },
                    {
                      "name": "manager_shift_start",
                      "type": "time"
                    },
                    {
                      "name": "direct_reports",
                      "type": "long"
                    },
                    {
                      "name": "avg_report_score",
                      "type": "double"
                    },
                    {
                      "name": "active_share_pct",
                      "type": "double"
                    },
                    {
                      "name": "earliest_shift",
                      "type": "time"
                    },
                    {
                      "name": "earliest_signup",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Grace",
                      "09:00:00",
                      "2",
                      "30",
                      "50",
                      "10:00:00",
                      "2019-07-10"
                    ],
                    [
                      "Hank",
                      "10:00:00",
                      "1",
                      "0",
                      "100",
                      "08:00:00",
                      "2022-11-05"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DirectReportsPerManagerQuery().Result).TextValue
        );
    }
}
