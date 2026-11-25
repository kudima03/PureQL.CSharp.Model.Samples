using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record GroupsWithATotalsRowQueryTests
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
                      "name": "report",
                      "query": {
                        "unionAll": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "groupBy": [
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
                            ],
                            "select": [
                              {
                                "alias": "label",
                                "expression": {
                                  "key": "status",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              },
                              {
                                "alias": "orders",
                                "expression": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              },
                              {
                                "alias": "revenue",
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
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "aggregate": [
                              {
                                "alias": "label",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "all"
                                }
                              },
                              {
                                "alias": "orders",
                                "expression": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              },
                              {
                                "alias": "revenue",
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
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "report"
                  },
                  "select": [
                    {
                      "alias": "label",
                      "expression": {
                        "source": "report",
                        "field": "label",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "source": "report",
                        "field": "orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "source": "report",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "average",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "report",
                            "field": "revenue",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "source": "report",
                              "field": "orders",
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
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "report",
                          "field": "label",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "all"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "report",
                        "field": "label",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupsWithATotalsRowQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "average",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "1",
                      "75.25",
                      "75.25"
                    ],
                    [
                      "pending",
                      "2",
                      "150.5",
                      "75.25"
                    ],
                    [
                      "shipped",
                      "3",
                      "600.5",
                      "200.16666666666666"
                    ],
                    [
                      "all",
                      "6",
                      "826.25",
                      "137.70833333333334"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupsWithATotalsRowQuery().Result).TextValue
        );
    }
}
