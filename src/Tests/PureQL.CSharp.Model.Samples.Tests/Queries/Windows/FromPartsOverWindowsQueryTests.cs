using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record FromPartsOverWindowsQueryTests
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
                    },
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "alias": "shipped",
                      "on": {
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
                              "source": "shipped",
                              "field": "order_id",
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
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "shipped",
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
                          }
                        ]
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "month",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "month": {
                          "operator": "month",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "orders_in_month",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "dateFromParts",
                              "type": {
                                "name": "date"
                              },
                              "year": {
                                "operator": "year",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              },
                              "month": {
                                "operator": "month",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              },
                              "day": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "position_by_window",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
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
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "shift_start",
                                  "type": {
                                    "name": "time"
                                  }
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
                      "alias": "previous_window_start",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
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
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "shift_start",
                                  "type": {
                                    "name": "time"
                                  }
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
                      "alias": "seconds_to_window",
                      "expression": {
                        "operator": "datetimeDiffSeconds",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "window_on_shipping_day",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "source": "shipped",
                          "field": "placed_on",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        },
                        "time": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
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
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
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
                """
            ).TextValue,
            new QueryJson(new FromPartsOverWindowsQuery().Value).TextValue
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
                      "name": "month",
                      "type": "date"
                    },
                    {
                      "name": "orders_in_month",
                      "type": "long"
                    },
                    {
                      "name": "position_by_window",
                      "type": "long"
                    },
                    {
                      "name": "previous_window_start",
                      "type": "datetime"
                    },
                    {
                      "name": "seconds_to_window",
                      "type": "double"
                    },
                    {
                      "name": "window_on_shipping_day",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "6",
                      "1",
                      "",
                      "-3600",
                      "2024-06-01T09:00:00Z"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "6",
                      "1",
                      "2024-06-01T09:00:00Z",
                      "-7200",
                      ""
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "6",
                      "1",
                      "2024-06-02T09:00:00Z",
                      "-7200",
                      "2024-06-03T10:00:00Z"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "6",
                      "1",
                      "2024-06-03T10:00:00Z",
                      "-14400",
                      ""
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "6",
                      "1",
                      "2024-06-04T09:00:00Z",
                      "-18000",
                      "2024-06-05T09:00:00Z"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "6",
                      "1",
                      "2024-06-05T09:00:00Z",
                      "-12600",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FromPartsOverWindowsQuery().Result).TextValue
        );
    }
}
