using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record OperatorsOverAListInEveryContextQueryTests
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
                      "name": "per_order",
                      "query": {
                        "where": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                                "operator": "modulo",
                                "type": {
                                  "name": "integer"
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
                                  "value": 2
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            ]
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        },
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
                          },
                          {
                            "alias": "units",
                            "expression": {
                              "operator": "add",
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
                                  "operator": "modulo",
                                  "type": {
                                    "name": "integer"
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
                                    "value": 2
                                  }
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
                        ],
                        "having": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "add",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "sum",
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
                              {
                                "operator": "count",
                                "type": {
                                  "name": "integer"
                                }
                              },
                              {
                                "key": "units",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            ]
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "13.5"
                          }
                        },
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
                            "alias": "weighted",
                            "expression": {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
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
                                      },
                                      {
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "value": "0.2"
                                      }
                                    ]
                                  }
                                },
                                {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                {
                                  "key": "units",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              ]
                            }
                          },
                          {
                            "alias": "share",
                            "expression": {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "operator": "sum",
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
                                {
                                  "operator": "sum",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_price",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "over": "all"
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 100
                                }
                              ]
                            }
                          }
                        ],
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
                        ]
                      }
                    },
                    {
                      "name": "totals",
                      "query": {
                        "aggregate": [
                          {
                            "alias": "units",
                            "expression": {
                              "operator": "subtract",
                              "type": {
                                "name": "integer"
                              },
                              "values": [
                                {
                                  "operator": "sum",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "selector": {
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
                                  "operator": "sum",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "selector": {
                                    "operator": "modulo",
                                    "type": {
                                      "name": "integer"
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
                                      "value": 2
                                    }
                                  }
                                },
                                {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              ]
                            }
                          },
                          {
                            "alias": "spread",
                            "expression": {
                              "operator": "subtract",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "values": [
                                {
                                  "operator": "max",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_price",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                },
                                {
                                  "operator": "min",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.products",
                                    "field": "product_price",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                },
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.01"
                                }
                              ]
                            }
                          }
                        ],
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
                        ]
                      }
                    }
                  ],
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
                      "subquery": "per_order",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "per_order",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "inner",
                      "subquery": "totals",
                      "on": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "add",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "source": "totals",
                              "field": "units",
                              "type": {
                                "name": "integer"
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
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    }
                  ],
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
                      "alias": "running",
                      "expression": {
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
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.2"
                            }
                          ]
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "change",
                      "expression": {
                        "operator": "subtract",
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
                            "operator": "previous",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.products",
                              "field": "product_price",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "default": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            },
                            "over": {
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
                          },
                          {
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "weighted",
                      "expression": {
                        "source": "per_order",
                        "field": "weighted",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ],
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
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.2"
                          }
                        ]
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
                  "pagination": {
                    "take": 20
                  }
                }
                """
            ).TextValue,
            new QueryJson(new OperatorsOverAListInEveryContextQuery().Value).TextValue
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
                      "name": "running",
                      "type": "double"
                    },
                    {
                      "name": "change",
                      "type": "double"
                    },
                    {
                      "name": "weighted",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "5.994",
                      "-23.323333333333334",
                      "1.1988"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "7.994",
                      "-3.3233333333333333",
                      "1.332"
                    ],
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "3.996",
                      "-3.3333333333333335",
                      "1.332"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OperatorsOverAListInEveryContextQuery().Result).TextValue
        );
    }
}
