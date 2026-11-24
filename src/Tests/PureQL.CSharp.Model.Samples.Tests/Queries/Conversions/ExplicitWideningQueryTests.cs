using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record ExplicitWideningQueryTests
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
                      "name": "lines",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.order_items"
                        },
                        "select": [
                          {
                            "alias": "item_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "item_order_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "quantity",
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
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "lines"
                  },
                  "select": [
                    {
                      "alias": "item_id",
                      "expression": {
                        "source": "lines",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "quantity",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "previous_quantity_or_half",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "default": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.5"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "lines",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "lines",
                                "field": "item_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "quantity_unless_one",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1"
                        }
                      }
                    },
                    {
                      "alias": "optional_order_id",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "operand": {
                          "source": "lines",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "optional_quantity",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "lines",
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
            new QueryJson(new ExplicitWideningQuery().Value).TextValue
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
                      "name": "quantity",
                      "type": "double"
                    },
                    {
                      "name": "previous_quantity_or_half",
                      "type": "double"
                    },
                    {
                      "name": "quantity_unless_one",
                      "type": "double"
                    },
                    {
                      "name": "optional_order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "optional_quantity",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "2",
                      "0.5",
                      "2",
                      "00000065-0000-0000-0000-000000000000",
                      "2"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "1",
                      "2",
                      "",
                      "00000065-0000-0000-0000-000000000000",
                      "1"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "5",
                      "0.5",
                      "5",
                      "00000067-0000-0000-0000-000000000000",
                      "5"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "3",
                      "0.5",
                      "3",
                      "00000069-0000-0000-0000-000000000000",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ExplicitWideningQuery().Result).TextValue
        );
    }
}
