using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record GreatestAndLeastInATotalQueryTests
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
                      "alias": "orders_at_least_one",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "largest_capped",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "max",
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
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "250"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "first_day_in_period",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "values": [
                          {
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
                          },
                          {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-03"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "first_day_or_start",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "operator": "greatest",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "values": [
                              {
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
                              },
                              {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-03"
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-03"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "revenue_floor",
                      "expression": {
                        "operator": "greatest",
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
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GreatestAndLeastInATotalQuery().Value).TextValue
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
                      "name": "orders_at_least_one",
                      "type": "long"
                    },
                    {
                      "name": "largest_capped",
                      "type": "double"
                    },
                    {
                      "name": "first_day_in_period",
                      "type": "date"
                    },
                    {
                      "name": "first_day_or_start",
                      "type": "date"
                    },
                    {
                      "name": "revenue_floor",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6",
                      "250",
                      "2024-06-03",
                      "2024-06-03",
                      "826.25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GreatestAndLeastInATotalQuery().Result).TextValue
        );
    }
}
