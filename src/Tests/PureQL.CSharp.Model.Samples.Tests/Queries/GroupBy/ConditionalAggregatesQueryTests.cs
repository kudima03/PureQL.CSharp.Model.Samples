using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record ConditionalAggregatesQueryTests
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
                      "alias": "order_user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "all",
                    "type": {
                      "name": "boolean"
                    },
                    "predicate": {
                      "operator": "notEqual",
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
                        "value": "cancelled"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "order_user_id",
                      "expression": {
                        "key": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "cancelled",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
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
                            "value": "cancelled"
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_total",
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
                        },
                        "predicate": {
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
                        }
                      }
                    },
                    {
                      "alias": "shipped_total_via_if",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "decimal"
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "has_pending",
                      "expression": {
                        "operator": "any",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
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
                            "value": "pending"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConditionalAggregatesQuery().Value).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "cancelled",
                      "type": "long"
                    },
                    {
                      "name": "shipped_total",
                      "type": "double"
                    },
                    {
                      "name": "shipped_total_via_if",
                      "type": "double"
                    },
                    {
                      "name": "has_pending",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "0",
                      "100.5",
                      "100.5",
                      "True"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "0",
                      "200",
                      "200",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "0",
                      "0",
                      "0",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConditionalAggregatesQuery().Result).TextValue
        );
    }
}
