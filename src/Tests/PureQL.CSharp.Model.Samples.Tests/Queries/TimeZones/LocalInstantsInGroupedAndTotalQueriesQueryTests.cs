using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalInstantsInGroupedAndTotalQueriesQueryTests
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
                      "name": "by_day",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "order_date",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
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
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                          },
                          "right": {
                            "operator": "asDatetime",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "key": "order_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Pacific/Pago_Pago"
                            }
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
                            "alias": "local_midnight_of_the_key",
                            "expression": {
                              "operator": "asDatetime",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "key": "order_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          },
                          {
                            "alias": "local_noon_of_the_key",
                            "expression": {
                              "operator": "datetimeFromParts",
                              "type": {
                                "name": "datetime"
                              },
                              "date": {
                                "key": "order_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "time": {
                                "type": {
                                  "name": "time"
                                },
                                "value": "12:00:00"
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Europe/Moscow"
                              }
                            }
                          },
                          {
                            "alias": "earliest_window",
                            "expression": {
                              "operator": "datetimeFromParts",
                              "type": {
                                "name": "datetime"
                              },
                              "date": {
                                "key": "order_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "time": {
                                "operator": "min",
                                "type": {
                                  "name": "time"
                                },
                                "selector": {
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
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          },
                          {
                            "alias": "orders_before_local_noon",
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
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "right": {
                                  "operator": "datetimeFromParts",
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "date": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_on",
                                    "type": {
                                      "name": "date"
                                    }
                                  },
                                  "time": {
                                    "type": {
                                      "name": "time"
                                    },
                                    "value": "12:00:00"
                                  },
                                  "zone": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Pacific/Pago_Pago"
                                  }
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "one_day",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "and",
                          "type": {
                            "name": "boolean"
                          },
                          "conditions": [
                            {
                              "operator": "greaterThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "right": {
                                "operator": "asDatetime",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-03"
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Pago_Pago"
                                }
                              }
                            },
                            {
                              "operator": "lessThan",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "right": {
                                "operator": "asDatetime",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "operator": "dateAddDays",
                                  "type": {
                                    "name": "date"
                                  },
                                  "operand": {
                                    "type": {
                                      "name": "date"
                                    },
                                    "value": "2024-06-03"
                                  },
                                  "days": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 1
                                  }
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Pago_Pago"
                                }
                              }
                            }
                          ]
                        },
                        "aggregate": [
                          {
                            "alias": "orders_of_the_local_day",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "before_the_cutoff",
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
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "right": {
                                  "operator": "datetimeFromParts",
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "date": {
                                    "type": {
                                      "name": "date"
                                    },
                                    "value": "2024-06-03"
                                  },
                                  "time": {
                                    "type": {
                                      "name": "time"
                                    },
                                    "value": "18:00:00"
                                  },
                                  "zone": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Pacific/Pago_Pago"
                                  }
                                }
                              }
                            }
                          },
                          {
                            "alias": "first_window",
                            "expression": {
                              "operator": "datetimeFromParts",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "date": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-03"
                              },
                              "time": {
                                "operator": "min",
                                "type": {
                                  "name": "time",
                                  "nullable": true
                                },
                                "selector": {
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
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          },
                          {
                            "alias": "local_midnight_of_the_last_order_date",
                            "expression": {
                              "operator": "asDatetime",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "operand": {
                                "operator": "max",
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
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Pago_Pago"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "by_day"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "one_day",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "status",
                      "expression": {
                        "source": "by_day",
                        "field": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "local_midnight",
                      "expression": {
                        "source": "by_day",
                        "field": "local_midnight_of_the_key",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "orders_of_the_local_day",
                      "expression": {
                        "source": "one_day",
                        "field": "orders_of_the_local_day",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new LocalInstantsInGroupedAndTotalQueriesQuery().Value
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "local_midnight",
                      "type": "datetime"
                    },
                    {
                      "name": "orders_of_the_local_day",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "2024-06-04T11:00:00Z",
                      "1"
                    ],
                    [
                      "pending",
                      "2024-06-02T11:00:00Z",
                      "1"
                    ],
                    [
                      "pending",
                      "2024-06-06T11:00:00Z",
                      "1"
                    ],
                    [
                      "shipped",
                      "2024-06-03T11:00:00Z",
                      "1"
                    ],
                    [
                      "shipped",
                      "2024-06-05T11:00:00Z",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new LocalInstantsInGroupedAndTotalQueriesQuery().Result
            ).TextValue
        );
    }
}
