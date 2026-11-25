using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record StatisticsFromAGroupedSubqueryQueryTests
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
                      "name": "stats",
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
                            "alias": "median_total",
                            "expression": {
                              "operator": "median",
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
                          },
                          {
                            "alias": "spread",
                            "expression": {
                              "operator": "standardDeviationSample",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
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
                            "alias": "usual_status",
                            "expression": {
                              "operator": "mode",
                              "type": {
                                "name": "string"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
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
                      "kind": "left",
                      "subquery": "stats",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "stats",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "coalesce",
                      "type": {
                        "name": "decimal"
                      },
                      "values": [
                        {
                          "source": "stats",
                          "field": "median_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "50"
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "median_total",
                      "expression": {
                        "source": "stats",
                        "field": "median_total",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "spread",
                      "expression": {
                        "source": "stats",
                        "field": "spread",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "usual_status",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "stats",
                            "field": "usual_status",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "no orders"
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "stats",
                        "field": "median_total",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new StatisticsFromAGroupedSubqueryQuery().Value).TextValue
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
                      "name": "median_total",
                      "type": "double"
                    },
                    {
                      "name": "spread",
                      "type": "double"
                    },
                    {
                      "name": "usual_status",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "200",
                      "",
                      "shipped"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "187.625",
                      "158.92224907167656",
                      "cancelled"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "100.5",
                      "",
                      "pending"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "75.25",
                      "35.70889244992065",
                      "pending"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new StatisticsFromAGroupedSubqueryQuery().Result).TextValue
        );
    }
}
