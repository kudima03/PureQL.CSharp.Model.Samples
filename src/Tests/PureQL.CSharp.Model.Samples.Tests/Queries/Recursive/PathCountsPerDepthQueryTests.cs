using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record PathCountsPerDepthQueryTests
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
                      "name": "links",
                      "rows": [
                        [
                          {
                            "alias": "source_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bd-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "target_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002be-0000-0000-0000-000000000000"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "source_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bd-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "target_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bf-0000-0000-0000-000000000000"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "source_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002be-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "target_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002c0-0000-0000-0000-000000000000"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "source_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bf-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "target_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002c0-0000-0000-0000-000000000000"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "reached",
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
                              "alias": "depth",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "paths",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
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
                              "subquery": "links",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "links",
                                  "field": "target_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            },
                            {
                              "kind": "inner",
                              "subquery": "reached",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "reached",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "source": "links",
                                  "field": "source_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            }
                          ],
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
                            }
                          ],
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
                              "alias": "depth",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "operator": "max",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "selector": {
                                      "source": "reached",
                                      "field": "depth",
                                      "type": {
                                        "name": "integer"
                                      }
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
                              "alias": "paths",
                              "expression": {
                                "operator": "sum",
                                "type": {
                                  "name": "integer"
                                },
                                "selector": {
                                  "source": "reached",
                                  "field": "paths",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 20
                      }
                    }
                  ],
                  "from": {
                    "subquery": "reached"
                  },
                  "groupBy": [
                    {
                      "alias": "depth",
                      "expression": {
                        "source": "reached",
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
                      "alias": "users",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "paths",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "reached",
                          "field": "paths",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "depth"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PathCountsPerDepthQuery().Value).TextValue
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
                      "name": "depth",
                      "type": "long"
                    },
                    {
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "paths",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0",
                      "1",
                      "1"
                    ],
                    [
                      "1",
                      "2",
                      "2"
                    ],
                    [
                      "2",
                      "1",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PathCountsPerDepthQuery().Result).TextValue
        );
    }
}
