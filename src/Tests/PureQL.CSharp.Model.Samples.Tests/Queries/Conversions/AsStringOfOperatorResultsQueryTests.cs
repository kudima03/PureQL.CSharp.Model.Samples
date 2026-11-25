using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record AsStringOfOperatorResultsQueryTests
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
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "alias": "shipped",
                      "on": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "shipped",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "shipped",
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
                        ]
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "month",
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
                  "select": [
                    {
                      "alias": "month",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "key": "month",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
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
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
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
                      }
                    },
                    {
                      "alias": "latest",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "max",
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
                    },
                    {
                      "alias": "last_shipped",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "max",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "selector": {
                            "source": "shipped",
                            "field": "placed_on",
                            "type": {
                              "name": "date",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "stamps",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
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
                                "operator": "microsecond",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "operator": "datetimeAddSeconds",
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
                                  "seconds": {
                                    "operator": "divide",
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
                                          "name": "integer"
                                        },
                                        "value": 100
                                      }
                                    ]
                                  }
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "@"
                            },
                            {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "startOfSecond",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "operator": "datetimeAddSeconds",
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
                                  "seconds": {
                                    "operator": "divide",
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
                                          "name": "integer"
                                        },
                                        "value": 100
                                      }
                                    ]
                                  }
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "/"
                            },
                            {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "divide",
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
                                      "name": "integer"
                                    },
                                    "value": 3
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AsStringOfOperatorResultsQuery().Value).TextValue
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
                      "name": "month",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "string"
                    },
                    {
                      "name": "revenue",
                      "type": "string"
                    },
                    {
                      "name": "latest",
                      "type": "string"
                    },
                    {
                      "name": "last_shipped",
                      "type": "string"
                    },
                    {
                      "name": "stamps",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "6",
                      "826.25",
                      "2024-06-06T15:00:00Z",
                      "2024-06-05",
                      "5000@2024-06-01T10:00:01Z/33.5, 500000@2024-06-02T11:00:00Z/16.66666666666666666666666667, 0@2024-06-03T12:00:02Z/66.66666666666666666666666667, 752500@2024-06-04T13:00:00Z/25.08333333333333333333333333, 0@2024-06-05T14:00:03Z/100, 5000@2024-06-06T15:00:01Z/33.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AsStringOfOperatorResultsQuery().Result).TextValue
        );
    }
}
