using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundAndTruncateKeepTheOperandTypeQueryTests
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
                      "name": "lines",
                      "rows": [
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000033f-0000-0000-0000-000000000000"
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
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "unit_price",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "9.99"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000340-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 25
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "unit_price",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "19.99"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000341-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 15
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "unit_price",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "4.5"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000342-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 149
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 9
                            }
                          },
                          {
                            "alias": "unit_price",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "250"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000343-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 260
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          {
                            "alias": "unit_price",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "1234.5678"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "lines"
                  },
                  "select": [
                    {
                      "alias": "integer_to_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "decimal_to_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "unit_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "integer_to_two_places",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "decimal_to_two_places",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "unit_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "integer_truncated",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
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
                      "alias": "decimal_truncated",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "unit_price",
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
                      "alias": "widened_first",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "of_an_integer_sum",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "add",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "source": "lines",
                              "field": "quantity",
                              "type": {
                                "name": "integer"
                              }
                            },
                            {
                              "source": "lines",
                              "field": "returned_quantity",
                              "type": {
                                "name": "integer"
                              }
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "of_a_mixed_sum",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "add",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "lines",
                              "field": "quantity",
                              "type": {
                                "name": "integer"
                              }
                            },
                            {
                              "source": "lines",
                              "field": "unit_price",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "of_a_quotient",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "lines",
                              "field": "quantity",
                              "type": {
                                "name": "integer"
                              }
                            },
                            {
                              "source": "lines",
                              "field": "returned_quantity",
                              "type": {
                                "name": "integer"
                              }
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "of_an_integer_quotient",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "integerDivide",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 3
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "of_a_literal_integer",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2750
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
                      "alias": "of_a_literal_decimal",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.15"
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "of_a_literal_truncated",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1299
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
                      "alias": "half_even",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "nullable_operand",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "nullable_digits",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "operator": "asNullable",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
                          }
                        }
                      }
                    },
                    {
                      "alias": "nullable_decimal",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "rounded_twice",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
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
                      "alias": "without_digits_of_a_decimal",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "unit_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundAndTruncateKeepTheOperandTypeQuery().Value).TextValue
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
                      "name": "integer_to_tens",
                      "type": "long"
                    },
                    {
                      "name": "decimal_to_tens",
                      "type": "double"
                    },
                    {
                      "name": "integer_to_two_places",
                      "type": "long"
                    },
                    {
                      "name": "decimal_to_two_places",
                      "type": "double"
                    },
                    {
                      "name": "integer_truncated",
                      "type": "long"
                    },
                    {
                      "name": "decimal_truncated",
                      "type": "double"
                    },
                    {
                      "name": "widened_first",
                      "type": "double"
                    },
                    {
                      "name": "of_an_integer_sum",
                      "type": "long"
                    },
                    {
                      "name": "of_a_mixed_sum",
                      "type": "double"
                    },
                    {
                      "name": "of_a_quotient",
                      "type": "double"
                    },
                    {
                      "name": "of_an_integer_quotient",
                      "type": "long"
                    },
                    {
                      "name": "of_a_literal_integer",
                      "type": "long"
                    },
                    {
                      "name": "of_a_literal_decimal",
                      "type": "double"
                    },
                    {
                      "name": "of_a_literal_truncated",
                      "type": "long"
                    },
                    {
                      "name": "half_even",
                      "type": "long"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "long"
                    },
                    {
                      "name": "nullable_digits",
                      "type": "long"
                    },
                    {
                      "name": "nullable_decimal",
                      "type": "double"
                    },
                    {
                      "name": "rounded_twice",
                      "type": "long"
                    },
                    {
                      "name": "without_digits_of_a_decimal",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0",
                      "10",
                      "4",
                      "9.99",
                      "0",
                      "0",
                      "0",
                      "10",
                      "10",
                      "4",
                      "0",
                      "2800",
                      "0.2",
                      "1200",
                      "0",
                      "",
                      "0",
                      "",
                      "0",
                      "10"
                    ],
                    [
                      "30",
                      "20",
                      "25",
                      "19.99",
                      "0",
                      "0",
                      "30",
                      "30",
                      "40",
                      "13",
                      "10",
                      "2800",
                      "0.2",
                      "1200",
                      "20",
                      "",
                      "20",
                      "",
                      "0",
                      "20"
                    ],
                    [
                      "20",
                      "0",
                      "15",
                      "4.5",
                      "0",
                      "0",
                      "20",
                      "20",
                      "20",
                      "5",
                      "10",
                      "2800",
                      "0.2",
                      "1200",
                      "20",
                      "",
                      "10",
                      "",
                      "0",
                      "5"
                    ],
                    [
                      "150",
                      "250",
                      "149",
                      "250",
                      "100",
                      "200",
                      "150",
                      "160",
                      "400",
                      "17",
                      "50",
                      "2800",
                      "0.2",
                      "1200",
                      "150",
                      "",
                      "140",
                      "",
                      "200",
                      "250"
                    ],
                    [
                      "260",
                      "1230",
                      "260",
                      "1234.57",
                      "200",
                      "1200",
                      "260",
                      "270",
                      "1490",
                      "52",
                      "90",
                      "2800",
                      "0.2",
                      "1200",
                      "260",
                      "",
                      "260",
                      "",
                      "300",
                      "1235"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new RoundAndTruncateKeepTheOperandTypeQuery().Result
            ).TextValue
        );
    }
}
