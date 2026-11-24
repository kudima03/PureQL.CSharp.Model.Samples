using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record HourAsAnAngleQueryTests
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
                      "alias": "hour",
                      "expression": {
                        "operator": "hour",
                        "type": {
                          "name": "integer"
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
                      "alias": "hour_angle",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "hour",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
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
                                "operator": "pi",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 12
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "alias": "hour_x",
                      "expression": {
                        "operator": "cos",
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
                              "operator": "hour",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
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
                                  "operator": "pi",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 12
                                }
                              ]
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "hour_y",
                      "expression": {
                        "operator": "sin",
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
                              "operator": "hour",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
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
                                  "operator": "pi",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 12
                                }
                              ]
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "angle_back_in_degrees",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "atan2",
                            "type": {
                              "name": "decimal"
                            },
                            "y": {
                              "operator": "sin",
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
                                    "operator": "hour",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "operand": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_at",
                                      "type": {
                                        "name": "datetime"
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
                                        "operator": "pi",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 12
                                      }
                                    ]
                                  }
                                ]
                              }
                            },
                            "x": {
                              "operator": "cos",
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
                                    "operator": "hour",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "operand": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_at",
                                      "type": {
                                        "name": "datetime"
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
                                        "operator": "pi",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 12
                                      }
                                    ]
                                  }
                                ]
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
                                "type": {
                                  "name": "integer"
                                },
                                "value": 180
                              },
                              {
                                "operator": "pi",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "alias": "pi",
                      "expression": {
                        "operator": "pi",
                        "type": {
                          "name": "decimal"
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
            new QueryJson(new HourAsAnAngleQuery().Value).TextValue
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
                      "name": "hour",
                      "type": "long"
                    },
                    {
                      "name": "hour_angle",
                      "type": "double"
                    },
                    {
                      "name": "hour_x",
                      "type": "double"
                    },
                    {
                      "name": "hour_y",
                      "type": "double"
                    },
                    {
                      "name": "angle_back_in_degrees",
                      "type": "double"
                    },
                    {
                      "name": "pi",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "10",
                      "2.6179938779914944",
                      "-0.8660254037844386",
                      "0.5",
                      "150",
                      "3.141592653589793"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "11",
                      "2.879793265790644",
                      "-0.9659258262890683",
                      "0.25881904510252074",
                      "165",
                      "3.141592653589793"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "12",
                      "3.141592653589793",
                      "-1",
                      "-7E-28",
                      "-180",
                      "3.141592653589793"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "13",
                      "3.4033920413889427",
                      "-0.9659258262890683",
                      "-0.25881904510252074",
                      "-165",
                      "3.141592653589793"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "14",
                      "3.6651914291880923",
                      "-0.8660254037844386",
                      "-0.5",
                      "-150",
                      "3.141592653589793"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "15",
                      "3.9269908169872414",
                      "-0.7071067811865476",
                      "-0.7071067811865476",
                      "-135",
                      "3.141592653589793"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new HourAsAnAngleQuery().Result).TextValue
        );
    }
}
