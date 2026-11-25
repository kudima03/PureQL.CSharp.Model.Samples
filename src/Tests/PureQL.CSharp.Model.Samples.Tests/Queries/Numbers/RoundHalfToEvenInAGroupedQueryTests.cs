using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundHalfToEvenInAGroupedQueryTests
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
                      "alias": "product_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "rounded_price",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "mode": "halfEven"
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "round",
                      "type": {
                        "name": "decimal"
                      },
                      "operand": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
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
                              ]
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.13"
                            }
                          ]
                        }
                      },
                      "digits": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 2
                      },
                      "mode": "halfEven"
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "2.7"
                    }
                  },
                  "select": [
                    {
                      "alias": "product_id",
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "rounded_price",
                      "expression": {
                        "key": "rounded_price",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "tax_rounded_once",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
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
                                ]
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0.13"
                              }
                            ]
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "tax_rounded_per_line",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
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
                                ]
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0.13"
                              }
                            ]
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          },
                          "mode": "halfEven"
                        }
                      }
                    },
                    {
                      "alias": "rounding_difference",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "round",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
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
                                    ]
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "0.13"
                                  }
                                ]
                              }
                            },
                            "digits": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            "mode": "halfEven"
                          },
                          {
                            "operator": "sum",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
                              "operator": "round",
                              "type": {
                                "name": "decimal"
                              },
                              "operand": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
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
                                    ]
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "0.13"
                                  }
                                ]
                              },
                              "digits": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 2
                              },
                              "mode": "halfEven"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "average_price_to_cents",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "lines_rounding_down",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "lessThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "round",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
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
                                  ]
                                },
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.13"
                                }
                              ]
                            },
                            "digits": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            "mode": "halfEven"
                          },
                          "right": {
                            "operator": "round",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
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
                                  ]
                                },
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.13"
                                }
                              ]
                            },
                            "digits": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
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
                                ]
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0.13"
                              }
                            ]
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "rounded_price",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundHalfToEvenInAGroupedQuery().Value).TextValue
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
                      "name": "product_id",
                      "type": "uuid"
                    },
                    {
                      "name": "rounded_price",
                      "type": "long"
                    },
                    {
                      "name": "tax_rounded_once",
                      "type": "double"
                    },
                    {
                      "name": "tax_rounded_per_line",
                      "type": "double"
                    },
                    {
                      "name": "rounding_difference",
                      "type": "double"
                    },
                    {
                      "name": "average_price_to_cents",
                      "type": "double"
                    },
                    {
                      "name": "lines_rounding_down",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000000cb-0000-0000-0000-000000000000",
                      "4",
                      "2.92",
                      "2.92",
                      "0",
                      "4.5",
                      "1"
                    ],
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "10",
                      "6.49",
                      "6.5",
                      "-0.01",
                      "9.99",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundHalfToEvenInAGroupedQuery().Result).TextValue
        );
    }
}
