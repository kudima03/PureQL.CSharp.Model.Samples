using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record SortedSubqueriesDistinctAndPagedQueryTests
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
                      "name": "latest",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "select": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "ordered_on",
                            "expression": {
                              "operator": "asDate",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          },
                          {
                            "alias": "total",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        ],
                        "orderBy": [
                          {
                            "column": "ordered_on",
                            "direction": "desc"
                          },
                          {
                            "column": "total",
                            "direction": "desc"
                          },
                          {
                            "column": "id"
                          }
                        ],
                        "distinctBy": [
                          "user_id"
                        ]
                      }
                    },
                    {
                      "name": "top",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "key": "user_id",
                              "type": {
                                "name": "uuid"
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
                        ],
                        "orderBy": [
                          {
                            "column": "revenue",
                            "direction": "desc"
                          },
                          {
                            "column": "user_id"
                          }
                        ],
                        "pagination": {
                          "take": 3
                        }
                      }
                    }
                  ],
                  "from": {
                    "subquery": "latest"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "top",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "latest",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "top",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "latest",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "last_order",
                      "expression": {
                        "source": "latest",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "last_total",
                      "expression": {
                        "source": "latest",
                        "field": "total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "source": "top",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "revenue",
                      "direction": "desc"
                    },
                    {
                      "column": "last_total"
                    },
                    {
                      "column": "user_id"
                    }
                  ],
                  "distinctBy": [
                    "user_id"
                  ],
                  "pagination": {
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(new SortedSubqueriesDistinctAndPagedQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "last_order",
                      "type": "uuid"
                    },
                    {
                      "name": "last_total",
                      "type": "double"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      "375.25"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      "200"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SortedSubqueriesDistinctAndPagedQuery().Result).TextValue
        );
    }
}
