using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record WeeklyUnitsPerProductQueryTests
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
                    },
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
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-01"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "week_no",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-05-27"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "week_start",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-05-27"
                        },
                        "right": {
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "key": "week_no",
                              "type": {
                                "name": "integer"
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "units_widget",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Widget"
                          }
                        }
                      }
                    },
                    {
                      "alias": "units_gadget",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Gadget"
                          }
                        }
                      }
                    },
                    {
                      "alias": "units_gizmo",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Gizmo"
                          }
                        }
                      }
                    },
                    {
                      "alias": "units_deluxe",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_qty",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Deluxe"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "week_no",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WeeklyUnitsPerProductQuery().Value).TextValue
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
                      "name": "week_start",
                      "type": "date"
                    },
                    {
                      "name": "units_widget",
                      "type": "double"
                    },
                    {
                      "name": "units_gadget",
                      "type": "double"
                    },
                    {
                      "name": "units_gizmo",
                      "type": "double"
                    },
                    {
                      "name": "units_deluxe",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-05-27",
                      "2",
                      "1",
                      "0",
                      "0"
                    ],
                    [
                      "2024-06-03",
                      "3",
                      "0",
                      "5",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WeeklyUnitsPerProductQuery().Result).TextValue
        );
    }
}
