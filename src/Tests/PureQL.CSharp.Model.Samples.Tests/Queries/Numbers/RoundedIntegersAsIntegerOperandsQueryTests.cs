using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundedIntegersAsIntegerOperandsQueryTests
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
                              "value": "00000335-0000-0000-0000-000000000000"
                            }
                          },
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
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000336-0000-0000-0000-000000000000"
                            }
                          },
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
                              "value": 0
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
                              "value": "00000337-0000-0000-0000-000000000000"
                            }
                          },
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
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000338-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 18
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
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000339-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 42
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
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000033a-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 36
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
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
                              "value": "0000033b-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 49
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
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "lines"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "lines",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
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
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "lines",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "days",
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
                      }
                    },
                    {
                      "alias": "months",
                      "expression": {
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
                            "value": -1
                          }
                        }
                      }
                    },
                    {
                      "alias": "decade",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
                          "operator": "truncate",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
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
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
                          }
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
                      "alias": "stars",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "*"
                        },
                        "count": {
                          "operator": "integerDivide",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
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
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        }
                      }
                    },
                    {
                      "alias": "padded",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
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
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    },
                    {
                      "alias": "left_over",
                      "expression": {
                        "operator": "modulo",
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
                          "operator": "greatest",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
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
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "sum_of_integers",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
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
                          {
                            "source": "lines",
                            "field": "returned_quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "absolute",
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
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
                            {
                              "source": "lines",
                              "field": "quantity",
                              "type": {
                                "name": "integer"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "unless_zero",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
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
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "either",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "integer"
                        },
                        "condition": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
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
                            "value": 40
                          }
                        },
                        "then": {
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
                            "value": -2
                          }
                        },
                        "else": {
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
                      }
                    },
                    {
                      "alias": "bucket",
                      "expression": {
                        "operator": "ntile",
                        "type": {
                          "name": "integer"
                        },
                        "buckets": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 4
                        },
                        "over": {
                          "orderBy": [
                            {
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
                              "expression": {
                                "source": "lines",
                                "field": "id",
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
                      "alias": "previous_tens",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "integer",
                          "nullable": true
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
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "lines",
                                "field": "id",
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
                      "alias": "in_a_list",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
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
                        "list": {
                          "type": {
                            "name": "integerList"
                          },
                          "value": [
                            10,
                            20,
                            50
                          ]
                        }
                      }
                    },
                    {
                      "alias": "as_text",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
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
                        }
                      }
                    },
                    {
                      "alias": "widened_on_purpose",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
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
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
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
            new QueryJson(new RoundedIntegersAsIntegerOperandsQuery().Value).TextValue
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
                      "name": "days",
                      "type": "date"
                    },
                    {
                      "name": "months",
                      "type": "date"
                    },
                    {
                      "name": "decade",
                      "type": "date"
                    },
                    {
                      "name": "stars",
                      "type": "string"
                    },
                    {
                      "name": "padded",
                      "type": "string"
                    },
                    {
                      "name": "left_over",
                      "type": "long"
                    },
                    {
                      "name": "sum_of_integers",
                      "type": "long"
                    },
                    {
                      "name": "absolute",
                      "type": "long"
                    },
                    {
                      "name": "unless_zero",
                      "type": "long"
                    },
                    {
                      "name": "either",
                      "type": "long"
                    },
                    {
                      "name": "bucket",
                      "type": "long"
                    },
                    {
                      "name": "previous_tens",
                      "type": "long"
                    },
                    {
                      "name": "in_a_list",
                      "type": "bool"
                    },
                    {
                      "name": "as_text",
                      "type": "string"
                    },
                    {
                      "name": "widened_on_purpose",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000033b-0000-0000-0000-000000000000",
                      "2024-07-21",
                      "2027-10-01",
                      "2020-01-01",
                      "*****",
                      "                                           shipped",
                      "49",
                      "59",
                      "1",
                      "50",
                      "0",
                      "4",
                      "40",
                      "True",
                      "50",
                      "50"
                    ],
                    [
                      "00000339-0000-0000-0000-000000000000",
                      "2024-07-15",
                      "2027-10-05",
                      "2020-01-01",
                      "****",
                      "                                 shipped",
                      "2",
                      "45",
                      "2",
                      "40",
                      "0",
                      "2",
                      "20",
                      "False",
                      "40",
                      "40"
                    ],
                    [
                      "0000033a-0000-0000-0000-000000000000",
                      "2024-07-16",
                      "2026-12-06",
                      "2020-01-01",
                      "****",
                      "                                 pending",
                      "36",
                      "40",
                      "4",
                      "40",
                      "40",
                      "3",
                      "40",
                      "False",
                      "40",
                      "40"
                    ],
                    [
                      "00000336-0000-0000-0000-000000000000",
                      "2024-07-02",
                      "2026-02-02",
                      "2020-01-01",
                      "***",
                      "                       pending",
                      "25",
                      "30",
                      "5",
                      "30",
                      "30",
                      "2",
                      "",
                      "False",
                      "30",
                      "30"
                    ],
                    [
                      "00000337-0000-0000-0000-000000000000",
                      "2024-06-23",
                      "2025-04-03",
                      "2020-01-01",
                      "**",
                      "             shipped",
                      "15",
                      "23",
                      "5",
                      "20",
                      "20",
                      "1",
                      "30",
                      "True",
                      "20",
                      "20"
                    ],
                    [
                      "00000338-0000-0000-0000-000000000000",
                      "2024-06-24",
                      "2025-04-04",
                      "2020-01-01",
                      "**",
                      "           cancelled",
                      "18",
                      "22",
                      "2",
                      "20",
                      "20",
                      "1",
                      "20",
                      "True",
                      "20",
                      "20"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundedIntegersAsIntegerOperandsQuery().Result).TextValue
        );
    }
}
