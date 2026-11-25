using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record OrdersPerDayOfADatetimeQueryTests
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
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
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
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "day",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "active",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
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
                      "operator": "asTime",
                      "type": {
                        "name": "time"
                      },
                      "operand": {
                        "operator": "max",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "time"
                      },
                      "value": "11:30:00"
                    }
                  },
                  "select": [
                    {
                      "alias": "day",
                      "expression": {
                        "key": "day",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "active",
                      "expression": {
                        "key": "active",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "title",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "key": "day",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ": "
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": " orders, "
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "first_order_time",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "datetime"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "day_start",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "key": "day",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "afternoon_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "and",
                          "type": {
                            "name": "boolean"
                          },
                          "conditions": [
                            {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "asDate",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
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
                            {
                              "operator": "greaterThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "asTime",
                                "type": {
                                  "name": "time"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "time"
                                },
                                "value": "13:00:00"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "longest_total",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "length",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "day",
                        "type": {
                          "name": "date"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "active",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrdersPerDayOfADatetimeQuery().Value).TextValue
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
                      "name": "day",
                      "type": "date"
                    },
                    {
                      "name": "active",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "title",
                      "type": "string"
                    },
                    {
                      "name": "first_order_time",
                      "type": "time"
                    },
                    {
                      "name": "day_start",
                      "type": "datetime"
                    },
                    {
                      "name": "afternoon_orders",
                      "type": "long"
                    },
                    {
                      "name": "longest_total",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-03",
                      "true",
                      "1",
                      "2024-06-03: 1 orders, 100.5",
                      "15:00:00",
                      "2024-06-03T00:00:00Z",
                      "1",
                      "5"
                    ],
                    [
                      "2024-06-02",
                      "false",
                      "1",
                      "2024-06-02: 1 orders, 200",
                      "12:00:00",
                      "2024-06-02T00:00:00Z",
                      "0",
                      "3"
                    ],
                    [
                      "2024-05-30",
                      "true",
                      "2",
                      "2024-05-30: 2 orders, 375.25",
                      "13:00:00",
                      "2024-05-30T00:00:00Z",
                      "2",
                      "5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrdersPerDayOfADatetimeQuery().Result).TextValue
        );
    }
}
