using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalDatesGroupedAndTotalledQueryTests
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
                      "name": "per_local_day",
                      "query": {
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
                            "alias": "zone",
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
                                "value": "America/Los_Angeles"
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
                            "alias": "local_day",
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
                              },
                              "zone": {
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
                                  "value": "America/Los_Angeles"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
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
                                },
                                "zone": {
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
                                    "value": "America/Los_Angeles"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Asia/Tokyo"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "time"
                                },
                                "value": "06:00:00"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        },
                        "select": [
                          {
                            "alias": "zone",
                            "expression": {
                              "key": "zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          {
                            "alias": "local_day",
                            "expression": {
                              "key": "local_day",
                              "type": {
                                "name": "date"
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
                            "alias": "night_orders",
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
                                  },
                                  "zone": {
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
                                      "value": "America/Los_Angeles"
                                    },
                                    "else": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "Asia/Tokyo"
                                    }
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "time"
                                  },
                                  "value": "06:00:00"
                                }
                              }
                            }
                          },
                          {
                            "alias": "latest_local_time",
                            "expression": {
                              "operator": "max",
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
                                },
                                "zone": {
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
                                    "value": "America/Los_Angeles"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Asia/Tokyo"
                                  }
                                }
                              }
                            }
                          },
                          {
                            "alias": "local_days_shipped_on",
                            "expression": {
                              "operator": "countDistinct",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "operator": "asDate",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "operator": "datetimeAddSeconds",
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
                                  "seconds": {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "111600"
                                  }
                                },
                                "zone": {
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
                                    "value": "America/Los_Angeles"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Asia/Tokyo"
                                  }
                                }
                              }
                            }
                          },
                          {
                            "alias": "next_local_day",
                            "expression": {
                              "operator": "dateAddDays",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "key": "local_day",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "days": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
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
                            },
                            "zone": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-05"
                          }
                        },
                        "aggregate": [
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
                            "alias": "first_local_time",
                            "expression": {
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
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            }
                          },
                          {
                            "alias": "last_local_day_shipped",
                            "expression": {
                              "operator": "max",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "selector": {
                                "operator": "asDate",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "operator": "datetimeAddSeconds",
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
                                  "seconds": {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "111600"
                                  }
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            }
                          },
                          {
                            "alias": "median_local_time",
                            "expression": {
                              "operator": "median",
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
                                },
                                "zone": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "per_local_day"
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
                      "alias": "zone",
                      "expression": {
                        "source": "per_local_day",
                        "field": "zone",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "local_day",
                      "expression": {
                        "source": "per_local_day",
                        "field": "local_day",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "source": "per_local_day",
                        "field": "orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "orders_of_the_day",
                      "expression": {
                        "source": "one_day",
                        "field": "orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "night_orders",
                      "expression": {
                        "source": "per_local_day",
                        "field": "night_orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "latest_local_time",
                      "expression": {
                        "source": "per_local_day",
                        "field": "latest_local_time",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "local_days_shipped_on",
                      "expression": {
                        "source": "per_local_day",
                        "field": "local_days_shipped_on",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "next_local_day",
                      "expression": {
                        "source": "per_local_day",
                        "field": "next_local_day",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "first_local_time",
                      "expression": {
                        "source": "one_day",
                        "field": "first_local_time",
                        "type": {
                          "name": "time",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "last_local_day_shipped",
                      "expression": {
                        "source": "one_day",
                        "field": "last_local_day_shipped",
                        "type": {
                          "name": "date",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "median_local_time",
                      "expression": {
                        "source": "one_day",
                        "field": "median_local_time",
                        "type": {
                          "name": "time",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalDatesGroupedAndTotalledQuery().Value).TextValue
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
                      "name": "zone",
                      "type": "string"
                    },
                    {
                      "name": "local_day",
                      "type": "date"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "orders_of_the_day",
                      "type": "long"
                    },
                    {
                      "name": "night_orders",
                      "type": "long"
                    },
                    {
                      "name": "latest_local_time",
                      "type": "time"
                    },
                    {
                      "name": "local_days_shipped_on",
                      "type": "long"
                    },
                    {
                      "name": "next_local_day",
                      "type": "date"
                    },
                    {
                      "name": "first_local_time",
                      "type": "time"
                    },
                    {
                      "name": "last_local_day_shipped",
                      "type": "date"
                    },
                    {
                      "name": "median_local_time",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "America/Los_Angeles",
                      "2024-06-01",
                      "1",
                      "1",
                      "1",
                      "03:00:00",
                      "1",
                      "2024-06-02",
                      "23:00:00",
                      "2024-06-07",
                      "23:00:00"
                    ],
                    [
                      "America/Los_Angeles",
                      "2024-06-02",
                      "1",
                      "1",
                      "1",
                      "04:00:00",
                      "1",
                      "2024-06-03",
                      "23:00:00",
                      "2024-06-07",
                      "23:00:00"
                    ],
                    [
                      "America/Los_Angeles",
                      "2024-06-03",
                      "1",
                      "1",
                      "1",
                      "05:00:00",
                      "1",
                      "2024-06-04",
                      "23:00:00",
                      "2024-06-07",
                      "23:00:00"
                    ],
                    [
                      "Asia/Tokyo",
                      "2024-06-07",
                      "1",
                      "1",
                      "1",
                      "00:00:00",
                      "1",
                      "2024-06-08",
                      "23:00:00",
                      "2024-06-07",
                      "23:00:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalDatesGroupedAndTotalledQuery().Result).TextValue
        );
    }
}
