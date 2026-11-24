using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record AncestorsQueryTests
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
                      "name": "chain",
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
                              "value": "000002c0-0000-0000-0000-000000000000"
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
                              "alias": "manager_id",
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
                              "alias": "name",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
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
                            "subquery": "chain"
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
                                  "source": "chain",
                                  "field": "manager_id",
                                  "type": {
                                    "name": "uuid",
                                    "nullable": true
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
                              "alias": "manager_id",
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
                              "alias": "name",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
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
                                    "source": "chain",
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
                        "maxDepth": 50
                      }
                    }
                  ],
                  "from": {
                    "subquery": "chain"
                  },
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "chain",
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
                      "alias": "level",
                      "expression": {
                        "source": "chain",
                        "field": "level",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "id",
                      "expression": {
                        "source": "chain",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "name",
                      "expression": {
                        "source": "chain",
                        "field": "name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "is_top",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "chain",
                          "field": "manager_id",
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
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "chain",
                        "field": "level",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AncestorsQuery().Value).TextValue
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
                      "name": "level",
                      "type": "long"
                    },
                    {
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "name",
                      "type": "string"
                    },
                    {
                      "name": "is_top",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "000002be-0000-0000-0000-000000000000",
                      "Hank",
                      "False"
                    ],
                    [
                      "2",
                      "000002bd-0000-0000-0000-000000000000",
                      "Grace",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AncestorsQuery().Result).TextValue
        );
    }
}
