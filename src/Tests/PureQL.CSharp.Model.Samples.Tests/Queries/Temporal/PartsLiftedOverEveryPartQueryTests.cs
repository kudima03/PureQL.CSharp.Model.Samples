using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record PartsLiftedOverEveryPartQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
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
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "datetimeFromParts",
                      "type": {
                        "name": "datetime",
                        "nullable": true
                      },
                      "date": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date",
                          "nullable": true
                        }
                      },
                      "time": {
                        "type": {
                          "name": "time"
                        },
                        "value": "09:00:00"
                      }
                    },
                    "right": {
                      "source": "schema_with_foreign_keys.users",
                      "field": "last_login",
                      "type": {
                        "name": "datetime"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "year_of_score",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "year": {
                          "operator": "add",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "values": [
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_score",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1990
                            }
                          ]
                        },
                        "month": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
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
                      "alias": "chosen_month",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "year": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "month": {
                          "operator": "integerDivide",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "10"
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
                      "alias": "birthday",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2025
                        },
                        "month": {
                          "operator": "month",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "day": {
                          "operator": "day",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "slot",
                      "expression": {
                        "operator": "timeFromParts",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "hour": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 9
                        },
                        "minute": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "second": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "call",
                      "expression": {
                        "operator": "timeFromParts",
                        "type": {
                          "name": "time"
                        },
                        "hour": {
                          "operator": "hour",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "minute": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
                        },
                        "second": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "45"
                        }
                      }
                    },
                    {
                      "alias": "trial_call",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "source": "schema_with_foreign_keys.orders",
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
                    },
                    {
                      "alias": "local_call",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
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
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
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
                      }
                    },
                    {
                      "alias": "chosen_zone_call",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "date": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
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
                        },
                        "zone": {
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "left": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean"
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
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Asia/Tokyo"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "year": {
                          "operator": "add",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "values": [
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_score",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1990
                            }
                          ]
                        },
                        "month": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 31
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PartsLiftedOverEveryPartQuery().Value).TextValue
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
                      "name": "year_of_score",
                      "type": "date"
                    },
                    {
                      "name": "chosen_month",
                      "type": "date"
                    },
                    {
                      "name": "birthday",
                      "type": "date"
                    },
                    {
                      "name": "slot",
                      "type": "time"
                    },
                    {
                      "name": "call",
                      "type": "time"
                    },
                    {
                      "name": "trial_call",
                      "type": "datetime"
                    },
                    {
                      "name": "local_call",
                      "type": "datetime"
                    },
                    {
                      "name": "chosen_zone_call",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2020-01-01",
                      "2020-03-01",
                      "2025-01-15",
                      "09:00:30",
                      "09:30:45",
                      "2024-06-01T09:00:00Z",
                      "2020-01-15T14:00:00Z",
                      "2020-01-15T14:00:00Z"
                    ],
                    [
                      "2020-01-01",
                      "2019-03-01",
                      "2025-07-10",
                      "09:00:30",
                      "09:30:45",
                      "2024-06-05T09:00:00Z",
                      "2019-07-10T13:00:00Z",
                      "2019-07-10T13:00:00Z"
                    ],
                    [
                      "",
                      "",
                      "2025-03-20",
                      "",
                      "10:30:45",
                      "2024-06-03T10:00:00Z",
                      "2021-03-20T01:00:00Z",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PartsLiftedOverEveryPartQuery().Result).TextValue
        );
    }
}
