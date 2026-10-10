using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record DecimalWideningAndDivideQueryTests
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
                  "select": [
                    {
                      "alias": "item_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "line_total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "multiply",
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
                            "value": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "half_quantity",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "divide",
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
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        ]
                      }
                    },
                    {
                      "alias": "price_with_tax",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "multiply",
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
                            "value": 1.2
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DecimalWideningAndDivideQuery().Value).TextValue
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
                      "name": "line_total",
                      "type": "double"
                    },
                    {
                      "name": "half_quantity",
                      "type": "double"
                    },
                    {
                      "name": "price_with_tax",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "19.98",
                      "1",
                      "11.988"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "19.99",
                      "0.5",
                      "23.988"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "22.5",
                      "2.5",
                      "5.4"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "29.97",
                      "1.5",
                      "11.988"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DecimalWideningAndDivideQuery().Result).TextValue
        );
    }
}
