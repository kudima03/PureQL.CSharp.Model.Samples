using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record WholeNumbersQueryTests
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
                  "select": [
                    {
                      "alias": "item_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "units",
                      "expression": {
                        "operator": "round",
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
                    },
                    {
                      "alias": "whole_price",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "price_floor",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "line_total_rounded_up",
                      "expression": {
                        "operator": "ceiling",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
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
                              "operator": "round",
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "half_the_units",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "round",
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
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "units_in_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "round",
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
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "pairs",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "round",
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
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WholeNumbersQuery().Value).TextValue
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
                      "name": "item_id",
                      "type": "uuid"
                    },
                    {
                      "name": "units",
                      "type": "long"
                    },
                    {
                      "name": "whole_price",
                      "type": "long"
                    },
                    {
                      "name": "price_floor",
                      "type": "long"
                    },
                    {
                      "name": "line_total_rounded_up",
                      "type": "long"
                    },
                    {
                      "name": "half_the_units",
                      "type": "long"
                    },
                    {
                      "name": "units_in_tens",
                      "type": "long"
                    },
                    {
                      "name": "pairs",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "2",
                      "10",
                      "9",
                      "20",
                      "1",
                      "0",
                      "1"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "1",
                      "20",
                      "19",
                      "20",
                      "0",
                      "0",
                      "0"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "5",
                      "5",
                      "4",
                      "23",
                      "2",
                      "10",
                      "2"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "3",
                      "10",
                      "9",
                      "30",
                      "1",
                      "0",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WholeNumbersQuery().Result).TextValue
        );
    }
}
