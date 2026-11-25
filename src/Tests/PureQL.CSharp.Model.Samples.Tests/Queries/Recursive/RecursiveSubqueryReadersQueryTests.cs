using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record RecursiveSubqueryReadersQueryTests
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
                      "name": "referrals",
                      "recursive": {
                        "seed": {
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
                            },
                            {
                              "alias": "first_name",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "alias": "depth",
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
                              "subquery": "referrals",
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
                                  "source": "referrals",
                                  "field": "id",
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
                              "source": "referrals",
                              "field": "depth",
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
                              "alias": "first_name",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "alias": "depth",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "referrals",
                                    "field": "depth",
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
                        "maxDepth": 1
                      }
                    },
                    {
                      "name": "levels",
                      "query": {
                        "from": {
                          "subquery": "referrals"
                        },
                        "groupBy": [
                          {
                            "alias": "depth",
                            "expression": {
                              "source": "referrals",
                              "field": "depth",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "depth",
                            "expression": {
                              "key": "depth",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "members",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "deepest",
                      "query": {
                        "from": {
                          "subquery": "referrals"
                        },
                        "aggregate": [
                          {
                            "alias": "depth",
                            "expression": {
                              "operator": "max",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "selector": {
                                "source": "referrals",
                                "field": "depth",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "referrals"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "levels",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "levels",
                          "field": "depth",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "source": "referrals",
                          "field": "depth",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
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
                          "field": "employee_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "referrals",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "in",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "source": "referrals",
                      "field": "depth",
                      "type": {
                        "name": "integer"
                      }
                    },
                    "list": {
                      "subquery": "deepest",
                      "field": "depth",
                      "type": {
                        "name": "integer",
                        "nullable": true
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "referrals",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "first_name",
                      "expression": {
                        "source": "referrals",
                        "field": "first_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "shift_start",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "members_at_this_depth",
                      "expression": {
                        "source": "levels",
                        "field": "members",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "position_in_level",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "referrals",
                              "field": "depth",
                              "type": {
                                "name": "integer"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "referrals",
                                "field": "first_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "referrals",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "share_of_tree",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "referrals",
                        "field": "first_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "referrals",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "distinctBy": [
                    "id"
                  ],
                  "pagination": {
                    "take": 100
                  }
                }
                """
            ).TextValue,
            new QueryJson(new RecursiveSubqueryReadersQuery().Value).TextValue
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
                      "name": "first_name",
                      "type": "string"
                    },
                    {
                      "name": "shift_start",
                      "type": "time"
                    },
                    {
                      "name": "members_at_this_depth",
                      "type": "long"
                    },
                    {
                      "name": "position_in_level",
                      "type": "long"
                    },
                    {
                      "name": "share_of_tree",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "Hank",
                      "10:00:00",
                      "2",
                      "1",
                      "0.5"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "Iris",
                      "11:30:00",
                      "2",
                      "2",
                      "0.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RecursiveSubqueryReadersQuery().Result).TextValue
        );
    }
}
