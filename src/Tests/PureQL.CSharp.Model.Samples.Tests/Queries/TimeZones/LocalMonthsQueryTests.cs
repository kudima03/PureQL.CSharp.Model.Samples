using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalMonthsQueryTests
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
                      "field": "placed_at",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    "right": {
                      "operator": "startOfYear",
                      "type": {
                        "name": "datetime"
                      },
                      "operand": {
                        "type": {
                          "name": "datetime"
                        },
                        "value": "2024-06-06T20:00:00Z"
                      },
                      "zone": {
                        "type": {
                          "name": "string"
                        },
                        "value": "Pacific/Pago_Pago"
                      }
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "month_start",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Pacific/Pago_Pago"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "month_start",
                      "expression": {
                        "key": "month_start",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "month",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "key": "month_start",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Pacific/Pago_Pago"
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
                    },
                    {
                      "alias": "business_days",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "startOfDay",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Pacific/Pago_Pago"
                          }
                        }
                      }
                    },
                    {
                      "alias": "revenue_of_the_current_week",
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
                        "predicate": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "operator": "startOfWeek",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-06T20:00:00Z"
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Pacific/Pago_Pago"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_order_at",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "month_start"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalMonthsQuery().Value).TextValue
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
                      "name": "month_start",
                      "type": "datetime"
                    },
                    {
                      "name": "month",
                      "type": "date"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "business_days",
                      "type": "long"
                    },
                    {
                      "name": "revenue_of_the_current_week",
                      "type": "double"
                    },
                    {
                      "name": "first_order_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-05-01T11:00:00Z",
                      "2024-05-01",
                      "1",
                      "100.5",
                      "1",
                      "0",
                      "2024-06-01T10:00:00Z"
                    ],
                    [
                      "2024-06-01T11:00:00Z",
                      "2024-06-01",
                      "5",
                      "725.75",
                      "5",
                      "675.75",
                      "2024-06-02T11:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalMonthsQuery().Result).TextValue
        );
    }
}
