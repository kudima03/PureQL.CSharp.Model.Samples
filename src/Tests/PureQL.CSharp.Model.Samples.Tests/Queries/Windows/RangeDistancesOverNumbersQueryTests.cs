using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RangeDistancesOverNumbersQueryTests
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
                      "alias": "integer_key_preceding",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "ceiling",
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
                          ],
                          "range": {
                            "preceding": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "integer_key_following",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "ceiling",
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
                          ],
                          "range": {
                            "following": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "integer_key_all_before",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "ceiling",
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
                          ],
                          "range": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "integer_key_all_after_and_one_before",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "ceiling",
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
                          ],
                          "range": {
                            "preceding": 1,
                            "following": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_key_by_a_whole_number",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": 10
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_key_by_a_decimal",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "5.5"
                            },
                            "following": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "5.5"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_key_preceding_a_decimal",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "5.5"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_key_following_a_whole_number",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ],
                          "range": {
                            "following": 10
                          }
                        }
                      }
                    },
                    {
                      "alias": "decimal_key_small_distance",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ],
                          "range": {
                            "following": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.0000000000000000000000000001"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "computed_key",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
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
                          "range": {
                            "preceding": 5
                          }
                        }
                      }
                    },
                    {
                      "alias": "descending_key",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.products",
                                "field": "product_price",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "direction": "desc"
                            }
                          ],
                          "range": {
                            "preceding": 10
                          }
                        }
                      }
                    },
                    {
                      "alias": "partitioned",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "ceiling",
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
                          ],
                          "range": {
                            "preceding": 1,
                            "following": 1
                          },
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
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
            new QueryJson(new RangeDistancesOverNumbersQuery().Value).TextValue
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
                      "name": "integer_key_preceding",
                      "type": "long"
                    },
                    {
                      "name": "integer_key_following",
                      "type": "long"
                    },
                    {
                      "name": "integer_key_all_before",
                      "type": "long"
                    },
                    {
                      "name": "integer_key_all_after_and_one_before",
                      "type": "long"
                    },
                    {
                      "name": "decimal_key_by_a_whole_number",
                      "type": "long"
                    },
                    {
                      "name": "decimal_key_by_a_decimal",
                      "type": "long"
                    },
                    {
                      "name": "decimal_key_preceding_a_decimal",
                      "type": "long"
                    },
                    {
                      "name": "decimal_key_following_a_whole_number",
                      "type": "long"
                    },
                    {
                      "name": "decimal_key_small_distance",
                      "type": "long"
                    },
                    {
                      "name": "computed_key",
                      "type": "long"
                    },
                    {
                      "name": "descending_key",
                      "type": "long"
                    },
                    {
                      "name": "partitioned",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "2",
                      "2",
                      "2",
                      "4",
                      "3",
                      "3",
                      "3",
                      "3",
                      "2",
                      "1",
                      "3",
                      "2"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "1",
                      "2",
                      "1",
                      "4",
                      "3",
                      "1",
                      "1",
                      "1",
                      "1",
                      "2",
                      "1",
                      "2"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "2",
                      "1",
                      "4",
                      "1",
                      "1",
                      "3",
                      "1",
                      "3",
                      "1",
                      "3",
                      "3",
                      "1"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "3",
                      "1",
                      "3",
                      "3",
                      "3",
                      "3",
                      "3",
                      "3",
                      "2",
                      "1",
                      "3",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RangeDistancesOverNumbersQuery().Result).TextValue
        );
    }
}
