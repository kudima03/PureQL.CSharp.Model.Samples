using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record LastTwelveMonthsPerUserQueryTests
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
                      "name": "monthly",
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
                          },
                          {
                            "alias": "month",
                            "expression": {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "operator": "dateAddMonths",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "months": {
                                  "operator": "if",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "condition": {
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
                                  "then": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 0
                                  },
                                  "else": {
                                    "operator": "if",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "condition": {
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
                                        "value": "pending"
                                      }
                                    },
                                    "then": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": -7
                                    },
                                    "else": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": -13
                                    }
                                  }
                                }
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
                            "alias": "month",
                            "expression": {
                              "key": "month",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "alias": "revenue",
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
                    "subquery": "monthly"
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "monthly",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "month",
                      "expression": {
                        "source": "monthly",
                        "field": "month",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "source": "monthly",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "last_twelve_months",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "monthly",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "monthly",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "monthly",
                                "field": "month",
                                "type": {
                                  "name": "date"
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
                      "alias": "months_with_orders_in_window",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "monthly",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "monthly",
                                "field": "month",
                                "type": {
                                  "name": "date"
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
                      "alias": "year_to_date",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "monthly",
                          "field": "revenue",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "monthly",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            {
                              "operator": "year",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "monthly",
                                "field": "month",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "monthly",
                                "field": "month",
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
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "monthly",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "monthly",
                        "field": "month",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LastTwelveMonthsPerUserQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "month",
                      "type": "date"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "last_twelve_months",
                      "type": "double"
                    },
                    {
                      "name": "months_with_orders_in_window",
                      "type": "long"
                    },
                    {
                      "name": "year_to_date",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2023-11-01",
                      "50",
                      "50",
                      "1",
                      "50"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "100.5",
                      "150.5",
                      "2",
                      "100.5"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "200",
                      "200",
                      "1",
                      "200"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2023-05-01",
                      "75.25",
                      "75.25",
                      "1",
                      "75.25"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "300",
                      "300",
                      "1",
                      "300"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2023-11-01",
                      "100.5",
                      "100.5",
                      "1",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LastTwelveMonthsPerUserQuery().Result).TextValue
        );
    }
}
