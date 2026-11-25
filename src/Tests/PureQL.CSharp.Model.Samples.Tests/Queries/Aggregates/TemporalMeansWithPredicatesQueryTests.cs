using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record TemporalMeansWithPredicatesQueryTests
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
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "decimal"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "60"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "order_status",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "lessThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "median",
                      "type": {
                        "name": "date"
                      },
                      "selector": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-05"
                    }
                  },
                  "select": [
                    {
                      "alias": "order_status",
                      "expression": {
                        "key": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "average_paid_date",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "predicate": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "100"
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_date_of_all",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": "all"
                      }
                    },
                    {
                      "alias": "median_placed_at",
                      "expression": {
                        "operator": "median",
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
                    },
                    {
                      "alias": "first_decile_time",
                      "expression": {
                        "operator": "percentileContinuous",
                        "type": {
                          "name": "time",
                          "nullable": true
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
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.1"
                        },
                        "predicate": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "100"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TemporalMeansWithPredicatesQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "average_paid_date",
                      "type": "date"
                    },
                    {
                      "name": "average_date_of_all",
                      "type": "date"
                    },
                    {
                      "name": "median_placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "first_decile_time",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped",
                      "2024-06-03",
                      "2024-06-04",
                      "2024-06-03T12:00:00Z",
                      "10:24:00"
                    ],
                    [
                      "cancelled",
                      "",
                      "2024-06-04",
                      "2024-06-04T13:00:00Z",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TemporalMeansWithPredicatesQuery().Result).TextValue
        );
    }
}
