using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record PlacedVersusShippedPerDayQueryTests
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
                      "name": "placed_by_day",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "placed_on",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "placed_on",
                            "expression": {
                              "key": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "alias": "placed",
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
                        ]
                      }
                    },
                    {
                      "name": "shipped_by_day",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
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
                            "value": "shipped"
                          }
                        },
                        "groupBy": [
                          {
                            "alias": "placed_on",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "placed_on",
                            "expression": {
                              "key": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "alias": "shipped",
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
                    }
                  ],
                  "from": {
                    "subquery": "placed_by_day"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "shipped_by_day",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "placed_by_day",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "source": "shipped_by_day",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "placed_on",
                      "expression": {
                        "source": "placed_by_day",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "placed",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "placed_by_day",
                          "field": "placed",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "shipped",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "shipped_by_day",
                              "field": "shipped",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0"
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "not_yet_shipped",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "placed_by_day",
                              "field": "placed",
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
                                  "source": "shipped_by_day",
                                  "field": "shipped",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  }
                                },
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0"
                                }
                              ]
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "placed_by_day",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PlacedVersusShippedPerDayQuery().Value).TextValue
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
                      "name": "placed_on",
                      "type": "date"
                    },
                    {
                      "name": "placed",
                      "type": "double"
                    },
                    {
                      "name": "shipped",
                      "type": "double"
                    },
                    {
                      "name": "not_yet_shipped",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "100.5",
                      "100.5",
                      "0"
                    ],
                    [
                      "2024-06-02",
                      "50",
                      "0",
                      "50"
                    ],
                    [
                      "2024-06-03",
                      "200",
                      "200",
                      "0"
                    ],
                    [
                      "2024-06-04",
                      "75.25",
                      "0",
                      "75.25"
                    ],
                    [
                      "2024-06-05",
                      "300",
                      "300",
                      "0"
                    ],
                    [
                      "2024-06-06",
                      "100.5",
                      "0",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PlacedVersusShippedPerDayQuery().Result).TextValue
        );
    }
}
