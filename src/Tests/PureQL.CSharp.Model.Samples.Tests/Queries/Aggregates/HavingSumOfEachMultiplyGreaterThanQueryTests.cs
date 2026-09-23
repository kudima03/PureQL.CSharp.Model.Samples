using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record HavingSumOfEachMultiplyGreaterThanQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "operator": "sum",
                      "arg": {
                        "operator": "eachMultiply",
                        "values": [
                          {
                            "entity": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "number"
                            }
                          },
                          {
                            "entity": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "number"
                            }
                          }
                        ]
                      },
                      "alias": "revenue"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.order_items",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.orders",
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
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.order_items",
                          "field": "item_product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.products",
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
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "left": {
                      "operator": "sum",
                      "arg": {
                        "operator": "eachMultiply",
                        "values": [
                          {
                            "entity": "schema_with_foreign_keys.order_items",
                            "field": "item_qty",
                            "type": {
                              "name": "number"
                            }
                          },
                          {
                            "entity": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "number"
                            }
                          }
                        ]
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 25
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new HavingSumOfEachMultiplyGreaterThanQuery().Value).TextValue
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
                      "name": "revenue",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "39.97"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "29.97"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new HavingSumOfEachMultiplyGreaterThanQuery().Result
            ).TextValue
        );
    }
}
