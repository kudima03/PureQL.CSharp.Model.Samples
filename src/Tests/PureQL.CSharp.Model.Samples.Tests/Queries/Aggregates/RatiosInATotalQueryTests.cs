using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record RatiosInATotalQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-07"
                    }
                  },
                  "aggregate": [
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
                      "alias": "average_total",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
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
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "operator": "count",
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
                    },
                    {
                      "alias": "paid_share",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
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
                                "value": "shipped"
                              }
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "operator": "count",
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
                    },
                    {
                      "alias": "paid_share_or_zero",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "divide",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "values": [
                              {
                                "operator": "count",
                                "type": {
                                  "name": "integer"
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
                                    "value": "shipped"
                                  }
                                }
                              },
                              {
                                "operator": "nullIfEqual",
                                "type": {
                                  "name": "integer",
                                  "nullable": true
                                },
                                "left": {
                                  "operator": "count",
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
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "orders_or_null",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "count",
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RatiosInATotalQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "average_total",
                      "type": "double"
                    },
                    {
                      "name": "paid_share",
                      "type": "double"
                    },
                    {
                      "name": "paid_share_or_zero",
                      "type": "double"
                    },
                    {
                      "name": "orders_or_null",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0",
                      "",
                      "",
                      "0",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RatiosInATotalQuery().Result).TextValue
        );
    }
}
