using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalInstantsInEveryClauseQueryTests
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
                            "alias": "preferred_call_time",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "shift_start",
                              "type": {
                                "name": "time"
                              }
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "date"
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
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-05"
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
                              "operator": "asDatetime",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "members",
                                "field": "birth_date",
                                "type": {
                                  "name": "date"
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
                          "operator": "asDatetime",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-02"
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
                          "operator": "asDatetime",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-08"
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
                      "alias": "local_midnight",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
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
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
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
                      "alias": "utc_midnight",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
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
                      }
                    },
                    {
                      "alias": "call_time_on_the_order_day",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
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
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "time": {
                          "source": "members",
                          "field": "preferred_call_time",
                          "type": {
                            "name": "time"
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
                      "alias": "ordered_before_the_call_time",
                      "expression": {
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
                              "source": "members",
                              "field": "time_zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "time": {
                            "source": "members",
                            "field": "preferred_call_time",
                            "type": {
                              "name": "time"
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
                      "alias": "orders_since_local_midnight",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
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
                              "operator": "asDate",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2024-06-05T02:00:00Z"
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
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
                      }
                    },
                    {
                      "alias": "latest_window",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "operator": "datetimeFromParts",
                          "type": {
                            "name": "datetime"
                          },
                          "date": {
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
                          },
                          "time": {
                            "operator": "if",
                            "type": {
                              "name": "time"
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
                                "name": "time"
                              },
                              "value": "09:00:00"
                            },
                            "else": {
                              "type": {
                                "name": "time"
                              },
                              "value": "14:00:00"
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
                      "alias": "orders_of_the_window",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "datetimeFromParts",
                              "type": {
                                "name": "datetime"
                              },
                              "date": {
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
                              },
                              "time": {
                                "operator": "if",
                                "type": {
                                  "name": "time"
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
                                    "name": "time"
                                  },
                                  "value": "09:00:00"
                                },
                                "else": {
                                  "type": {
                                    "name": "time"
                                  },
                                  "value": "14:00:00"
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
                        },
                        "time": {
                          "operator": "if",
                          "type": {
                            "name": "time"
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
                              "name": "time"
                            },
                            "value": "09:00:00"
                          },
                          "else": {
                            "type": {
                              "name": "time"
                            },
                            "value": "14:00:00"
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
            new QueryJson(new LocalInstantsInEveryClauseQuery().Value).TextValue
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
                      "name": "local_midnight",
                      "type": "datetime"
                    },
                    {
                      "name": "utc_midnight",
                      "type": "datetime"
                    },
                    {
                      "name": "call_time_on_the_order_day",
                      "type": "datetime"
                    },
                    {
                      "name": "ordered_before_the_call_time",
                      "type": "bool"
                    },
                    {
                      "name": "orders_since_local_midnight",
                      "type": "long"
                    },
                    {
                      "name": "latest_window",
                      "type": "datetime"
                    },
                    {
                      "name": "orders_of_the_window",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-02T04:00:00Z",
                      "2024-06-02T00:00:00Z",
                      "2024-06-02T13:00:00Z",
                      "True",
                      "3",
                      "2024-06-01T18:00:00Z",
                      "2"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04T04:00:00Z",
                      "2024-06-04T00:00:00Z",
                      "2024-06-04T13:00:00Z",
                      "False",
                      "3",
                      "2024-06-01T18:00:00Z",
                      "2"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05T04:00:00Z",
                      "2024-06-05T00:00:00Z",
                      "2024-06-05T13:00:00Z",
                      "False",
                      "3",
                      "2024-06-01T18:00:00Z",
                      "1"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00Z",
                      "2024-06-06T00:00:00Z",
                      "2024-06-07T02:30:00Z",
                      "True",
                      "3",
                      "2024-06-01T18:00:00Z",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalInstantsInEveryClauseQuery().Result).TextValue
        );
    }
}
