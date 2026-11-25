using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalDatesInEveryClauseQueryTests
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
                      "kind": "inner",
                      "entity": "audit.logins",
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
                              "source": "audit.logins",
                              "field": "login_user_id",
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
                          },
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
                                  "value": "Asia/Dhaka"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/Chicago"
                                }
                              }
                            },
                            "right": {
                              "operator": "asDate",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "audit.logins",
                                "field": "login_at",
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
                                  "value": "Asia/Dhaka"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/Chicago"
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
                              "value": "Asia/Dhaka"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/Chicago"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "09:00:00"
                        }
                      },
                      {
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
                              "value": "Asia/Dhaka"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/Chicago"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "18:00:00"
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
                            "value": "Asia/Dhaka"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/Chicago"
                          }
                        }
                      }
                    },
                    {
                      "alias": "local_time",
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
                            "value": "Asia/Dhaka"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/Chicago"
                          }
                        }
                      }
                    },
                    {
                      "alias": "utc_day",
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
                      "alias": "orders_of_the_local_day",
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
                                  "value": "Asia/Dhaka"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/Chicago"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "days_to_ship",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
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
                              "value": "Asia/Dhaka"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/Chicago"
                            }
                          }
                        },
                        "right": {
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
                              "value": "Asia/Dhaka"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/Chicago"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_time_of_the_local_day",
                      "expression": {
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
                              "value": "Asia/Dhaka"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/Chicago"
                            }
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
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
                                  "value": "Asia/Dhaka"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/Chicago"
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
                            "value": "Asia/Dhaka"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/Chicago"
                          }
                        }
                      },
                      "direction": "desc"
                    },
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
                            "value": "Asia/Dhaka"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/Chicago"
                          }
                        }
                      }
                    },
                    {
                      "column": "id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalDatesInEveryClauseQuery().Value).TextValue
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
                      "name": "local_day",
                      "type": "date"
                    },
                    {
                      "name": "local_time",
                      "type": "time"
                    },
                    {
                      "name": "utc_day",
                      "type": "date"
                    },
                    {
                      "name": "orders_of_the_local_day",
                      "type": "long"
                    },
                    {
                      "name": "days_to_ship",
                      "type": "long"
                    },
                    {
                      "name": "first_time_of_the_local_day",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-02",
                      "17:00:00",
                      "2024-06-02",
                      "1",
                      "2",
                      "17:00:00"
                    ],
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-06-01",
                      "16:00:00",
                      "2024-06-01",
                      "1",
                      "1",
                      "16:00:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalDatesInEveryClauseQuery().Result).TextValue
        );
    }
}
