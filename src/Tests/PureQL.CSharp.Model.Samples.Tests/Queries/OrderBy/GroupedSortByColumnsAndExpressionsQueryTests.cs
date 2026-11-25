using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record GroupedSortByColumnsAndExpressionsQueryTests
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
                    },
                    {
                      "alias": "status",
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
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "count",
                      "type": {
                        "name": "integer"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
                    }
                  },
                  "select": [
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
                      "alias": "status",
                      "expression": {
                        "key": "status",
                        "type": {
                          "name": "string"
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
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "latest",
                      "expression": {
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
                    },
                    {
                      "alias": "first_shipped",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
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
                            "operator": "dateAddDays",
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
                            "days": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "month",
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
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
                          },
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          }
                        ]
                      },
                      "direction": "desc"
                    },
                    {
                      "column": "first_shipped"
                    },
                    {
                      "column": "latest",
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": 20
                  }
                }
                """
            ).TextValue,
            new QueryJson(new GroupedSortByColumnsAndExpressionsQuery().Value).TextValue
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
                      "type": "date"
                    },
                    {
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "latest",
                      "type": "datetime"
                    },
                    {
                      "name": "first_shipped",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "pending",
                      "150.5",
                      "2",
                      "2024-06-06T15:00:00Z",
                      ""
                    ],
                    [
                      "2024-06-01",
                      "cancelled",
                      "75.25",
                      "1",
                      "2024-06-04T13:00:00Z",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new GroupedSortByColumnsAndExpressionsQuery().Result
            ).TextValue
        );
    }
}
