using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record UnionColumnTypesQueryTests
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
                      "name": "amounts",
                      "query": {
                        "union": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.order_items"
                            },
                            "select": [
                              {
                                "alias": "key",
                                "expression": {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_product_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "amount",
                                "expression": {
                                  "operator": "round",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                }
                              },
                              {
                                "alias": "note",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "units"
                                }
                              }
                            ]
                          },
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
                            "select": [
                              {
                                "alias": "note",
                                "expression": {
                                  "type": {
                                    "name": "string",
                                    "nullable": true
                                  },
                                  "value": null
                                }
                              },
                              {
                                "alias": "amount",
                                "expression": {
                                  "source": "schema_with_foreign_keys.products",
                                  "field": "product_price",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              },
                              {
                                "alias": "key",
                                "expression": {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_product_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "key",
                                "expression": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "alias": "amount",
                                "expression": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_score",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  }
                                }
                              },
                              {
                                "alias": "note",
                                "expression": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string",
                                    "nullable": true
                                  },
                                  "condition": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_active",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  },
                                  "then": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_name",
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
                                }
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "amounts"
                  },
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "amounts",
                      "field": "amount",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      }
                    },
                    "right": {
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "value": null
                    }
                  },
                  "select": [
                    {
                      "alias": "key",
                      "expression": {
                        "source": "amounts",
                        "field": "key",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "amount",
                      "expression": {
                        "source": "amounts",
                        "field": "amount",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "note",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "amounts",
                            "field": "note",
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
                        "source": "amounts",
                        "field": "key",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "amounts",
                        "field": "amount",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "amounts",
                        "field": "note",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UnionColumnTypesQuery().Value).TextValue
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
                      "name": "key",
                      "type": "uuid"
                    },
                    {
                      "name": "amount",
                      "type": "double"
                    },
                    {
                      "name": "note",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "30",
                      "Ann"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "30",
                      "Cara"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "10",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "28",
                      "Fay"
                    ],
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "2",
                      "units"
                    ],
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "3",
                      "units"
                    ],
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "9.99",
                      ""
                    ],
                    [
                      "000000ca-0000-0000-0000-000000000000",
                      "1",
                      "units"
                    ],
                    [
                      "000000ca-0000-0000-0000-000000000000",
                      "19.99",
                      ""
                    ],
                    [
                      "000000cb-0000-0000-0000-000000000000",
                      "4.5",
                      ""
                    ],
                    [
                      "000000cb-0000-0000-0000-000000000000",
                      "5",
                      "units"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UnionColumnTypesQuery().Result).TextValue
        );
    }
}
