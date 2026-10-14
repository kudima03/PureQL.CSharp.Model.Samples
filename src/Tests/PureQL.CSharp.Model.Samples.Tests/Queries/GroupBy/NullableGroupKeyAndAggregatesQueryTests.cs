using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record NullableGroupKeyAndAggregatesQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
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
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
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
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "buyer",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string",
                          "nullable": true
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
                    },
                    "right": {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-06-02T12:00:00Z"
                    }
                  },
                  "select": [
                    {
                      "alias": "buyer",
                      "expression": {
                        "key": "buyer",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "last_order_at",
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
                      "alias": "average_order_at",
                      "expression": {
                        "operator": "average",
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
                      "alias": "min_large_total",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "min",
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
                            },
                            "predicate": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "integer"
                                },
                                "value": 100
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
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "buyer",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullableGroupKeyAndAggregatesQuery().Value).TextValue
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
                      "name": "buyer",
                      "type": "string"
                    },
                    {
                      "name": "last_order_at",
                      "type": "datetime"
                    },
                    {
                      "name": "average_order_at",
                      "type": "datetime"
                    },
                    {
                      "name": "min_large_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "",
                      "2024-06-03T12:00:00",
                      "2024-06-03T12:00:00",
                      "200"
                    ],
                    [
                      "Cara",
                      "2024-06-05T14:00:00",
                      "2024-06-05T01:30:00",
                      "300"
                    ],
                    [
                      "Dan",
                      "2024-06-06T15:00:00",
                      "2024-06-06T15:00:00",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullableGroupKeyAndAggregatesQuery().Result).TextValue
        );
    }
}
