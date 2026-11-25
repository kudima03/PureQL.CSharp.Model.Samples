using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record RatiosGuardedAgainstZeroDivisorsPerProductQueryTests
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
                    }
                  ],
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
                      "alias": "return_rate",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
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
                            },
                            "predicate": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000386-0000-0000-0000-000000000000"
                              }
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
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
                              },
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
                                  "value": 2
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "return_rate_or_zero",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "divide",
                            "type": {
                              "name": "decimal",
                              "nullable": true
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
                                },
                                "predicate": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000386-0000-0000-0000-000000000000"
                                  }
                                }
                              },
                              {
                                "operator": "nullIfEqual",
                                "type": {
                                  "name": "integer",
                                  "nullable": true
                                },
                                "left": {
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
                                  },
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
                                      "value": 2
                                    }
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 0
                                }
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "full_boxes",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
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
                        "right": {
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "left": {
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
                            },
                            "predicate": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000386-0000-0000-0000-000000000000"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "loose_units",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
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
                        "right": {
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "left": {
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
                            },
                            "predicate": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000386-0000-0000-0000-000000000000"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "share_of_all_units",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
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
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
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
                              },
                              "over": "all"
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "divide",
                            "type": {
                              "name": "decimal",
                              "nullable": true
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
                                },
                                "predicate": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000386-0000-0000-0000-000000000000"
                                  }
                                }
                              },
                              {
                                "operator": "nullIfEqual",
                                "type": {
                                  "name": "integer",
                                  "nullable": true
                                },
                                "left": {
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
                                  },
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
                                      "value": 2
                                    }
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 0
                                }
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          }
                        ]
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "product_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new RatiosGuardedAgainstZeroDivisorsPerProductQuery().Value
            ).TextValue
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
                      "name": "return_rate",
                      "type": "double"
                    },
                    {
                      "name": "return_rate_or_zero",
                      "type": "double"
                    },
                    {
                      "name": "full_boxes",
                      "type": "long"
                    },
                    {
                      "name": "loose_units",
                      "type": "long"
                    },
                    {
                      "name": "share_of_all_units",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000000c9-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "1",
                      "2",
                      "0.45454545454545453"
                    ],
                    [
                      "000000ca-0000-0000-0000-000000000000",
                      "",
                      "0",
                      "",
                      "",
                      "0.09090909090909091"
                    ],
                    [
                      "000000cb-0000-0000-0000-000000000000",
                      "0",
                      "0",
                      "",
                      "",
                      "0.45454545454545453"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new RatiosGuardedAgainstZeroDivisorsPerProductQuery().Result
            ).TextValue
        );
    }
}
