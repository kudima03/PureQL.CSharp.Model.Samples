using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RoundHalfToEvenOverWindowsQueryTests
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
                      "alias": "running_half_total_to_cents",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "operator": "multiply",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0.5"
                              }
                            ]
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
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
                                }
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
                            "frame": {
                              "preceding": "all"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "share_in_percent",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal",
                            "nullable": true
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
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "operator": "nullIfEqual",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "left": {
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
                                "name": "integer"
                              },
                              "value": 100
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "previous_rounded",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "mode": "halfEven"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rank_by_rounded_total",
                      "expression": {
                        "operator": "rank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "round",
                                "type": {
                                  "name": "decimal"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "digits": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": -1
                                },
                                "mode": "halfEven"
                              },
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundHalfToEvenOverWindowsQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "running_half_total_to_cents",
                      "type": "double"
                    },
                    {
                      "name": "share_in_percent",
                      "type": "double"
                    },
                    {
                      "name": "previous_rounded",
                      "type": "long"
                    },
                    {
                      "name": "rank_by_rounded_total",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "75.25",
                      "6.1",
                      "100",
                      "6"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "37.62",
                      "9.1",
                      "200",
                      "5"
                    ],
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "50.25",
                      "12.2",
                      "",
                      "3"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "50.25",
                      "12.2",
                      "300",
                      "3"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "100",
                      "24.2",
                      "50",
                      "2"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "187.62",
                      "36.3",
                      "75",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundHalfToEvenOverWindowsQuery().Result).TextValue
        );
    }
}
