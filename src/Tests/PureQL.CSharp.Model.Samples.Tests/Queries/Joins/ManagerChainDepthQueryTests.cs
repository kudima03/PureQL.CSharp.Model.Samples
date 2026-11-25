using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record ManagerChainDepthQueryTests
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
                    "alias": "e"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "manager",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "e",
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
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "grand_manager",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "manager",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "grand_manager",
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
                      "alias": "employee",
                      "expression": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "manager",
                      "expression": {
                        "source": "manager",
                        "field": "employee_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "grand_manager",
                      "expression": {
                        "source": "grand_manager",
                        "field": "employee_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "chain_depth",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "integer"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "manager",
                            "field": "employee_id",
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
                        "then": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "else": {
                          "operator": "if",
                          "type": {
                            "name": "integer"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "grand_manager",
                              "field": "employee_id",
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
                          "then": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "else": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ManagerChainDepthQuery().Value).TextValue
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
                      "name": "employee",
                      "type": "string"
                    },
                    {
                      "name": "manager",
                      "type": "string"
                    },
                    {
                      "name": "grand_manager",
                      "type": "string"
                    },
                    {
                      "name": "chain_depth",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Grace",
                      "",
                      "",
                      "0"
                    ],
                    [
                      "Hank",
                      "Grace",
                      "",
                      "1"
                    ],
                    [
                      "Iris",
                      "Grace",
                      "",
                      "1"
                    ],
                    [
                      "Jack",
                      "Hank",
                      "Grace",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ManagerChainDepthQuery().Result).TextValue
        );
    }
}
