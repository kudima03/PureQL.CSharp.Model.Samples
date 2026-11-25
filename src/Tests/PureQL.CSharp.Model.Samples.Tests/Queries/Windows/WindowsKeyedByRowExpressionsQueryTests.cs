using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record WindowsKeyedByRowExpressionsQueryTests
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
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.users",
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
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "payer",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "notEqual",
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
                              "value": "cancelled"
                            }
                          }
                        ]
                      },
                      "alias": "payer"
                    }
                  ],
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
                      "alias": "month",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "date"
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
                      "alias": "size",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "100"
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "large"
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": "small"
                        }
                      }
                    },
                    {
                      "alias": "month_total",
                      "expression": {
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
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "orders_of_size_in_year",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
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
                            {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "greaterThanOrEqual",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "100"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "large"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "small"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rank_in_month",
                      "expression": {
                        "operator": "rank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
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
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rank_by_rounded_total",
                      "expression": {
                        "operator": "denseRank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
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
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "position_by_weekday_then_time",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "dayOfWeek",
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
                            }
                          ],
                          "orderBy": [
                            {
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
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
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
                      }
                    },
                    {
                      "alias": "position_within_literal",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "everyone"
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
                      }
                    },
                    {
                      "alias": "running_total_by_status",
                      "expression": {
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
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "coalesce",
                                "type": {
                                  "name": "date"
                                },
                                "values": [
                                  {
                                    "source": "payer",
                                    "field": "signup_date",
                                    "type": {
                                      "name": "date",
                                      "nullable": true
                                    }
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_on",
                                    "type": {
                                      "name": "date"
                                    }
                                  }
                                ]
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
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "previous_in_tenant",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "payer",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "payer",
                                "field": "signup_date",
                                "type": {
                                  "name": "date",
                                  "nullable": true
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
                      "alias": "third_by_total",
                      "expression": {
                        "operator": "ntile",
                        "type": {
                          "name": "integer"
                        },
                        "buckets": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
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
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
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
            new QueryJson(new WindowsKeyedByRowExpressionsQuery().Value).TextValue
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
                      "name": "month",
                      "type": "date"
                    },
                    {
                      "name": "size",
                      "type": "string"
                    },
                    {
                      "name": "month_total",
                      "type": "double"
                    },
                    {
                      "name": "orders_of_size_in_year",
                      "type": "long"
                    },
                    {
                      "name": "rank_in_month",
                      "type": "long"
                    },
                    {
                      "name": "rank_by_rounded_total",
                      "type": "long"
                    },
                    {
                      "name": "position_by_weekday_then_time",
                      "type": "long"
                    },
                    {
                      "name": "position_within_literal",
                      "type": "long"
                    },
                    {
                      "name": "running_total_by_status",
                      "type": "double"
                    },
                    {
                      "name": "previous_in_tenant",
                      "type": "double"
                    },
                    {
                      "name": "third_by_total",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "large",
                      "826.25",
                      "4",
                      "3",
                      "3",
                      "1",
                      "1",
                      "400.5",
                      "300",
                      "2"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "small",
                      "826.25",
                      "2",
                      "6",
                      "5",
                      "1",
                      "2",
                      "50",
                      "100.5",
                      "1"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "large",
                      "826.25",
                      "4",
                      "2",
                      "2",
                      "1",
                      "3",
                      "600.5",
                      "50",
                      "3"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "small",
                      "826.25",
                      "2",
                      "5",
                      "4",
                      "1",
                      "4",
                      "75.25",
                      "",
                      "1"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "large",
                      "826.25",
                      "4",
                      "1",
                      "1",
                      "1",
                      "5",
                      "300",
                      "",
                      "3"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "large",
                      "826.25",
                      "4",
                      "3",
                      "3",
                      "1",
                      "6",
                      "150.5",
                      "",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WindowsKeyedByRowExpressionsQuery().Result).TextValue
        );
    }
}
