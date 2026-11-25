using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record PagesAfterACursorInSubqueriesQueryTests
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
                      "name": "next_orders",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "or",
                          "type": {
                            "name": "boolean"
                          },
                          "conditions": [
                            {
                              "operator": "greaterThan",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2024-06-02T11:00:00Z"
                              }
                            },
                            {
                              "operator": "and",
                              "type": {
                                "name": "boolean"
                              },
                              "conditions": [
                                {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_at",
                                    "type": {
                                      "name": "datetime"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "datetime"
                                    },
                                    "value": "2024-06-02T11:00:00Z"
                                  }
                                },
                                {
                                  "operator": "greaterThan",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000065-0000-0000-0000-000000000000"
                                  }
                                }
                              ]
                            }
                          ]
                        },
                        "select": [
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
                            "alias": "ordered_at",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
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
                        "orderBy": [
                          {
                            "column": "ordered_at"
                          },
                          {
                            "column": "id"
                          }
                        ],
                        "pagination": {
                          "take": 4
                        }
                      }
                    },
                    {
                      "name": "busiest",
                      "query": {
                        "from": {
                          "subquery": "next_orders"
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "next_orders",
                              "field": "user_id",
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
                            "alias": "orders",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ],
                        "orderBy": [
                          {
                            "column": "orders",
                            "direction": "desc"
                          },
                          {
                            "column": "user_id"
                          }
                        ],
                        "pagination": {
                          "take": 2
                        }
                      }
                    }
                  ],
                  "from": {
                    "subquery": "busiest"
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "busiest",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "source": "busiest",
                        "field": "orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PagesAfterACursorInSubqueriesQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PagesAfterACursorInSubqueriesQuery().Result).TextValue
        );
    }
}
