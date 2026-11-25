using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record JoinOnNormalizedSkuQueryTests
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
                      "name": "catalogue",
                      "rows": [
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "SKU:000000C9-0000-0000-0000-000000000000"
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
                              "value": "sku:000000ca-0000-0000-0000-000000000000"
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
                              "value": "SKU:000000CB-0000-0000-0000-000000000000"
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
                              "value": "SKU:000000CC-0000-0000-0000-000000000000"
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
                      "subquery": "catalogue",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_product_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "right": {
                          "operator": "replace",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "lower",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "catalogue",
                              "field": "sku",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "sku:"
                          },
                          "replacement": {
                            "type": {
                              "name": "string"
                            },
                            "value": ""
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "position",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "source": "catalogue",
                        "field": "sku",
                        "type": {
                          "name": "string"
                        }
                      },
                      "pattern": {
                        "type": {
                          "name": "string"
                        },
                        "value": "SKU:"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 1
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "sku_prefix",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "catalogue",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "sku_code",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "catalogue",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "product_id_group",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.order_items",
                            "field": "item_product_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "catalogue",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
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
                """
            ).TextValue,
            new QueryJson(new JoinOnNormalizedSkuQuery().Value).TextValue
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
                      "name": "sku_prefix",
                      "type": "string"
                    },
                    {
                      "name": "sku_code",
                      "type": "string"
                    },
                    {
                      "name": "product_id_group",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "SKU",
                      "000000C9-0000-0000-0000-000000000000",
                      "000000c9"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "SKU",
                      "000000C9-0000-0000-0000-000000000000",
                      "000000c9"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "SKU",
                      "000000CB-0000-0000-0000-000000000000",
                      "000000cb"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new JoinOnNormalizedSkuQuery().Result).TextValue
        );
    }
}
