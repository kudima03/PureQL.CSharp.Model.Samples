using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record TotalQueryJoinedOnTrueQueryTests
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
                      "name": "paid_stats",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
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
                            "value": "pending"
                          }
                        },
                        "aggregate": [
                          {
                            "alias": "paid_orders",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "largest",
                            "expression": {
                              "operator": "max",
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
                            "alias": "any_refunded",
                            "expression": {
                              "operator": "any",
                              "type": {
                                "name": "boolean"
                              },
                              "predicate": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
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
                                  "value": "cancelled"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "paid_stats",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    }
                  ],
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
                      "alias": "share_of_largest",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "coalesce",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "paid_stats",
                                "field": "largest",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
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
                        ]
                      }
                    },
                    {
                      "alias": "smallest_overall",
                      "expression": {
                        "operator": "min",
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
                      "alias": "paid_orders",
                      "expression": {
                        "source": "paid_stats",
                        "field": "paid_orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TotalQueryJoinedOnTrueQuery().Value).TextValue
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
                      "name": "share_of_largest",
                      "type": "double"
                    },
                    {
                      "name": "smallest_overall",
                      "type": "double"
                    },
                    {
                      "name": "paid_orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "0.335",
                      "50",
                      "4"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "0.16666666666666666",
                      "50",
                      "4"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "0.6666666666666666",
                      "50",
                      "4"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "0.25083333333333335",
                      "50",
                      "4"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "1",
                      "50",
                      "4"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "0.335",
                      "50",
                      "4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TotalQueryJoinedOnTrueQuery().Result).TextValue
        );
    }
}
