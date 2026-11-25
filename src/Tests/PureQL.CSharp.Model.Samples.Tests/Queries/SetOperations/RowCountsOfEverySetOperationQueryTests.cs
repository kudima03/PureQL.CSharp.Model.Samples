using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record RowCountsOfEverySetOperationQueryTests
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
                      "name": "all_rows",
                      "query": {
                        "unionAll": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_active",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "pending"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "cancelled"
                                  }
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "name": "distinct_rows",
                      "query": {
                        "union": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_active",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "pending"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "cancelled"
                                  }
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "name": "shared_rows",
                      "query": {
                        "intersect": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_active",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "pending"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "cancelled"
                                  }
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "name": "order_only_rows",
                      "query": {
                        "except": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "status",
                                "expression": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_active",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "pending"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "cancelled"
                                  }
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "name": "counts",
                      "query": {
                        "unionAll": [
                          {
                            "from": {
                              "subquery": "all_rows"
                            },
                            "aggregate": [
                              {
                                "alias": "operation",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "unionAll"
                                }
                              },
                              {
                                "alias": "rows",
                                "expression": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "subquery": "distinct_rows"
                            },
                            "aggregate": [
                              {
                                "alias": "operation",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "union"
                                }
                              },
                              {
                                "alias": "rows",
                                "expression": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "subquery": "shared_rows"
                            },
                            "aggregate": [
                              {
                                "alias": "operation",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "intersect"
                                }
                              },
                              {
                                "alias": "rows",
                                "expression": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "subquery": "order_only_rows"
                            },
                            "aggregate": [
                              {
                                "alias": "operation",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "except"
                                }
                              },
                              {
                                "alias": "rows",
                                "expression": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "counts"
                  },
                  "select": [
                    {
                      "alias": "operation",
                      "expression": {
                        "source": "counts",
                        "field": "operation",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "rows",
                      "expression": {
                        "source": "counts",
                        "field": "rows",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "counts",
                        "field": "operation",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RowCountsOfEverySetOperationQuery().Value).TextValue
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
                      "name": "operation",
                      "type": "string"
                    },
                    {
                      "name": "rows",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "except",
                      "1"
                    ],
                    [
                      "intersect",
                      "2"
                    ],
                    [
                      "union",
                      "3"
                    ],
                    [
                      "unionAll",
                      "12"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RowCountsOfEverySetOperationQuery().Result).TextValue
        );
    }
}
