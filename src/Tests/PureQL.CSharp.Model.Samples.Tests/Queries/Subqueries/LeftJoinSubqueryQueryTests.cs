using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record LeftJoinSubqueryQueryTests
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
                      "name": "user_totals",
                      "query": {
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
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "key": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "total",
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
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "user_totals",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "user_totals",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "source": "user_totals",
                            "field": "orders",
                            "type": {
                              "name": "integer",
                              "nullable": true
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
                    },
                    {
                      "alias": "total",
                      "expression": {
                        "source": "user_totals",
                        "field": "total",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LeftJoinSubqueryQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann",
                      "1",
                      "100.5"
                    ],
                    [
                      "Bob",
                      "1",
                      "200"
                    ],
                    [
                      "Cara",
                      "1",
                      "300"
                    ],
                    [
                      "Dan",
                      "0",
                      ""
                    ],
                    [
                      "Eve",
                      "0",
                      ""
                    ],
                    [
                      "Fay",
                      "0",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LeftJoinSubqueryQuery().Result).TextValue
        );
    }
}
