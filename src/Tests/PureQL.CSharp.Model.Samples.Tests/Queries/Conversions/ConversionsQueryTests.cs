using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record ConversionsQueryTests
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
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThanOrEqual",
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
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-05"
                    }
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "Order of "
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
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
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "user_active_text",
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
                    },
                    {
                      "alias": "shipped_on_text",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
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
                      }
                    },
                    {
                      "alias": "placed_day",
                      "expression": {
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
                      }
                    },
                    {
                      "alias": "placed_time",
                      "expression": {
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
                    {
                      "alias": "placed_day_start",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "logged_in_on_the_order_day",
                      "expression": {
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
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
                      }
                    }
                  ],
                  "orderBy": [
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
                """
            ).TextValue,
            new QueryJson(new ConversionsQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "user_active_text",
                      "type": "string"
                    },
                    {
                      "name": "shipped_on_text",
                      "type": "string"
                    },
                    {
                      "name": "placed_day",
                      "type": "date"
                    },
                    {
                      "name": "placed_time",
                      "type": "time"
                    },
                    {
                      "name": "placed_day_start",
                      "type": "datetime"
                    },
                    {
                      "name": "logged_in_on_the_order_day",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "Order of 2024-06-01: 100.5",
                      "true",
                      "2024-06-01",
                      "2024-06-01",
                      "10:00:00",
                      "2024-06-01T00:00:00Z",
                      "True"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "Order of 2024-06-02: 50",
                      "true",
                      "",
                      "2024-06-02",
                      "11:00:00",
                      "2024-06-02T00:00:00Z",
                      "False"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "Order of 2024-06-03: 200",
                      "false",
                      "2024-06-03",
                      "2024-06-03",
                      "12:00:00",
                      "2024-06-03T00:00:00Z",
                      "False"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "Order of 2024-06-04: 75.25",
                      "true",
                      "",
                      "2024-06-04",
                      "13:00:00",
                      "2024-06-04T00:00:00Z",
                      "False"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "Order of 2024-06-05: 300",
                      "true",
                      "2024-06-05",
                      "2024-06-05",
                      "14:00:00",
                      "2024-06-05T00:00:00Z",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConversionsQuery().Result).TextValue
        );
    }
}
