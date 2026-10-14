using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupedRevenueQueryTests
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
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
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
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "sum",
                      "type": {
                        "name": "decimal"
                      },
                      "selector": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "25"
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
                      "alias": "revenue",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "line_items",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "share_of_all_lines",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            },
                            "over": "all"
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupedRevenueQuery().Value).TextValue
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
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "line_items",
                      "type": "long"
                    },
                    {
                      "name": "share_of_all_lines",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "39.97",
                      "2",
                      "0.5"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "29.97",
                      "1",
                      "0.25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupedRevenueQuery().Result).TextValue
        );
    }
}
