using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalPeriodStartsInEveryClauseQueryTests
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
                      "name": "members",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.users"
                        },
                        "select": [
                          {
                            "alias": "id",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "time_zone",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          {
                            "alias": "created_at",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "datetime"
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "last_login",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "else": {
                                "operator": "datetimeAddMonths",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "last_login",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "months": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 1
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "members",
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
                              "source": "members",
                              "field": "id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
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
                              "operator": "startOfMonth",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "members",
                                "field": "created_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
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
                          "operator": "startOfDay",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-02T12:00:00Z"
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
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
                          "operator": "startOfHour",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-06T20:30:00Z"
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    ]
                  },
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
                      "alias": "local_day_start",
                      "expression": {
                        "operator": "startOfDay",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "utc_day_start",
                      "expression": {
                        "operator": "startOfDay",
                        "type": {
                          "name": "datetime"
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
                      "alias": "seconds_into_the_local_day",
                      "expression": {
                        "operator": "datetimeDiffSeconds",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "operator": "startOfDay",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_of_the_local_week",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            {
                              "operator": "startOfWeek",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "first_local_quarter",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "operator": "startOfQuarter",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "revenue_of_the_last_seven_local_days",
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
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "startOfDay",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "source": "members",
                                  "field": "time_zone",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "seconds": 518400
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "startOfDay",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "column": "id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalPeriodStartsInEveryClauseQuery().Value).TextValue
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
                      "name": "local_day_start",
                      "type": "datetime"
                    },
                    {
                      "name": "utc_day_start",
                      "type": "datetime"
                    },
                    {
                      "name": "seconds_into_the_local_day",
                      "type": "double"
                    },
                    {
                      "name": "orders_of_the_local_week",
                      "type": "long"
                    },
                    {
                      "name": "first_local_quarter",
                      "type": "datetime"
                    },
                    {
                      "name": "revenue_of_the_last_seven_local_days",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00Z",
                      "2024-06-06T00:00:00Z",
                      "0",
                      "1",
                      "2024-03-31T15:00:00Z",
                      "100.5"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05T04:00:00Z",
                      "2024-06-05T00:00:00Z",
                      "36000",
                      "2",
                      "2024-03-31T15:00:00Z",
                      "375.25"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04T04:00:00Z",
                      "2024-06-04T00:00:00Z",
                      "32400",
                      "2",
                      "2024-03-31T15:00:00Z",
                      "75.25"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-02T04:00:00Z",
                      "2024-06-02T00:00:00Z",
                      "25200",
                      "1",
                      "2024-03-31T15:00:00Z",
                      "50"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalPeriodStartsInEveryClauseQuery().Result).TextValue
        );
    }
}
