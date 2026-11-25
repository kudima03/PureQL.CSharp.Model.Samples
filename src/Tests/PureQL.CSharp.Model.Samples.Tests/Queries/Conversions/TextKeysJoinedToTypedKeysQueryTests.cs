using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record TextKeysJoinedToTypedKeysQueryTests
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
                      "name": "listing",
                      "rows": [
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "000000c9-0000-0000-0000-000000000000"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "SKU-19.99"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "000000cb-0000-0000-0000-000000000000"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "000000cc-0000-0000-0000-000000000000"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "coupons",
                      "rows": [
                        [
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "9.99"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "4.5"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "SAVE10"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
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
                      "subquery": "listing",
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
                          "operator": "tryAsUuid",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "operand": {
                            "source": "listing",
                            "field": "sku",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "coupons",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "tryAsDecimal",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "operand": {
                            "source": "coupons",
                            "field": "code",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThan",
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
                      "operator": "asInteger",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "4"
                      }
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
                      "alias": "sku",
                      "expression": {
                        "source": "listing",
                        "field": "sku",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "sku_is_the_product_id",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "tryAsUuid",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "operand": {
                            "source": "listing",
                            "field": "sku",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
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
                      "alias": "coupon_amount",
                      "expression": {
                        "operator": "tryAsDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "coupons",
                          "field": "code",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "tryAsDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "coupons",
                          "field": "code",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
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
                """
            ).TextValue,
            new QueryJson(new TextKeysJoinedToTypedKeysQuery().Value).TextValue
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
                      "name": "sku",
                      "type": "string"
                    },
                    {
                      "name": "sku_is_the_product_id",
                      "type": "bool"
                    },
                    {
                      "name": "coupon_amount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "000000c9-0000-0000-0000-000000000000",
                      "True",
                      "9.99"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "000000c9-0000-0000-0000-000000000000",
                      "True",
                      "9.99"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "",
                      "False",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TextKeysJoinedToTypedKeysQuery().Result).TextValue
        );
    }
}
