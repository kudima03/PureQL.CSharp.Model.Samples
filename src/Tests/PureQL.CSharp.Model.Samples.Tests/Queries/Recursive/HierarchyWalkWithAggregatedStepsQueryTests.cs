using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record HierarchyWalkWithAggregatedStepsQueryTests
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
                      "name": "walk",
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
                              "alias": "level",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "spent",
                              "expression": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0"
                              }
                            },
                            {
                              "alias": "share",
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
                                  "value": "1"
                                }
                              }
                            },
                            {
                              "alias": "orders",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "latest",
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
                              "alias": "vip",
                              "expression": {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": false
                              }
                            },
                            {
                              "alias": "statuses",
                              "expression": {
                                "type": {
                                  "name": "string"
                                },
                                "value": ""
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "walk"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "entity": "schema_with_foreign_keys.employees",
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
                                  "source": "walk",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            },
                            {
                              "kind": "left",
                              "entity": "schema_with_foreign_keys.orders",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_user_id",
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
                            }
                          ],
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "walk",
                              "field": "level",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          "groupBy": [
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
                              "alias": "level",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "walk",
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
                            }
                          ],
                          "having": {
                            "operator": "or",
                            "type": {
                              "name": "boolean"
                            },
                            "conditions": [
                              {
                                "operator": "greaterThan",
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
                                  "value": 1
                                }
                              },
                              {
                                "operator": "any",
                                "type": {
                                  "name": "boolean"
                                },
                                "predicate": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_status",
                                    "type": {
                                      "name": "string",
                                      "nullable": true
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "shipped"
                                  }
                                }
                              }
                            ]
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "key": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "level",
                              "expression": {
                                "key": "level",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            },
                            {
                              "alias": "spent",
                              "expression": {
                                "operator": "sum",
                                "type": {
                                  "name": "decimal"
                                },
                                "selector": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  }
                                }
                              }
                            },
                            {
                              "alias": "share",
                              "expression": {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "values": [
                                  {
                                    "operator": "sum",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "selector": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_total",
                                      "type": {
                                        "name": "decimal",
                                        "nullable": true
                                      }
                                    }
                                  },
                                  {
                                    "operator": "nullIfEqual",
                                    "type": {
                                      "name": "decimal",
                                      "nullable": true
                                    },
                                    "left": {
                                      "operator": "sum",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "selector": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_total",
                                        "type": {
                                          "name": "decimal",
                                          "nullable": true
                                        }
                                      },
                                      "over": "all"
                                    },
                                    "right": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 0
                                    }
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "orders",
                              "expression": {
                                "operator": "countDistinct",
                                "type": {
                                  "name": "integer"
                                },
                                "selector": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid",
                                    "nullable": true
                                  }
                                }
                              }
                            },
                            {
                              "alias": "latest",
                              "expression": {
                                "operator": "max",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                },
                                "selector": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime",
                                    "nullable": true
                                  }
                                }
                              }
                            },
                            {
                              "alias": "vip",
                              "expression": {
                                "operator": "all",
                                "type": {
                                  "name": "boolean"
                                },
                                "predicate": {
                                  "operator": "coalesce",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "values": [
                                    {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_active",
                                      "type": {
                                        "name": "boolean",
                                        "nullable": true
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "boolean"
                                      },
                                      "value": false
                                    }
                                  ]
                                }
                              }
                            },
                            {
                              "alias": "statuses",
                              "expression": {
                                "operator": "concatAll",
                                "type": {
                                  "name": "string"
                                },
                                "selector": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string",
                                    "nullable": true
                                  }
                                },
                                "separator": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": ", "
                                },
                                "orderBy": [
                                  {
                                    "expression": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_status",
                                      "type": {
                                        "name": "string",
                                        "nullable": true
                                      }
                                    }
                                  }
                                ],
                                "distinct": true
                              }
                            }
                          ]
                        },
                        "maxDepth": 6
                      }
                    }
                  ],
                  "from": {
                    "subquery": "walk"
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "walk",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "level",
                      "expression": {
                        "source": "walk",
                        "field": "level",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "spent",
                      "expression": {
                        "source": "walk",
                        "field": "spent",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "share",
                      "expression": {
                        "source": "walk",
                        "field": "share",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "source": "walk",
                        "field": "orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "latest",
                      "expression": {
                        "source": "walk",
                        "field": "latest",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "vip",
                      "expression": {
                        "source": "walk",
                        "field": "vip",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "statuses",
                      "expression": {
                        "source": "walk",
                        "field": "statuses",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new HierarchyWalkWithAggregatedStepsQuery().Value).TextValue
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
                      "name": "level",
                      "type": "long"
                    },
                    {
                      "name": "spent",
                      "type": "double"
                    },
                    {
                      "name": "share",
                      "type": "double"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "latest",
                      "type": "datetime"
                    },
                    {
                      "name": "vip",
                      "type": "bool"
                    },
                    {
                      "name": "statuses",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "0",
                      "0",
                      "1",
                      "0",
                      "2024-06-01T08:30:00Z",
                      "False",
                      ""
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "1",
                      "200",
                      "0.3476749239461104",
                      "1",
                      "2024-06-03T12:00:00Z",
                      "False",
                      "shipped"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "1",
                      "375.25",
                      "0.6523250760538896",
                      "2",
                      "2024-06-05T14:00:00Z",
                      "True",
                      "cancelled, shipped"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new HierarchyWalkWithAggregatedStepsQuery().Result).TextValue
        );
    }
}
