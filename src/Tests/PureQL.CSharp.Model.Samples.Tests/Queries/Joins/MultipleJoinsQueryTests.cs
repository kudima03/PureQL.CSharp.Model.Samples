using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record MultipleJoinsQueryTests
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
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "type": "left",
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
                      "alias": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "product_name",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.products",
                        "field": "product_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "item_qty",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_qty",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MultipleJoinsQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "product_name",
                      "type": "string"
                    },
                    {
                      "name": "item_qty",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann",
                      "00000065-0000-0000-0000-000000000000",
                      "Widget",
                      "2"
                    ],
                    [
                      "Ann",
                      "00000065-0000-0000-0000-000000000000",
                      "Gadget",
                      "1"
                    ],
                    [
                      "Bob",
                      "00000067-0000-0000-0000-000000000000",
                      "Gizmo",
                      "5"
                    ],
                    [
                      "Cara",
                      "00000069-0000-0000-0000-000000000000",
                      "Widget",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MultipleJoinsQuery().Result).TextValue
        );
    }
}
