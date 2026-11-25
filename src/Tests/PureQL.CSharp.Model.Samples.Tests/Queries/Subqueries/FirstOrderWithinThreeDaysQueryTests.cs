using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record FirstOrderWithinThreeDaysQueryTests
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
                      "name": "first_orders",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
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
                            "alias": "first_order_at",
                            "expression": {
                              "operator": "min",
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
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "first_orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "first_orders",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "datetimeDiffSeconds",
                      "type": {
                        "name": "decimal"
                      },
                      "left": {
                        "source": "first_orders",
                        "field": "first_order_at",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "right": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    "right": {
                      "operator": "multiply",
                      "type": {
                        "name": "integer"
                      },
                      "values": [
                        {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        },
                        {
                          "type": {
                            "name": "integer"
                          },
                          "value": 86400
                        }
                      ]
                    }
                  },
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
                      "alias": "first_order_at",
                      "expression": {
                        "source": "first_orders",
                        "field": "first_order_at",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "hours_to_first_order",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "datetimeDiffSeconds",
                              "type": {
                                "name": "decimal"
                              },
                              "left": {
                                "source": "first_orders",
                                "field": "first_order_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "right": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "last_login",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3600
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FirstOrderWithinThreeDaysQuery().Value).TextValue
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
                      "name": "first_order_at",
                      "type": "datetime"
                    },
                    {
                      "name": "hours_to_first_order",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann",
                      "2024-06-01T10:00:00Z",
                      "1.5"
                    ],
                    [
                      "Bob",
                      "2024-06-03T12:00:00Z",
                      "26.8"
                    ],
                    [
                      "Dan",
                      "2024-06-06T15:00:00Z",
                      "68.3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FirstOrderWithinThreeDaysQuery().Result).TextValue
        );
    }
}
