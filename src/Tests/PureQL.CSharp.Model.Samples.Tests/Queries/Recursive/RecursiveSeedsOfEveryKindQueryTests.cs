using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record RecursiveSeedsOfEveryKindQueryTests
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
                      "name": "from_top_managers",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "where": {
                            "operator": "notEqual",
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
                          "groupBy": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_manager_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            }
                          ],
                          "having": {
                            "operator": "greaterThanOrEqual",
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
                              "value": 2
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "key": "id",
                                "type": {
                                  "name": "uuid",
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
                              "subquery": "from_top_managers",
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
                                  "source": "from_top_managers",
                                  "field": "id",
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
                            }
                          ]
                        },
                        "maxDepth": 50
                      }
                    },
                    {
                      "name": "from_two_roots",
                      "recursive": {
                        "seed": {
                          "unionAll": [
                            {
                              "from": {
                                "entity": "schema_with_foreign_keys.employees"
                              },
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
                                }
                              ]
                            },
                            {
                              "from": {
                                "entity": "schema_with_foreign_keys.employees"
                              },
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
                                  "value": "000002be-0000-0000-0000-000000000000"
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
                                }
                              ]
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
                              "subquery": "from_two_roots",
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
                                  "source": "from_two_roots",
                                  "field": "id",
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
                            }
                          ]
                        },
                        "maxDepth": 20
                      }
                    },
                    {
                      "name": "from_earliest_shift",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
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
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_shift_start",
                                "type": {
                                  "name": "time"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ],
                          "pagination": {
                            "take": 1
                          }
                        },
                        "next": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "from_earliest_shift",
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
                                  "source": "from_earliest_shift",
                                  "field": "id",
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
                            }
                          ]
                        },
                        "maxDepth": 1
                      }
                    },
                    {
                      "name": "since_first_order",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.orders"
                          },
                          "aggregate": [
                            {
                              "alias": "day",
                              "expression": {
                                "operator": "min",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "selector": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "since_first_order"
                          },
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "since_first_order",
                              "field": "day",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            },
                            "right": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-04"
                            }
                          },
                          "select": [
                            {
                              "alias": "day",
                              "expression": {
                                "operator": "dateAddDays",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "operand": {
                                  "source": "since_first_order",
                                  "field": "day",
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  }
                                },
                                "days": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 1
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 10
                      }
                    }
                  ],
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
                    },
                    {
                      "kind": "inner",
                      "subquery": "since_first_order",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        },
                        "right": {
                          "source": "since_first_order",
                          "field": "day",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "list": {
                          "subquery": "from_two_roots",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      },
                      {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "list": {
                          "subquery": "from_top_managers",
                          "field": "id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      },
                      {
                        "operator": "not",
                        "type": {
                          "name": "boolean"
                        },
                        "condition": {
                          "operator": "in",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.employees",
                            "field": "employee_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "list": {
                            "subquery": "from_earliest_shift",
                            "field": "id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "employee_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "employee_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "last_login_on",
                      "expression": {
                        "source": "since_first_order",
                        "field": "day",
                        "type": {
                          "name": "date",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RecursiveSeedsOfEveryKindQuery().Value).TextValue
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
                      "name": "employee_id",
                      "type": "uuid"
                    },
                    {
                      "name": "employee_name",
                      "type": "string"
                    },
                    {
                      "name": "last_login_on",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "Grace",
                      "2024-06-01"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "Hank",
                      "2024-06-02"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RecursiveSeedsOfEveryKindQuery().Result).TextValue
        );
    }
}
