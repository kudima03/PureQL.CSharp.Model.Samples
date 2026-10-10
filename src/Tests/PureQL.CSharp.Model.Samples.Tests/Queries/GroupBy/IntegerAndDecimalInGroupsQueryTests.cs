using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record IntegerAndDecimalInGroupsQueryTests
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
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
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
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "equal",
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
                    "left": {
                      "operator": "modulo",
                      "left": {
                        "operator": "round",
                        "value": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      },
                      "right": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 2
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 1
                    }
                  },
                  "groupBy": [
                    {
                      "type": {
                        "name": "uuid"
                      },
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
                      "alias": "order_user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "units",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "sum",
                        "selector": {
                          "operator": "round",
                          "value": {
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
                      "alias": "revenue",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "round",
                        "value": {
                          "operator": "sum",
                          "selector": {
                            "operator": "multiply",
                            "values": [
                              {
                                "operator": "round",
                                "value": {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_qty",
                                  "type": {
                                    "name": "decimal"
                                  }
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
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "tens_of_lines",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "integerDivide",
                        "left": {
                          "operator": "count"
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        }
                      }
                    },
                    {
                      "alias": "reorder_estimate",
                      "type": {
                        "name": "date"
                      },
                      "expression": {
                        "operator": "dateAddDays",
                        "left": {
                          "operator": "max",
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "operator": "floor",
                          "value": {
                            "operator": "average",
                            "selector": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new IntegerAndDecimalInGroupsQuery().Value).TextValue
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
                      "name": "units",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "tens_of_lines",
                      "type": "long"
                    },
                    {
                      "name": "reorder_estimate",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "1",
                      "20",
                      "0",
                      "2024-06-02"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "5",
                      "22.5",
                      "0",
                      "2024-06-08"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "3",
                      "30",
                      "0",
                      "2024-06-08"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new IntegerAndDecimalInGroupsQuery().Result).TextValue
        );
    }
}
