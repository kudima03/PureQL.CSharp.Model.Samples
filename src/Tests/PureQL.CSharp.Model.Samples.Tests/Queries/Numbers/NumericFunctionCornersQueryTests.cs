using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record NumericFunctionCornersQueryTests
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
                    }
                  ],
                  "select": [
                    {
                      "alias": "abs_integer_nullable",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "if",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
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
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          "then": {
                            "operator": "subtract",
                            "type": {
                              "name": "integer"
                            },
                            "values": [
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
                              },
                              {
                                "operator": "round",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "operator": "divide",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_total",
                                      "type": {
                                        "name": "decimal"
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "value": "50"
                                    }
                                  ]
                                }
                              }
                            ]
                          },
                          "else": {
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "abs_integer_literal",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -9223372036854775807
                        }
                      }
                    },
                    {
                      "alias": "abs_decimal_literal",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "-0.5"
                        }
                      }
                    },
                    {
                      "alias": "abs_of_mixed_sum",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "add",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
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
                            },
                            {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "sign_of_integer",
                      "expression": {
                        "operator": "sign",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
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
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "50"
                                  }
                                ]
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "sign_of_nullable",
                      "expression": {
                        "operator": "sign",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "if",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
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
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          "then": {
                            "operator": "divide",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "subtract",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "divide",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_total",
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
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "50"
                                  }
                                ]
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "3"
                              }
                            ]
                          },
                          "else": {
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "truncate_of_integer_with_digits",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "truncate_to_tens",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
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
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "truncate_to_two_places",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
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
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "truncate_to_computed",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "if",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
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
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          "then": {
                            "operator": "divide",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "subtract",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "divide",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_total",
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
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "50"
                                  }
                                ]
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "3"
                              }
                            ]
                          },
                          "else": {
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "digits": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "nested",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "truncate",
                          "type": {
                            "name": "decimal"
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
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
                              },
                              {
                                "operator": "sign",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "operator": "subtract",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "values": [
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
                                    },
                                    {
                                      "operator": "round",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "operand": {
                                        "operator": "divide",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "source": "schema_with_foreign_keys.orders",
                                            "field": "order_total",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          },
                                          {
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "value": "50"
                                          }
                                        ]
                                      }
                                    }
                                  ]
                                }
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "rounded",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "75.5"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "floored",
                      "expression": {
                        "operator": "floor",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "75.5"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "truncated",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "75.5"
                            }
                          ]
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NumericFunctionCornersQuery().Value).TextValue
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
                      "name": "abs_integer_nullable",
                      "type": "long"
                    },
                    {
                      "name": "abs_integer_literal",
                      "type": "long"
                    },
                    {
                      "name": "abs_decimal_literal",
                      "type": "double"
                    },
                    {
                      "name": "abs_of_mixed_sum",
                      "type": "double"
                    },
                    {
                      "name": "sign_of_integer",
                      "type": "long"
                    },
                    {
                      "name": "sign_of_nullable",
                      "type": "long"
                    },
                    {
                      "name": "truncate_of_integer_with_digits",
                      "type": "long"
                    },
                    {
                      "name": "truncate_to_tens",
                      "type": "double"
                    },
                    {
                      "name": "truncate_to_two_places",
                      "type": "double"
                    },
                    {
                      "name": "truncate_to_computed",
                      "type": "double"
                    },
                    {
                      "name": "nested",
                      "type": "double"
                    },
                    {
                      "name": "rounded",
                      "type": "long"
                    },
                    {
                      "name": "floored",
                      "type": "long"
                    },
                    {
                      "name": "truncated",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "",
                      "9223372036854775807",
                      "0.5",
                      "101.5",
                      "-1",
                      "",
                      "100",
                      "100",
                      "100.5",
                      "",
                      "100.5",
                      "25",
                      "25",
                      "25"
                    ],
                    [
                      "",
                      "9223372036854775807",
                      "0.5",
                      "52.25",
                      "0",
                      "",
                      "100",
                      "50",
                      "50.25",
                      "",
                      "0",
                      "-25",
                      "-26",
                      "-25"
                    ],
                    [
                      "1",
                      "9223372036854775807",
                      "0.5",
                      "45",
                      "1",
                      "-1",
                      "200",
                      "40",
                      "40",
                      "-3.3",
                      "40",
                      "-36",
                      "-36",
                      "-35"
                    ],
                    [
                      "3",
                      "9223372036854775807",
                      "0.5",
                      "103",
                      "-1",
                      "1",
                      "300",
                      "100",
                      "100",
                      "16.6",
                      "100",
                      "25",
                      "24",
                      "24"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NumericFunctionCornersQuery().Result).TextValue
        );
    }
}
