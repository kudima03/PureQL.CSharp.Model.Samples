using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundHalfToEvenInATotalQueryTests
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
                    "operator": "equal",
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
                      "operator": "round",
                      "type": {
                        "name": "decimal"
                      },
                      "operand": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "digits": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 1
                      },
                      "mode": "halfEven"
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "revenue_to_thousands",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
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
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -3
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "average_to_cents",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "median_whole",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "median",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "sum_of_rounded",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "mode": "halfEven"
                        }
                      }
                    },
                    {
                      "alias": "largest_rounded",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "mode": "halfEven"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundHalfToEvenInATotalQuery().Value).TextValue
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
                      "name": "revenue_to_thousands",
                      "type": "double"
                    },
                    {
                      "name": "average_to_cents",
                      "type": "double"
                    },
                    {
                      "name": "median_whole",
                      "type": "long"
                    },
                    {
                      "name": "sum_of_rounded",
                      "type": "long"
                    },
                    {
                      "name": "largest_rounded",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1000",
                      "150.2",
                      "100",
                      "750",
                      "300"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundHalfToEvenInATotalQuery().Result).TextValue
        );
    }
}
