using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AggregateWithPredicateNextToFieldsQueryTests
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
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "shipped_orders",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "count",
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
                            "value": "shipped"
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_revenue",
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
                        },
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
                            "value": "shipped"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AggregateWithPredicateNextToFieldsQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "shipped_orders",
                      "type": "long"
                    },
                    {
                      "name": "shipped_revenue",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "100.5",
                      "3",
                      "600.5"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "50",
                      "3",
                      "600.5"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      "3",
                      "600.5"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "75.25",
                      "3",
                      "600.5"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      "3",
                      "600.5"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "100.5",
                      "3",
                      "600.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new AggregateWithPredicateNextToFieldsQuery().Result
            ).TextValue
        );
    }
}
