using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record WindowFunctionsByKindQueryTests
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
                      "alias": "running",
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
                      }
                    },
                    {
                      "alias": "last_month",
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
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "months": 1
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "hour_around",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
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
                              },
                              "direction": "desc"
                            }
                          ],
                          "range": {
                            "preceding": {
                              "seconds": 100000
                            },
                            "following": {
                              "seconds": 100000
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "user_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "position",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rank",
                      "expression": {
                        "operator": "rank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "dense_rank",
                      "expression": {
                        "operator": "denseRank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "percent_rank",
                      "expression": {
                        "operator": "percentRank",
                        "type": {
                          "name": "decimal"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "distribution",
                      "expression": {
                        "operator": "cumulativeDistribution",
                        "type": {
                          "name": "decimal"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "quartile",
                      "expression": {
                        "operator": "ntile",
                        "type": {
                          "name": "integer"
                        },
                        "buckets": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 4
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
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
                      "alias": "second_next",
                      "expression": {
                        "operator": "next",
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
                        "offset": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "default": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                          ]
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WindowFunctionsByKindQuery().Value).TextValue
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
                      "name": "running",
                      "type": "double"
                    },
                    {
                      "name": "last_month",
                      "type": "double"
                    },
                    {
                      "name": "hour_around",
                      "type": "double"
                    },
                    {
                      "name": "user_orders",
                      "type": "long"
                    },
                    {
                      "name": "position",
                      "type": "long"
                    },
                    {
                      "name": "rank",
                      "type": "long"
                    },
                    {
                      "name": "dense_rank",
                      "type": "long"
                    },
                    {
                      "name": "percent_rank",
                      "type": "double"
                    },
                    {
                      "name": "distribution",
                      "type": "double"
                    },
                    {
                      "name": "quartile",
                      "type": "long"
                    },
                    {
                      "name": "second_next",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "100.5",
                      "100.5",
                      "100.5",
                      "2",
                      "1",
                      "3",
                      "3",
                      "0.4",
                      "0.16666666666666666",
                      "2",
                      "200"
                    ],
                    [
                      "150.5",
                      "150.5",
                      "100.5",
                      "2",
                      "2",
                      "6",
                      "2",
                      "0",
                      "0.3333333333333333",
                      "1",
                      "0"
                    ],
                    [
                      "200",
                      "350.5",
                      "200",
                      "1",
                      "1",
                      "2",
                      "2",
                      "0.8",
                      "0.5",
                      "3",
                      "0"
                    ],
                    [
                      "75.25",
                      "425.75",
                      "300",
                      "2",
                      "1",
                      "5",
                      "1",
                      "0.2",
                      "0.6666666666666666",
                      "1",
                      "100.5"
                    ],
                    [
                      "375.25",
                      "725.75",
                      "300",
                      "2",
                      "2",
                      "1",
                      "1",
                      "1",
                      "0.8333333333333334",
                      "4",
                      "0"
                    ],
                    [
                      "100.5",
                      "826.25",
                      "100.5",
                      "1",
                      "1",
                      "3",
                      "1",
                      "0.4",
                      "1",
                      "2",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WindowFunctionsByKindQuery().Result).TextValue
        );
    }
}
