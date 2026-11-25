using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record NestedAndFlatOperatorsQueryTests
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
                      "alias": "value",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "add",
                            "type": {
                              "name": "integer"
                            },
                            "values": [
                              {
                                "operator": "add",
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
                                    "operator": "modulo",
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
                                ]
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            ]
                          },
                          {
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
                                    "value": 4
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 3
                                  }
                                ]
                              }
                            ]
                          },
                          {
                            "operator": "divide",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
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
                                    "value": 4
                                  }
                                ]
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          },
                          {
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
                                  "value": 3
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2
                                }
                              ]
                            },
                            "digits": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NestedAndFlatOperatorsQuery().Value).TextValue
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
                      "name": "value",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6.335"
                    ],
                    [
                      "9.661666666666667"
                    ],
                    [
                      "8.5"
                    ],
                    [
                      "8.335"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NestedAndFlatOperatorsQuery().Result).TextValue
        );
    }
}
