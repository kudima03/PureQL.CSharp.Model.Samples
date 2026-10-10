using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByComputedExpressionQueryTests
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
                      "alias": "subtotal",
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
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
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
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByComputedExpressionQuery().Value).TextValue
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
                      "name": "subtotal",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "29.97"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "22.5"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "19.99"
                    ],
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "19.98"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByComputedExpressionQuery().Result).TextValue
        );
    }
}
