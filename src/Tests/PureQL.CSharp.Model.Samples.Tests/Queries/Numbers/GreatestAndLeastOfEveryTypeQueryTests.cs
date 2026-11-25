using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record GreatestAndLeastOfEveryTypeQueryTests
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
                      "entity": "schema_with_foreign_keys.employees",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_user_id",
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
                    },
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
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "shipped",
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
                      },
                      "alias": "shipped"
                    }
                  ],
                  "select": [
                    {
                      "alias": "decimal",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "60"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "integer",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 25
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 100
                          }
                        ]
                      }
                    },
                    {
                      "alias": "integer_nullable",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "dateDiffDays",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "source": "shipped",
                              "field": "placed_on",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1500
                          }
                        ]
                      }
                    },
                    {
                      "alias": "decimal_of_integer_and_decimal",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 150
                          }
                        ]
                      }
                    },
                    {
                      "alias": "string",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "pending"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "either_flag",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "boolean"
                        },
                        "values": [
                          {
                            "operator": "greaterThan",
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
                                "name": "integer"
                              },
                              "value": 100
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "both_flags",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "boolean"
                        },
                        "values": [
                          {
                            "operator": "greaterThan",
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
                                "name": "integer"
                              },
                              "value": 100
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "date",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-03"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "time",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "time"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          {
                            "type": {
                              "name": "time"
                            },
                            "value": "09:30:00"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "datetime",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "datetime"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "uuid",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "uuid"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "uuid_nullable",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.employees",
                            "field": "employee_manager_id",
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "middle_of_three",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "least",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            ]
                          },
                          {
                            "operator": "least",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "greatest",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_age",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                ]
                              },
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "60"
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GreatestAndLeastOfEveryTypeQuery().Value).TextValue
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
                      "name": "decimal",
                      "type": "double"
                    },
                    {
                      "name": "integer",
                      "type": "long"
                    },
                    {
                      "name": "integer_nullable",
                      "type": "long"
                    },
                    {
                      "name": "decimal_of_integer_and_decimal",
                      "type": "double"
                    },
                    {
                      "name": "string",
                      "type": "string"
                    },
                    {
                      "name": "either_flag",
                      "type": "bool"
                    },
                    {
                      "name": "both_flags",
                      "type": "bool"
                    },
                    {
                      "name": "date",
                      "type": "date"
                    },
                    {
                      "name": "time",
                      "type": "time"
                    },
                    {
                      "name": "datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "uuid",
                      "type": "uuid"
                    },
                    {
                      "name": "uuid_nullable",
                      "type": "uuid"
                    },
                    {
                      "name": "middle_of_three",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "100.5",
                      "25",
                      "1599",
                      "100.5",
                      "pending",
                      "True",
                      "True",
                      "2024-06-03",
                      "09:30:00",
                      "2024-06-01T08:30:00Z",
                      "00000001-0000-0000-0000-000000000000",
                      "",
                      "60"
                    ],
                    [
                      "100.5",
                      "25",
                      "",
                      "100.5",
                      "pending",
                      "True",
                      "True",
                      "2024-06-06",
                      "11:30:00",
                      "2024-06-03T18:45:00Z",
                      "00000004-0000-0000-0000-000000000000",
                      "000002be-0000-0000-0000-000000000000",
                      "60"
                    ],
                    [
                      "200",
                      "25",
                      "1500",
                      "150",
                      "pending",
                      "True",
                      "False",
                      "2024-06-03",
                      "10:00:00",
                      "2024-06-02T09:15:00Z",
                      "00000002-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "60"
                    ],
                    [
                      "300",
                      "25",
                      "1792",
                      "150",
                      "pending",
                      "True",
                      "True",
                      "2024-06-05",
                      "09:30:00",
                      "2024-05-30T14:00:00Z",
                      "00000003-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "60"
                    ],
                    [
                      "60",
                      "25",
                      "",
                      "50",
                      "pending",
                      "True",
                      "False",
                      "2024-06-03",
                      "09:30:00",
                      "2024-06-01T08:30:00Z",
                      "00000001-0000-0000-0000-000000000000",
                      "",
                      "50"
                    ],
                    [
                      "75.25",
                      "25",
                      "",
                      "75.25",
                      "cancelled",
                      "True",
                      "False",
                      "2024-06-04",
                      "09:30:00",
                      "2024-05-30T14:00:00Z",
                      "00000003-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "60"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GreatestAndLeastOfEveryTypeQuery().Result).TextValue
        );
    }
}
