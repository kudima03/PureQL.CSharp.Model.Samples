using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record TreeRollUpQueryTests
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
                      "name": "lineage",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "select": [
                            {
                              "alias": "ancestor_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
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
                              "alias": "user_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "distance",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
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
                              "subquery": "lineage",
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
                                  "source": "lineage",
                                  "field": "employee_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            }
                          ],
                          "select": [
                            {
                              "alias": "ancestor_id",
                              "expression": {
                                "source": "lineage",
                                "field": "ancestor_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
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
                              "alias": "user_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "distance",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "lineage",
                                    "field": "distance",
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
                          ]
                        },
                        "maxDepth": 50
                      }
                    }
                  ],
                  "from": {
                    "subquery": "lineage"
                  },
                  "joins": [
                    {
                      "kind": "inner",
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
                          "source": "lineage",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "ancestor_id",
                      "expression": {
                        "source": "lineage",
                        "field": "ancestor_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "employee_id",
                      "expression": {
                        "key": "ancestor_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "network_revenue",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "own_revenue",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "lineage",
                            "field": "distance",
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
                        }
                      }
                    },
                    {
                      "alias": "network_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "deepest_level_with_orders",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "lineage",
                          "field": "distance",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "ancestor_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TreeRollUpQuery().Value).TextValue
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
                      "name": "network_revenue",
                      "type": "double"
                    },
                    {
                      "name": "own_revenue",
                      "type": "double"
                    },
                    {
                      "name": "network_orders",
                      "type": "long"
                    },
                    {
                      "name": "deepest_level_with_orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "826.25",
                      "150.5",
                      "6",
                      "2"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "375.25",
                      "375.25",
                      "2",
                      "0"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "300.5",
                      "200",
                      "2",
                      "1"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "100.5",
                      "100.5",
                      "1",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TreeRollUpQuery().Result).TextValue
        );
    }
}
