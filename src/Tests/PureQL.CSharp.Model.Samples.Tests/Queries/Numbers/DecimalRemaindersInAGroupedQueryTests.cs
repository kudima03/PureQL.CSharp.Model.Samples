using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record DecimalRemaindersInAGroupedQueryTests
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
                  "groupBy": [
                    {
                      "alias": "price_band",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
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
                      "alias": "cents",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
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
                      "operator": "modulo",
                      "type": {
                        "name": "decimal"
                      },
                      "left": {
                        "operator": "sum",
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
                      },
                      "right": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 10
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "4.75"
                    }
                  },
                  "select": [
                    {
                      "alias": "price_band",
                      "expression": {
                        "key": "price_band",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "cents",
                      "expression": {
                        "key": "cents",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "remainder_of_the_sum",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "operator": "sum",
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
                      "alias": "average_remainder",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "modulo",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.05"
                          }
                        }
                      }
                    },
                    {
                      "alias": "off_step",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "modulo",
                            "type": {
                              "name": "decimal"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.05"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "band_start",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "key": "price_band",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        ]
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
                        "key": "price_band",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "cents",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DecimalRemaindersInAGroupedQuery().Value).TextValue
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
                      "name": "price_band",
                      "type": "long"
                    },
                    {
                      "name": "cents",
                      "type": "double"
                    },
                    {
                      "name": "remainder_of_the_sum",
                      "type": "double"
                    },
                    {
                      "name": "average_remainder",
                      "type": "double"
                    },
                    {
                      "name": "off_step",
                      "type": "long"
                    },
                    {
                      "name": "band_start",
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
                      "0",
                      "0.99",
                      "9.98",
                      "0.04",
                      "2",
                      "0",
                      "2"
                    ],
                    [
                      "1",
                      "0.99",
                      "9.99",
                      "0.04",
                      "1",
                      "10",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DecimalRemaindersInAGroupedQuery().Result).TextValue
        );
    }
}
