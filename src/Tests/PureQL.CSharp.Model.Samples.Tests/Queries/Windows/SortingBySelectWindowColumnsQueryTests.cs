using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record SortingBySelectWindowColumnsQueryTests
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
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "line_total",
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "running_total",
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
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "position",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
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
                                  }
                                ]
                              },
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "share",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
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
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
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
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "#"
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
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
                  ],
                  "orderBy": [
                    {
                      "column": "order_id"
                    },
                    {
                      "column": "position"
                    },
                    {
                      "column": "running_total",
                      "direction": "desc"
                    },
                    {
                      "column": "share"
                    },
                    {
                      "column": "label"
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SortingBySelectWindowColumnsQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "line_total",
                      "type": "double"
                    },
                    {
                      "name": "running_total",
                      "type": "double"
                    },
                    {
                      "name": "position",
                      "type": "long"
                    },
                    {
                      "name": "share",
                      "type": "double"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "00000065-0000-0000-0000-000000000000",
                      "19.99",
                      "39.97",
                      "1",
                      "0.21624837732583296",
                      "#1"
                    ],
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "00000065-0000-0000-0000-000000000000",
                      "19.98",
                      "19.98",
                      "2",
                      "0.21614019904803117",
                      "#2"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "00000067-0000-0000-0000-000000000000",
                      "22.5",
                      "22.5",
                      "1",
                      "0.24340112505408915",
                      "#5"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "00000069-0000-0000-0000-000000000000",
                      "29.97",
                      "29.97",
                      "1",
                      "0.32421029857204675",
                      "#3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SortingBySelectWindowColumnsQuery().Result).TextValue
        );
    }
}
