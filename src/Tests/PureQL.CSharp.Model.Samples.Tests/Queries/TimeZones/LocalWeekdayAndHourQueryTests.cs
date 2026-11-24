using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalWeekdayAndHourQueryTests
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
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-06-02T00:00:00Z"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "weekday",
                      "expression": {
                        "operator": "dayOfWeek",
                        "type": {
                          "name": "integer"
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
                          "value": "America/New_York"
                        }
                      }
                    },
                    {
                      "alias": "hour",
                      "expression": {
                        "operator": "hour",
                        "type": {
                          "name": "integer"
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
                          "value": "America/New_York"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "weekday",
                      "expression": {
                        "key": "weekday",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "hour",
                      "expression": {
                        "key": "hour",
                        "type": {
                          "name": "integer"
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
                      "alias": "at_the_weekend",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "key": "weekday",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        }
                      }
                    },
                    {
                      "alias": "in_business_hours",
                      "expression": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "key": "hour",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 9
                            }
                          },
                          {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "key": "hour",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 18
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "share_of_all_orders",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            },
                            "over": "all"
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "weekday"
                    },
                    {
                      "column": "hour"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalWeekdayAndHourQuery().Value).TextValue
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
                      "name": "weekday",
                      "type": "long"
                    },
                    {
                      "name": "hour",
                      "type": "long"
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
                      "name": "at_the_weekend",
                      "type": "bool"
                    },
                    {
                      "name": "in_business_hours",
                      "type": "bool"
                    },
                    {
                      "name": "share_of_all_orders",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "8",
                      "1",
                      "200",
                      "False",
                      "False",
                      "0.2"
                    ],
                    [
                      "2",
                      "9",
                      "1",
                      "75.25",
                      "False",
                      "True",
                      "0.2"
                    ],
                    [
                      "3",
                      "10",
                      "1",
                      "300",
                      "False",
                      "True",
                      "0.2"
                    ],
                    [
                      "4",
                      "11",
                      "1",
                      "100.5",
                      "False",
                      "True",
                      "0.2"
                    ],
                    [
                      "7",
                      "7",
                      "1",
                      "50",
                      "True",
                      "False",
                      "0.2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalWeekdayAndHourQuery().Result).TextValue
        );
    }
}
