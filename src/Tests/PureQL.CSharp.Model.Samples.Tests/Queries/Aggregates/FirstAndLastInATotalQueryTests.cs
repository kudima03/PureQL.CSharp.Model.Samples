using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record FirstAndLastInATotalQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "00000003-0000-0000-0000-000000000000"
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "first_order_id",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "latest_total",
                      "expression": {
                        "operator": "last",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "largest_order_status",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "direction": "desc"
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "earliest_shipped",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          },
                          "then": {
                            "operator": "dateAddDays",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "days": {
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
                                    "value": "100"
                                  }
                                ]
                              }
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              },
                              "then": {
                                "operator": "dateAddDays",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "days": {
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
                                        "value": "100"
                                      }
                                    ]
                                  }
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              }
                            }
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "if",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "shipped"
                              }
                            },
                            "then": {
                              "operator": "dateAddDays",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "days": {
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
                                      "value": "100"
                                    }
                                  ]
                                }
                              }
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "null_while_any_is_unshipped",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          },
                          "then": {
                            "operator": "dateAddDays",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "days": {
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
                                    "value": "100"
                                  }
                                ]
                              }
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              },
                              "then": {
                                "operator": "dateAddDays",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "days": {
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
                                        "value": "100"
                                      }
                                    ]
                                  }
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              }
                            }
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "days_between_first_and_last",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "last",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        },
                        "right": {
                          "operator": "first",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FirstAndLastInATotalQuery().Value).TextValue
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
                      "name": "first_order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "latest_total",
                      "type": "double"
                    },
                    {
                      "name": "largest_order_status",
                      "type": "string"
                    },
                    {
                      "name": "earliest_shipped",
                      "type": "date"
                    },
                    {
                      "name": "null_while_any_is_unshipped",
                      "type": "date"
                    },
                    {
                      "name": "days_between_first_and_last",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "300",
                      "shipped",
                      "2024-06-08",
                      "",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FirstAndLastInATotalQuery().Result).TextValue
        );
    }
}
