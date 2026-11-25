using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RankingQueryTests
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
                      "alias": "order_user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
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
                    },
                    {
                      "alias": "position_for_user",
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
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rank_for_user",
                      "expression": {
                        "operator": "rank",
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
                      "alias": "dense_rank_for_user",
                      "expression": {
                        "operator": "denseRank",
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
                      "alias": "rank_of_all",
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
                      "alias": "share_of_other_orders_above",
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
                              },
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "share_of_orders_at_or_above",
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "is_user_largest",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "rank",
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
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "direction": "desc"
                              }
                            ]
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RankingQuery().Value).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "position_for_user",
                      "type": "long"
                    },
                    {
                      "name": "rank_for_user",
                      "type": "long"
                    },
                    {
                      "name": "dense_rank_for_user",
                      "type": "long"
                    },
                    {
                      "name": "rank_of_all",
                      "type": "long"
                    },
                    {
                      "name": "share_of_other_orders_above",
                      "type": "double"
                    },
                    {
                      "name": "share_of_orders_at_or_above",
                      "type": "double"
                    },
                    {
                      "name": "quartile",
                      "type": "long"
                    },
                    {
                      "name": "is_user_largest",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "100.5",
                      "1",
                      "1",
                      "1",
                      "3",
                      "0.4",
                      "0.6666666666666666",
                      "2",
                      "True"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "50",
                      "2",
                      "2",
                      "2",
                      "6",
                      "1",
                      "1",
                      "4",
                      "False"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "200",
                      "1",
                      "1",
                      "1",
                      "2",
                      "0.2",
                      "0.3333333333333333",
                      "1",
                      "True"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "300",
                      "1",
                      "1",
                      "1",
                      "1",
                      "0",
                      "0.16666666666666666",
                      "1",
                      "True"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "75.25",
                      "2",
                      "2",
                      "2",
                      "5",
                      "0.8",
                      "0.8333333333333334",
                      "3",
                      "False"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "00000004-0000-0000-0000-000000000000",
                      "100.5",
                      "1",
                      "1",
                      "1",
                      "3",
                      "0.4",
                      "0.6666666666666666",
                      "2",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RankingQuery().Result).TextValue
        );
    }
}
