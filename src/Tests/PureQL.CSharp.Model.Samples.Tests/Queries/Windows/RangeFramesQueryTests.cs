using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RangeFramesQueryTests
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
                      "alias": "order_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "placed_on",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
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
                      "alias": "spent_on_the_day_and_the_day_before",
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
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "days": 1
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "spent_in_last_12_months",
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
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "startOfMonth",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "months": 11
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "balance_to_date",
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
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_within_25_hours",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
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
                            }
                          ],
                          "range": {
                            "preceding": {
                              "seconds": 90000
                            },
                            "following": {
                              "seconds": 90000
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_of_similar_size",
                      "expression": {
                        "operator": "count",
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
                              }
                            }
                          ],
                          "range": {
                            "preceding": 25,
                            "following": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "25.25"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
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
                """
            ).TextValue,
            new QueryJson(new RangeFramesQuery().Value).TextValue
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
                      "name": "order_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "placed_on",
                      "type": "date"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "spent_on_the_day_and_the_day_before",
                      "type": "double"
                    },
                    {
                      "name": "spent_in_last_12_months",
                      "type": "double"
                    },
                    {
                      "name": "balance_to_date",
                      "type": "double"
                    },
                    {
                      "name": "orders_within_25_hours",
                      "type": "long"
                    },
                    {
                      "name": "orders_of_similar_size",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "100.5",
                      "100.5",
                      "350.5",
                      "100.5",
                      "2",
                      "2"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "2024-06-02",
                      "50",
                      "150.5",
                      "350.5",
                      "150.5",
                      "3",
                      "2"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "2024-06-03",
                      "200",
                      "250",
                      "350.5",
                      "350.5",
                      "2",
                      "1"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "75.25",
                      "75.25",
                      "475.75",
                      "75.25",
                      "2",
                      "3"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2024-06-05",
                      "300",
                      "375.25",
                      "475.75",
                      "375.25",
                      "3",
                      "1"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2024-06-06",
                      "100.5",
                      "400.5",
                      "475.75",
                      "475.75",
                      "2",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RangeFramesQuery().Result).TextValue
        );
    }
}
