using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalBusinessDayQueryTests
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
                      "operator": "asDate",
                      "type": {
                        "name": "date"
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
                        "value": "Asia/Tokyo"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-03"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "business_day",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
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
                          "value": "Asia/Tokyo"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "business_day",
                      "expression": {
                        "key": "business_day",
                        "type": {
                          "name": "date"
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
                      "alias": "orders_after_six_in_the_evening",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "asTime",
                            "type": {
                              "name": "time"
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
                              "value": "Asia/Tokyo"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "time"
                            },
                            "value": "18:00:00"
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_order_at",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "time"
                        },
                        "selector": {
                          "operator": "asTime",
                          "type": {
                            "name": "time"
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
                            "value": "Asia/Tokyo"
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_on_another_utc_day",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          "right": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
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
                              "value": "Asia/Tokyo"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "business_day"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalBusinessDayQuery().Value).TextValue
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
                      "name": "business_day",
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
                      "name": "orders_after_six_in_the_evening",
                      "type": "long"
                    },
                    {
                      "name": "first_order_at",
                      "type": "time"
                    },
                    {
                      "name": "orders_on_another_utc_day",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-03",
                      "1",
                      "200",
                      "1",
                      "21:00:00",
                      "0"
                    ],
                    [
                      "2024-06-04",
                      "1",
                      "75.25",
                      "1",
                      "22:00:00",
                      "0"
                    ],
                    [
                      "2024-06-05",
                      "1",
                      "300",
                      "1",
                      "23:00:00",
                      "0"
                    ],
                    [
                      "2024-06-07",
                      "1",
                      "100.5",
                      "0",
                      "00:00:00",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalBusinessDayQuery().Result).TextValue
        );
    }
}
