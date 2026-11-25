using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record AddingAndTakingTheDifferenceQueryTests
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
                    }
                  ],
                  "select": [
                    {
                      "alias": "days_come_back",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "operator": "dateAddDays",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "days": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 30
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
                        }
                      }
                    },
                    {
                      "alias": "months_come_back",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "dateDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "operator": "dateAddMonths",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "months": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 5
                        }
                      }
                    },
                    {
                      "alias": "seconds_come_back",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "datetimeDiffSeconds",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
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
                              "value": "1.5"
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1.5"
                        }
                      }
                    },
                    {
                      "alias": "time_wraps",
                      "expression": {
                        "operator": "timeAddSeconds",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "seconds": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 86400
                        }
                      }
                    },
                    {
                      "alias": "shifted_twice",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "operator": "dateAddMonths",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "months": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        },
                        "days": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "previous_due",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "dateAddDays",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "days": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 30
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
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
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
                      "alias": "days_until_shipped_plus_grace",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "dateAddDays",
                          "type": {
                            "name": "date",
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
                              "operator": "dateAddDays",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "days": {
                                "operator": "integerDivide",
                                "type": {
                                  "name": "integer"
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
                                  "value": 50
                                }
                              }
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          "days": {
                            "operator": "integerDivide",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
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
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
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
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "days": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 30
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
            new QueryJson(new AddingAndTakingTheDifferenceQuery().Value).TextValue
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
                      "name": "days_come_back",
                      "type": "bool"
                    },
                    {
                      "name": "months_come_back",
                      "type": "bool"
                    },
                    {
                      "name": "seconds_come_back",
                      "type": "bool"
                    },
                    {
                      "name": "time_wraps",
                      "type": "time"
                    },
                    {
                      "name": "shifted_twice",
                      "type": "date"
                    },
                    {
                      "name": "previous_due",
                      "type": "date"
                    },
                    {
                      "name": "days_until_shipped_plus_grace",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "True",
                      "True",
                      "09:00:00",
                      "2024-06-30",
                      "",
                      "5"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "09:00:00",
                      "2024-07-01",
                      "2024-07-01",
                      ""
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "10:00:00",
                      "2024-07-02",
                      "",
                      ""
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "09:00:00",
                      "2024-07-03",
                      "",
                      ""
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "09:00:00",
                      "2024-07-04",
                      "2024-07-04",
                      "9"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "11:30:00",
                      "2024-07-05",
                      "",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AddingAndTakingTheDifferenceQuery().Result).TextValue
        );
    }
}
