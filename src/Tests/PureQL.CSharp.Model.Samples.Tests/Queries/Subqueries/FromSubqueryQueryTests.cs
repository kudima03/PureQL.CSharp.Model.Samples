using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record FromSubqueryQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "subqueries": [
                    {
                      "name": "recent_orders",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "greaterThanOrEqual",
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
                            "value": "2024-06-03T00:00:00Z"
                          }
                        },
                        "select": [
                          {
                            "alias": "id",
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
                            "alias": "total",
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
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "recent_orders"
                  },
                  "select": [
                    {
                      "alias": "id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "recent_orders",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "recent_orders",
                        "field": "total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "recent_orders",
                        "field": "total",
                        "type": {
                          "name": "decimal"
                        }
                      },
                      "direction": "desc"
                    }
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(new FromSubqueryQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FromSubqueryQuery().Result).TextValue
        );
    }
}
