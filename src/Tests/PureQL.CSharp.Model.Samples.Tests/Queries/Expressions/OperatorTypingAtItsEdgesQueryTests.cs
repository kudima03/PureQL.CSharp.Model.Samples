using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record OperatorTypingAtItsEdgesQueryTests
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
                      "name": "facts",
                      "rows": [
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000065-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "is_verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-04"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "09:00:00"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "SPRING"
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000066-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "is_verified",
                            "expression": {
                              "type": {
                                "name": "boolean",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "10:00:00"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000067-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "is_verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-05"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "11:30:00"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000068-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "is_verified",
                            "expression": {
                              "type": {
                                "name": "boolean",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "08:00:00"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "WINTER"
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000069-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "is_verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "23:59:30"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000006a-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "is_verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-09"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "18:45:00"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "SUMMER"
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
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
                      "subquery": "facts",
                      "on": {
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
                          "source": "facts",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
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
                        "source": "facts",
                        "field": "is_paid",
                        "type": {
                          "name": "boolean"
                        }
                      },
                      {
                        "operator": "coalesce",
                        "type": {
                          "name": "boolean"
                        },
                        "values": [
                          {
                            "source": "facts",
                            "field": "is_verified",
                            "type": {
                              "name": "boolean",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "boolean"
                            },
                            "value": false
                          }
                        ]
                      },
                      {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      },
                      {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      },
                      {
                        "operator": "not",
                        "type": {
                          "name": "boolean"
                        },
                        "condition": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "right": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
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
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "source": "facts",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        }
                      },
                      {
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "facts",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "floor_of_decimal",
                      "expression": {
                        "operator": "floor",
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
                      }
                    },
                    {
                      "alias": "hundreds",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -2
                        }
                      }
                    },
                    {
                      "alias": "computed_digits",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "operator": "add",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
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
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "source": "facts",
                          "field": "shipped_date",
                          "type": {
                            "name": "date",
                            "nullable": true
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
                    },
                    {
                      "alias": "one_minute_later",
                      "expression": {
                        "operator": "timeAddSeconds",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "facts",
                          "field": "window_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "seconds": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 60
                        }
                      }
                    },
                    {
                      "alias": "sum3",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          },
                          {
                            "source": "facts",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "same_status",
                      "expression": {
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
                    },
                    {
                      "alias": "either_id",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "uuid"
                        },
                        "condition": {
                          "source": "facts",
                          "field": "is_paid",
                          "type": {
                            "name": "boolean"
                          }
                        },
                        "then": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "else": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "code",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          },
                          {
                            "source": "facts",
                            "field": "coupon_code",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "none"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "points_or_total",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "facts",
                            "field": "bonus_points",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "bonus_points_value",
                      "expression": {
                        "source": "facts",
                        "field": "bonus_points",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "paid_and_verified",
                      "expression": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "source": "facts",
                            "field": "is_paid",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "facts",
                              "field": "is_verified",
                              "type": {
                                "name": "boolean",
                                "nullable": true
                              }
                            },
                            "right": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OperatorTypingAtItsEdgesQuery().Value).TextValue
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
                      "name": "floor_of_decimal",
                      "type": "long"
                    },
                    {
                      "name": "hundreds",
                      "type": "double"
                    },
                    {
                      "name": "computed_digits",
                      "type": "double"
                    },
                    {
                      "name": "days_to_ship",
                      "type": "long"
                    },
                    {
                      "name": "one_minute_later",
                      "type": "time"
                    },
                    {
                      "name": "sum3",
                      "type": "long"
                    },
                    {
                      "name": "same_status",
                      "type": "bool"
                    },
                    {
                      "name": "either_id",
                      "type": "uuid"
                    },
                    {
                      "name": "code",
                      "type": "string"
                    },
                    {
                      "name": "points_or_total",
                      "type": "double"
                    },
                    {
                      "name": "bonus_points_value",
                      "type": "long"
                    },
                    {
                      "name": "paid_and_verified",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "100",
                      "100",
                      "100.5",
                      "3",
                      "09:01:00",
                      "5",
                      "True",
                      "00000065-0000-0000-0000-000000000000",
                      "SPRING",
                      "10",
                      "10",
                      "True"
                    ],
                    [
                      "300",
                      "300",
                      "300",
                      "",
                      "00:00:30",
                      "6",
                      "True",
                      "00000069-0000-0000-0000-000000000000",
                      "none",
                      "300",
                      "",
                      "True"
                    ],
                    [
                      "100",
                      "100",
                      "100.5",
                      "3",
                      "18:46:00",
                      "4",
                      "False",
                      "0000006a-0000-0000-0000-000000000000",
                      "SUMMER",
                      "4",
                      "4",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OperatorTypingAtItsEdgesQuery().Result).TextValue
        );
    }
}
