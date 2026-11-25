using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record FirstAndLastPerStatusOverAllRowsQueryTests
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
                  "groupBy": [
                    {
                      "alias": "status",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "last",
                      "type": {
                        "name": "decimal"
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
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "100"
                    }
                  },
                  "select": [
                    {
                      "alias": "status",
                      "expression": {
                        "key": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "first_ordered_at",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
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
                      "alias": "has_the_first_order_of_all",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "first",
                          "type": {
                            "name": "uuid"
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
                        },
                        "right": {
                          "operator": "first",
                          "type": {
                            "name": "uuid"
                          },
                          "over": "all",
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
                      }
                    },
                    {
                      "alias": "latest_total",
                      "expression": {
                        "operator": "last",
                        "type": {
                          "name": "decimal"
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
                      "alias": "latest_total_of_all",
                      "expression": {
                        "operator": "last",
                        "type": {
                          "name": "decimal"
                        },
                        "over": "all",
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
                      "alias": "slowest_shipment_id",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "uuid"
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
                              "operator": "dateDiffDays",
                              "type": {
                                "name": "integer",
                                "nullable": true
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
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            "direction": "desc"
                          },
                          {
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "integer"
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
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              },
                              "else": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
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
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "last",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
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
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FirstAndLastPerStatusOverAllRowsQuery().Value).TextValue
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "first_ordered_at",
                      "type": "datetime"
                    },
                    {
                      "name": "has_the_first_order_of_all",
                      "type": "bool"
                    },
                    {
                      "name": "latest_total",
                      "type": "double"
                    },
                    {
                      "name": "latest_total_of_all",
                      "type": "double"
                    },
                    {
                      "name": "slowest_shipment_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "pending",
                      "2024-06-02T11:00:00Z",
                      "False",
                      "100.5",
                      "100.5",
                      "00000066-0000-0000-0000-000000000000"
                    ],
                    [
                      "shipped",
                      "2024-06-01T10:00:00Z",
                      "True",
                      "300",
                      "100.5",
                      "00000069-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FirstAndLastPerStatusOverAllRowsQuery().Result).TextValue
        );
    }
}
