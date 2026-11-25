using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundHalfToEvenCornersQueryTests
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
                      "name": "discounts",
                      "rows": [
                        [
                          {
                            "alias": "item_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000012d-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.125"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "item_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000012f-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "-0.025"
                            }
                          }
                        ]
                      ]
                    }
                  ],
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
                    },
                    {
                      "kind": "left",
                      "subquery": "discounts",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "discounts",
                          "field": "item_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "to_a_whole_number",
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
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "to_cents",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "to_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "integer_to_hundreds",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
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
                              "value": 50
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "by_a_digits_literal",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "by_computed_digits",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          ]
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "of_a_nullable",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "discounts",
                          "field": "amount",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "of_a_literal_halfway",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "of_a_negative_halfway",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "-2.5"
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "default_rule_beside_it",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "nested",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 3
                          },
                          "mode": "halfEven"
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "cash_rounding",
                      "expression": {
                        "operator": "multiply",
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
                              "operator": "divide",
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
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.05"
                                }
                              ]
                            },
                            "mode": "halfEven"
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.05"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "of_a_quotient",
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
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 16
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 4
                        },
                        "mode": "halfEven"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundHalfToEvenCornersQuery().Value).TextValue
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
                      "name": "to_a_whole_number",
                      "type": "long"
                    },
                    {
                      "name": "to_cents",
                      "type": "double"
                    },
                    {
                      "name": "to_tens",
                      "type": "double"
                    },
                    {
                      "name": "integer_to_hundreds",
                      "type": "long"
                    },
                    {
                      "name": "by_a_digits_literal",
                      "type": "double"
                    },
                    {
                      "name": "by_computed_digits",
                      "type": "double"
                    },
                    {
                      "name": "of_a_nullable",
                      "type": "double"
                    },
                    {
                      "name": "of_a_literal_halfway",
                      "type": "long"
                    },
                    {
                      "name": "of_a_negative_halfway",
                      "type": "long"
                    },
                    {
                      "name": "default_rule_beside_it",
                      "type": "long"
                    },
                    {
                      "name": "nested",
                      "type": "double"
                    },
                    {
                      "name": "cash_rounding",
                      "type": "double"
                    },
                    {
                      "name": "of_a_quotient",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "10",
                      "9.99",
                      "10",
                      "100",
                      "10",
                      "9.99",
                      "0.12",
                      "2",
                      "-2",
                      "3",
                      "10",
                      "10",
                      "0.6244"
                    ],
                    [
                      "20",
                      "19.99",
                      "20",
                      "0",
                      "20",
                      "19.99",
                      "",
                      "2",
                      "-2",
                      "3",
                      "20",
                      "20",
                      "1.2494"
                    ],
                    [
                      "4",
                      "4.5",
                      "0",
                      "200",
                      "4.5",
                      "4.5",
                      "-0.02",
                      "2",
                      "-2",
                      "3",
                      "4.5",
                      "4.5",
                      "0.2812"
                    ],
                    [
                      "10",
                      "9.99",
                      "10",
                      "200",
                      "10",
                      "9.99",
                      "",
                      "2",
                      "-2",
                      "3",
                      "10",
                      "10",
                      "0.6244"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundHalfToEvenCornersQuery().Result).TextValue
        );
    }
}
