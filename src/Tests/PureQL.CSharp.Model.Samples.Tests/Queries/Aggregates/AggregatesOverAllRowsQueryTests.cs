using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AggregatesOverAllRowsQueryTests
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
                      "alias": "orders",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "count"
                      }
                    },
                    {
                      "alias": "revenue",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "sum",
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
                      "alias": "smallest",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "min",
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
                      "alias": "latest",
                      "type": {
                        "name": "datetime",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "max",
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
                      "alias": "average",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "average",
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
                      "alias": "any_cancelled",
                      "type": {
                        "name": "boolean"
                      },
                      "expression": {
                        "operator": "any",
                        "predicate": {
                          "operator": "equal",
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
                            "value": "cancelled"
                          }
                        }
                      }
                    },
                    {
                      "alias": "all_positive",
                      "type": {
                        "name": "boolean"
                      },
                      "expression": {
                        "operator": "all",
                        "predicate": {
                          "operator": "greaterThan",
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AggregatesOverAllRowsQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "smallest",
                      "type": "double"
                    },
                    {
                      "name": "latest",
                      "type": "datetime"
                    },
                    {
                      "name": "average",
                      "type": "double"
                    },
                    {
                      "name": "any_cancelled",
                      "type": "bool"
                    },
                    {
                      "name": "all_positive",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6",
                      "826.25",
                      "50",
                      "2024-06-06T15:00:00",
                      "137.70833333333334",
                      "True",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AggregatesOverAllRowsQuery().Result).TextValue
        );
    }
}
