using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record DescendantsQueryTests
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
                      "name": "reports",
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
                              "subquery": "reports",
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
                                  "source": "reports",
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
                              "alias": "depth",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "reports",
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
                        "maxDepth": 50
                      }
                    }
                  ],
                  "from": {
                    "subquery": "reports"
                  },
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "reports",
                      "field": "depth",
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
                        "source": "reports",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "name",
                      "expression": {
                        "source": "reports",
                        "field": "name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "depth",
                      "expression": {
                        "source": "reports",
                        "field": "depth",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "reports",
                        "field": "depth",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "reports",
                        "field": "name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "reports",
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
            new QueryJson(new DescendantsQuery().Value).TextValue
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
                      "name": "name",
                      "type": "string"
                    },
                    {
                      "name": "depth",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "Hank",
                      "1"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "Iris",
                      "1"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "Jack",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DescendantsQuery().Result).TextValue
        );
    }
}
