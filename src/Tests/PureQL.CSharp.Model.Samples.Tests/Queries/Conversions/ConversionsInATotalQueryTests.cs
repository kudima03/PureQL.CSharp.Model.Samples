using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record ConversionsInATotalQueryTests
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
                  "aggregate": [
                    {
                      "alias": "summary",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": " orders, "
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
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
                            "type": {
                              "name": "string"
                            },
                            "value": " in total"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "last_day",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "max",
                          "type": {
                            "name": "datetime",
                            "nullable": true
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
                    },
                    {
                      "alias": "last_day_label",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "operand": {
                              "operator": "asDate",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "operand": {
                                "operator": "max",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
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
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "no orders"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "first_day_start",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "days_with_orders",
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
                          "right": {
                            "type": {
                              "name": "time"
                            },
                            "value": "12:00:00"
                          }
                        }
                      }
                    },
                    {
                      "alias": "any_paid",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "any",
                          "type": {
                            "name": "boolean"
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
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConversionsInATotalQuery().Value).TextValue
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
                      "name": "summary",
                      "type": "string"
                    },
                    {
                      "name": "last_day",
                      "type": "date"
                    },
                    {
                      "name": "last_day_label",
                      "type": "string"
                    },
                    {
                      "name": "first_day_start",
                      "type": "datetime"
                    },
                    {
                      "name": "days_with_orders",
                      "type": "long"
                    },
                    {
                      "name": "any_paid",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6 orders, 826.25 in total",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-01T00:00:00Z",
                      "5",
                      "true"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConversionsInATotalQuery().Result).TextValue
        );
    }
}
