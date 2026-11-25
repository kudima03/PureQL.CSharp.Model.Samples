using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record PartsAndBackQueryTests
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
                      "alias": "date_from_its_parts",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "left": {
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
                            "operator": "day",
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
                        }
                      }
                    },
                    {
                      "alias": "time_from_its_parts",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "left": {
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
                            "operator": "minute",
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
                          "second": {
                            "operator": "add",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "second",
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
                              {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "microsecond",
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
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 1000000
                                  }
                                ]
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "instant_from_its_parts",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "left": {
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
                            }
                          },
                          "time": {
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
                        }
                      }
                    },
                    {
                      "alias": "midnight",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
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
                            "operator": "timeFromParts",
                            "type": {
                              "name": "time"
                            },
                            "hour": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            },
                            "minute": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            },
                            "second": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        },
                        "right": {
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
                        }
                      }
                    },
                    {
                      "alias": "first_of_month",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
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
                        },
                        "right": {
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
                      }
                    },
                    {
                      "alias": "first_of_march",
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
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
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
                      "alias": "nullable_date",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "year": {
                          "operator": "year",
                          "type": {
                            "name": "integer",
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
                          }
                        },
                        "month": {
                          "operator": "month",
                          "type": {
                            "name": "integer",
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
                      "alias": "fraction_of_a_second",
                      "expression": {
                        "operator": "timeFromParts",
                        "type": {
                          "name": "time"
                        },
                        "hour": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 23
                        },
                        "minute": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 59
                        },
                        "second": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "59.999999"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PartsAndBackQuery().Value).TextValue
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
                      "name": "date_from_its_parts",
                      "type": "bool"
                    },
                    {
                      "name": "time_from_its_parts",
                      "type": "bool"
                    },
                    {
                      "name": "instant_from_its_parts",
                      "type": "bool"
                    },
                    {
                      "name": "midnight",
                      "type": "bool"
                    },
                    {
                      "name": "first_of_month",
                      "type": "bool"
                    },
                    {
                      "name": "first_of_march",
                      "type": "date"
                    },
                    {
                      "name": "nullable_date",
                      "type": "date"
                    },
                    {
                      "name": "fraction_of_a_second",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "2024-03-01",
                      "2024-06-01",
                      "23:59:59.999999"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "2024-03-01",
                      "",
                      "23:59:59.999999"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "2024-03-01",
                      "2024-06-01",
                      "23:59:59.999999"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "2024-03-01",
                      "",
                      "23:59:59.999999"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "2024-03-01",
                      "2024-06-01",
                      "23:59:59.999999"
                    ],
                    [
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "2024-03-01",
                      "",
                      "23:59:59.999999"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PartsAndBackQuery().Result).TextValue
        );
    }
}
