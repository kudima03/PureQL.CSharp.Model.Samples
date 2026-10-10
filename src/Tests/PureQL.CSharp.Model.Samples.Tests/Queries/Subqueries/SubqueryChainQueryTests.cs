using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record SubqueryChainQueryTests
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
                      "name": "user_totals",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "equal",
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        },
                        "groupBy": [
                          {
                            "type": {
                              "name": "uuid"
                            },
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
                            "type": {
                              "name": "uuid"
                            },
                            "expression": {
                              "key": 0,
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "total",
                            "type": {
                              "name": "decimal"
                            },
                            "expression": {
                              "operator": "sum",
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "alias": "orders",
                            "type": {
                              "name": "integer"
                            },
                            "expression": {
                              "operator": "count"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "big_spenders",
                      "query": {
                        "from": {
                          "subquery": "user_totals"
                        },
                        "where": {
                          "operator": "greaterThan",
                          "left": {
                            "source": "user_totals",
                            "field": "total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 150
                          }
                        },
                        "select": [
                          {
                            "alias": "user_id",
                            "type": {
                              "name": "uuid"
                            },
                            "expression": {
                              "source": "user_totals",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "total",
                            "type": {
                              "name": "decimal"
                            },
                            "expression": {
                              "source": "user_totals",
                              "field": "total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "subquery": "big_spenders",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "big_spenders",
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
                      "alias": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "big_spenders",
                        "field": "total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "big_spenders",
                        "field": "total",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SubqueryChainQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Cara",
                      "300"
                    ],
                    [
                      "Bob",
                      "200"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SubqueryChainQuery().Result).TextValue
        );
    }
}
