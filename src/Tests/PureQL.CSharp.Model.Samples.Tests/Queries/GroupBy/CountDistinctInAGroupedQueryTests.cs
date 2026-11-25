using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record CountDistinctInAGroupedQueryTests
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
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_product_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "product_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "countDistinct",
                      "type": {
                        "name": "integer"
                      },
                      "selector": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 1
                    }
                  },
                  "select": [
                    {
                      "alias": "product_id",
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "prices",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "quantities",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "share_of_orders",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "countDistinct",
                            "type": {
                              "name": "integer"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "countDistinct",
                            "type": {
                              "name": "integer"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "over": "all"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "sold_at_one_price",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "countDistinct",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "lines_per_order",
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
                            "operator": "countDistinct",
                            "type": {
                              "name": "integer"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CountDistinctInAGroupedQuery().Value).TextValue
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
                      "name": "product_id",
                      "type": "uuid"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "prices",
                      "type": "long"
                    },
                    {
                      "name": "quantities",
                      "type": "long"
                    },
                    {
                      "name": "share_of_orders",
                      "type": "double"
                    },
                    {
                      "name": "sold_at_one_price",
                      "type": "bool"
                    },
                    {
                      "name": "lines_per_order",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "2",
                      "1",
                      "2",
                      "0.6666666666666666",
                      "True",
                      "1"
                    ],
                    [
                      "000000ca-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "1",
                      "0.3333333333333333",
                      "True",
                      "1"
                    ],
                    [
                      "000000cb-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "1",
                      "0.3333333333333333",
                      "True",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CountDistinctInAGroupedQuery().Result).TextValue
        );
    }
}
