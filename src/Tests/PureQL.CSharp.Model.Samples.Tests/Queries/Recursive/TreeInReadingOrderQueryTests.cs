using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record TreeInReadingOrderQueryTests
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
                              "alias": "depth",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "path",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
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
                              "alias": "depth",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "tree",
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
                            },
                            {
                              "alias": "path",
                              "expression": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "source": "tree",
                                    "field": "path",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": " / "
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.employees",
                                    "field": "employee_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "operator": "repeat",
                                    "type": {
                                      "name": "string"
                                    },
                                    "operand": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "  "
                                    },
                                    "count": {
                                      "operator": "add",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "values": [
                                        {
                                          "source": "tree",
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
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.employees",
                                    "field": "employee_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "maxDepth": 10
                      }
                    }
                  ],
                  "from": {
                    "subquery": "tree"
                  },
                  "select": [
                    {
                      "alias": "label",
                      "expression": {
                        "source": "tree",
                        "field": "label",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "depth",
                      "expression": {
                        "source": "tree",
                        "field": "depth",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "path",
                      "expression": {
                        "source": "tree",
                        "field": "path",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "tree",
                        "field": "path",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
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
            new QueryJson(new TreeInReadingOrderQuery().Value).TextValue
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
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "depth",
                      "type": "long"
                    },
                    {
                      "name": "path",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Grace",
                      "0",
                      "Grace"
                    ],
                    [
                      "  Hank",
                      "1",
                      "Grace / Hank"
                    ],
                    [
                      "    Jack",
                      "2",
                      "Grace / Hank / Jack"
                    ],
                    [
                      "  Iris",
                      "1",
                      "Grace / Iris"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TreeInReadingOrderQuery().Result).TextValue
        );
    }
}
