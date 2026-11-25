using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundingIntegersToTensAndHundredsQueryTests
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
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "select": [
                    {
                      "alias": "to_tens",
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
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 37
                              }
                            ]
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
                      "alias": "to_hundreds_toward_zero",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 37
                              }
                            ]
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
                      "alias": "to_hundreds",
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
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 37
                              }
                            ]
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
                      "alias": "by_computed_digits",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 37
                              }
                            ]
                          }
                        },
                        "digits": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
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
                      "alias": "nullable_to_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
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
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_qty",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 37
                                }
                              ]
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
                      "alias": "of_a_sum_of_integers",
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
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 37
                                  }
                                ]
                              }
                            },
                            {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "operator": "add",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "multiply",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "source": "schema_with_foreign_keys.order_items",
                                        "field": "item_qty",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 20
                                      }
                                    ]
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 55
                                  }
                                ]
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
                      "alias": "with_no_digits_to_drop",
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
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 37
                              }
                            ]
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
                      "alias": "still_an_integer",
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
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 37
                                  }
                                ]
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
                            "value": 1
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundingIntegersToTensAndHundredsQuery().Value).TextValue
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
                      "name": "to_tens",
                      "type": "long"
                    },
                    {
                      "name": "to_hundreds_toward_zero",
                      "type": "long"
                    },
                    {
                      "name": "to_hundreds",
                      "type": "long"
                    },
                    {
                      "name": "by_computed_digits",
                      "type": "long"
                    },
                    {
                      "name": "nullable_to_tens",
                      "type": "long"
                    },
                    {
                      "name": "of_a_sum_of_integers",
                      "type": "long"
                    },
                    {
                      "name": "with_no_digits_to_drop",
                      "type": "long"
                    },
                    {
                      "name": "still_an_integer",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "70",
                      "0",
                      "100",
                      "70",
                      "70",
                      "170",
                      "74",
                      "71"
                    ],
                    [
                      "40",
                      "0",
                      "0",
                      "30",
                      "40",
                      "110",
                      "37",
                      "41"
                    ],
                    [
                      "190",
                      "100",
                      "200",
                      "180",
                      "190",
                      "340",
                      "185",
                      "191"
                    ],
                    [
                      "110",
                      "100",
                      "100",
                      "110",
                      "110",
                      "230",
                      "111",
                      "111"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundingIntegersToTensAndHundredsQuery().Result).TextValue
        );
    }
}
