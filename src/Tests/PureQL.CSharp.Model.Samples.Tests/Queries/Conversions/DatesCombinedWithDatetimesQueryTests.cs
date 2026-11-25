using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record DatesCombinedWithDatetimesQueryTests
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
                      "name": "profiles",
                      "rows": [
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000001-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-01T07:00:00Z"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000002-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-02T08:45:00Z"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000003-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-05"
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000004-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-10"
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-02T21:15:00Z"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
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
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
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
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "right": {
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
                          }
                        ]
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "profiles",
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
                          "source": "profiles",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
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
                        "name": "datetime",
                        "nullable": true
                      },
                      "operand": {
                        "source": "profiles",
                        "field": "trial_start",
                        "type": {
                          "name": "date",
                          "nullable": true
                        }
                      }
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
                      "alias": "delivery_window_opens",
                      "expression": {
                        "operator": "datetimeAddSeconds",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
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
                        },
                        "seconds": {
                          "operator": "timeDiffSeconds",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "time"
                            },
                            "value": "00:00:00"
                          }
                        }
                      }
                    },
                    {
                      "alias": "seconds_from_window_to_last_login",
                      "expression": {
                        "operator": "datetimeDiffSeconds",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "operator": "datetimeAddSeconds",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
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
                          },
                          "seconds": {
                            "operator": "timeDiffSeconds",
                            "type": {
                              "name": "decimal"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "shift_start",
                              "type": {
                                "name": "time"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "time"
                              },
                              "value": "00:00:00"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "days_after_last_login",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
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
                      }
                    },
                    {
                      "alias": "ordered_before_noon",
                      "expression": {
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
                          }
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "12:00:00"
                        }
                      }
                    },
                    {
                      "alias": "midnight_of_last_seen",
                      "expression": {
                        "operator": "asDatetime",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asDate",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "operand": {
                            "source": "profiles",
                            "field": "last_seen",
                            "type": {
                              "name": "datetime",
                              "nullable": true
                            }
                          }
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
                        }
                      }
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
                """
            ).TextValue,
            new QueryJson(new DatesCombinedWithDatetimesQuery().Value).TextValue
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
                      "name": "delivery_window_opens",
                      "type": "datetime"
                    },
                    {
                      "name": "seconds_from_window_to_last_login",
                      "type": "double"
                    },
                    {
                      "name": "days_after_last_login",
                      "type": "long"
                    },
                    {
                      "name": "ordered_before_noon",
                      "type": "bool"
                    },
                    {
                      "name": "midnight_of_last_seen",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-06-01T09:00:00Z",
                      "-1800",
                      "0",
                      "True",
                      "2024-06-01T00:00:00Z"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-02T09:00:00Z",
                      "-88200",
                      "1",
                      "True",
                      "2024-06-01T00:00:00Z"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04T09:00:00Z",
                      "-414000",
                      "5",
                      "False",
                      ""
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-06T11:30:00Z",
                      "-233100",
                      "3",
                      "False",
                      "2024-06-02T00:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DatesCombinedWithDatetimesQuery().Result).TextValue
        );
    }
}
