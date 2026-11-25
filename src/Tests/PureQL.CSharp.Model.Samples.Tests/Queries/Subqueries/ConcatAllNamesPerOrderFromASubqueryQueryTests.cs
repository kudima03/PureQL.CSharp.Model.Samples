using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record ConcatAllNamesPerOrderFromASubqueryQueryTests
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
                      "name": "order_skus",
                      "query": {
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
                            "alias": "order_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "order_id",
                            "expression": {
                              "key": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "skus",
                            "expression": {
                              "operator": "concatAll",
                              "type": {
                                "name": "string"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "separator": {
                                "type": {
                                  "name": "string"
                                },
                                "value": ", "
                              },
                              "orderBy": [
                                {
                                  "expression": {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                }
                              ],
                              "distinct": true
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "order_skus",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "order_skus",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "contains",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "source": "order_skus",
                      "field": "skus",
                      "type": {
                        "name": "string",
                        "nullable": true
                      }
                    },
                    "pattern": {
                      "type": {
                        "name": "string"
                      },
                      "value": "get"
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "skus",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "order_skus",
                            "field": "skus",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ""
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConcatAllNamesPerOrderFromASubqueryQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "skus",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "Gadget, Widget"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "Widget"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new ConcatAllNamesPerOrderFromASubqueryQuery().Result
            ).TextValue
        );
    }
}
