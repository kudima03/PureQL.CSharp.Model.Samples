using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record ItemValuePerUserAndProductQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
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
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.order_items",
                      "field": "item_qty",
                      "type": {
                        "name": "decimal"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "1"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "user_and_product",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": " / "
                          },
                          {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_and_product",
                      "expression": {
                        "key": "user_and_product",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "item_value",
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
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            ]
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "units",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
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
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          ]
                        }
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ItemValuePerUserAndProductQuery().Value).TextValue
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
                      "name": "user_and_product",
                      "type": "string"
                    },
                    {
                      "name": "item_value",
                      "type": "double"
                    },
                    {
                      "name": "units",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Cara / Widget",
                      "30",
                      "3"
                    ],
                    [
                      "Bob / Gizmo",
                      "23",
                      "5"
                    ],
                    [
                      "Ann / Widget",
                      "20",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ItemValuePerUserAndProductQuery().Result).TextValue
        );
    }
}
