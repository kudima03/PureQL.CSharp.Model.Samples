using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record PaginationFormsInSubqueriesQueryTests
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
                            "alias": "order_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "order_total",
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
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "direction": "desc"
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "pagination": {
                          "take": 5
                        }
                      }
                    },
                    {
                      "name": "older",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "select": [
                          {
                            "alias": "order_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "order_total",
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
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "direction": "desc"
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "pagination": {
                          "skip": 3
                        }
                      }
                    },
                    {
                      "name": "top_users",
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
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "direction": "desc"
                          },
                          {
                            "expression": {
                              "key": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "pagination": {
                          "skip": {
                            "param": "skipped_users",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "take": 10
                        }
                      }
                    }
                  ],
                  "from": {
                    "subquery": "latest"
                  },
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
                        "source": "latest",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "list": {
                        "subquery": "older",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "latest",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_total",
                      "expression": {
                        "source": "latest",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "latest",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "latest",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "distinctBy": [
                    "order_id"
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": {
                      "param": "page_size",
                      "type": {
                        "name": "integer"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new PaginationFormsInSubqueriesQuery().Value).TextValue
        );
    }
}
