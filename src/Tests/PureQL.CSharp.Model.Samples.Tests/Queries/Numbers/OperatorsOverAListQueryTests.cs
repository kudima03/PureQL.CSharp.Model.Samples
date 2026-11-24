using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record OperatorsOverAListQueryTests
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
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
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
                        },
                        {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.2"
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "3.997"
                    }
                  },
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
                      "alias": "tax",
                      "expression": {
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
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.2"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "kept_units",
                      "expression": {
                        "operator": "subtract",
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
                            "value": 1
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "share_in_steps",
                      "expression": {
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
                            "value": 3
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        ]
                      }
                    },
                    {
                      "alias": "share_divided_once",
                      "expression": {
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
                                "value": 2
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "alias": "units_and_a_half",
                      "expression": {
                        "operator": "add",
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
                            "value": 1
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.5"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "all_units",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
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
                      }
                    },
                    {
                      "alias": "average_price",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "tax",
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "take": 50
                  }
                }
                """
            ).TextValue,
            new QueryJson(new OperatorsOverAListQuery().Value).TextValue
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
                      "name": "tax",
                      "type": "double"
                    },
                    {
                      "name": "kept_units",
                      "type": "long"
                    },
                    {
                      "name": "share_in_steps",
                      "type": "double"
                    },
                    {
                      "name": "share_divided_once",
                      "type": "double"
                    },
                    {
                      "name": "units_and_a_half",
                      "type": "double"
                    },
                    {
                      "name": "all_units",
                      "type": "long"
                    },
                    {
                      "name": "average_price",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "5.994",
                      "1",
                      "1.665",
                      "1.665",
                      "4.5",
                      "9",
                      "11.493333333333334"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "4.5",
                      "3",
                      "0.75",
                      "0.75",
                      "6.5",
                      "9",
                      "11.493333333333334"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "3.998",
                      "-1",
                      "3.3316666666666666",
                      "3.3316666666666666",
                      "2.5",
                      "9",
                      "11.493333333333334"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OperatorsOverAListQuery().Result).TextValue
        );
    }
}
