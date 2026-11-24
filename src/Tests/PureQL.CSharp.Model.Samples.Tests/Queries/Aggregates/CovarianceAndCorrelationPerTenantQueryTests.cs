using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record CovarianceAndCorrelationPerTenantQueryTests
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
                      "alias": "price_and_quantity_move_together",
                      "expression": {
                        "operator": "correlation",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "covariance",
                      "expression": {
                        "operator": "covariancePopulation",
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
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "sample_covariance",
                      "expression": {
                        "operator": "covarianceSample",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "in_stock_price_and_quantity",
                      "expression": {
                        "operator": "correlation",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "predicate": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_in_stock",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "quantity_variance",
                      "expression": {
                        "operator": "covariancePopulation",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
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
            new QueryJson(new CovarianceAndCorrelationPerTenantQuery().Value).TextValue
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
                      "name": "price_and_quantity_move_together",
                      "type": "double"
                    },
                    {
                      "name": "covariance",
                      "type": "double"
                    },
                    {
                      "name": "sample_covariance",
                      "type": "double"
                    },
                    {
                      "name": "in_stock_price_and_quantity",
                      "type": "double"
                    },
                    {
                      "name": "quantity_variance",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "3",
                      "-0.9014985363819789",
                      "-9.825555555555555",
                      "-14.738333333333333",
                      "-1",
                      "2.888888888888889"
                    ],
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "1",
                      "",
                      "0",
                      "",
                      "",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CovarianceAndCorrelationPerTenantQuery().Result).TextValue
        );
    }
}
