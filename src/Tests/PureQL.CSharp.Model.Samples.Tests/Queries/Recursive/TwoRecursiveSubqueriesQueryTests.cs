using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record TwoRecursiveSubqueriesQueryTests
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
                      "name": "team",
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
                            },
                            {
                              "alias": "referrer_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_manager_id",
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
                              "subquery": "team",
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
                                  "source": "team",
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
                            },
                            {
                              "alias": "referrer_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_manager_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 50
                      }
                    },
                    {
                      "name": "upline",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "team"
                          },
                          "where": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "team",
                              "field": "id",
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
                                "source": "team",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "referrer_id",
                              "expression": {
                                "source": "team",
                                "field": "referrer_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
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
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "upline"
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
                                  "field": "employee_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "source": "upline",
                                  "field": "referrer_id",
                                  "type": {
                                    "name": "uuid",
                                    "nullable": true
                                  }
                                }
                              }
                            }
                          ],
                          "where": {
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
                                "subquery": "team",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
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
                              "alias": "referrer_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_manager_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "alias": "level",
                              "expression": {
                                "operator": "subtract",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "upline",
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
                          ]
                        },
                        "maxDepth": 3
                      }
                    }
                  ],
                  "unionAll": [
                    {
                      "from": {
                        "subquery": "team"
                      },
                      "select": [
                        {
                          "alias": "id",
                          "expression": {
                            "source": "team",
                            "field": "id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        {
                          "alias": "side",
                          "expression": {
                            "type": {
                              "name": "string"
                            },
                            "value": "below"
                          }
                        }
                      ]
                    },
                    {
                      "from": {
                        "subquery": "upline"
                      },
                      "where": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "upline",
                          "field": "level",
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
                      "select": [
                        {
                          "alias": "id",
                          "expression": {
                            "source": "upline",
                            "field": "id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        {
                          "alias": "side",
                          "expression": {
                            "type": {
                              "name": "string"
                            },
                            "value": "above"
                          }
                        }
                      ]
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TwoRecursiveSubqueriesQuery().Value).TextValue
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
                      "name": "side",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "below"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "below"
                    ],
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "above"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TwoRecursiveSubqueriesQuery().Result).TextValue
        );
    }
}
