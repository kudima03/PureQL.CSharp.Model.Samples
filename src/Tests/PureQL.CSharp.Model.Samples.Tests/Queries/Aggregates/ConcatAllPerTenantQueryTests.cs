using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record ConcatAllPerTenantQueryTests
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
                    },
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
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
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "item_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "item_tenant_id",
                      "expression": {
                        "key": "item_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "lines",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "product_names",
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
                    },
                    {
                      "alias": "order_statuses",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
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
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "in_stock_names",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_in_stock",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " | "
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
                    },
                    {
                      "alias": "lines_above_one_unit",
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
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
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
                        ],
                        "predicate": {
                          "operator": "greaterThan",
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
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "item_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConcatAllPerTenantQuery().Value).TextValue
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
                      "name": "item_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "lines",
                      "type": "long"
                    },
                    {
                      "name": "product_names",
                      "type": "string"
                    },
                    {
                      "name": "order_statuses",
                      "type": "string"
                    },
                    {
                      "name": "in_stock_names",
                      "type": "string"
                    },
                    {
                      "name": "lines_above_one_unit",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "3",
                      "Gadget, Gizmo, Widget",
                      "shipped",
                      "Gizmo | Widget",
                      "Gizmo, Widget"
                    ],
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "1",
                      "Widget",
                      "shipped",
                      "Widget",
                      "Widget"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConcatAllPerTenantQuery().Result).TextValue
        );
    }
}
