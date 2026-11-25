using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record PowersAndLogarithmsQueryTests
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
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "log10",
                      "type": {
                        "name": "decimal"
                      },
                      "operand": {
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
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 2
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
                      "alias": "compounded_total",
                      "expression": {
                        "operator": "multiply",
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
                            "operator": "power",
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
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 1
                                },
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.05"
                                }
                              ]
                            },
                            "exponent": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "continuously_compounded_total",
                      "expression": {
                        "operator": "multiply",
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
                            "operator": "exp",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0.05"
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 3
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "yearly_growth_from_score",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "power",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "operand": {
                              "operator": "divide",
                              "type": {
                                "name": "decimal",
                                "nullable": true
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
                                  "operator": "nullIfEqual",
                                  "type": {
                                    "name": "decimal",
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
                                      "name": "integer"
                                    },
                                    "value": 0
                                  }
                                }
                              ]
                            },
                            "exponent": {
                              "operator": "divide",
                              "type": {
                                "name": "decimal"
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
                                  "value": 3
                                }
                              ]
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "continuous_rate",
                      "expression": {
                        "operator": "ln",
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
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.05"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "digits_before_the_point",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "floor",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "operator": "log10",
                              "type": {
                                "name": "decimal"
                              },
                              "operand": {
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
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "doublings_from_one",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
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
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          ]
                        },
                        "base": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "square_root_of_total",
                      "expression": {
                        "operator": "sqrt",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
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
            new QueryJson(new PowersAndLogarithmsQuery().Value).TextValue
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
                      "name": "compounded_total",
                      "type": "double"
                    },
                    {
                      "name": "continuously_compounded_total",
                      "type": "double"
                    },
                    {
                      "name": "yearly_growth_from_score",
                      "type": "double"
                    },
                    {
                      "name": "continuous_rate",
                      "type": "double"
                    },
                    {
                      "name": "digits_before_the_point",
                      "type": "long"
                    },
                    {
                      "name": "doublings_from_one",
                      "type": "double"
                    },
                    {
                      "name": "square_root_of_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "116.3413125",
                      "116.76434139419246",
                      "0.4962871135284913",
                      "0.048790164169432",
                      "3",
                      "6.651051691178929",
                      "10.024968827881711"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "231.525",
                      "232.36684854565664",
                      "",
                      "0.048790164169432",
                      "3",
                      "7.643856189774724",
                      "14.142135623730951"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "347.2875",
                      "348.5502728184849",
                      "1.1544346900318838",
                      "0.048790164169432",
                      "3",
                      "8.228818690495881",
                      "17.320508075688775"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "116.3413125",
                      "116.76434139419246",
                      "",
                      "0.048790164169432",
                      "3",
                      "6.651051691178929",
                      "10.024968827881711"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PowersAndLogarithmsQuery().Result).TextValue
        );
    }
}
