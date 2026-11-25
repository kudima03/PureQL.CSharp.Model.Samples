using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record TrigonometricIdentitiesQueryTests
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
                      "kind": "left",
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
                      "alias": "sine_squared_plus_cosine_squared",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "power",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "sin",
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
                                    "value": 100
                                  }
                                ]
                              }
                            },
                            "exponent": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "operator": "power",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "cos",
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
                                    "value": 100
                                  }
                                ]
                              }
                            },
                            "exponent": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "tangent",
                      "expression": {
                        "operator": "tan",
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
                              "value": 100
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "sine_over_cosine",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "sin",
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
                                  "value": 100
                                }
                              ]
                            }
                          },
                          {
                            "operator": "cos",
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
                                  "value": 100
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "angle_of_its_sine",
                      "expression": {
                        "operator": "asin",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sin",
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
                                "value": 100
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "angle_of_its_tangent",
                      "expression": {
                        "operator": "atan",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "tan",
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
                                "value": 100
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "half_pi",
                      "expression": {
                        "operator": "acos",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "quarter_pi",
                      "expression": {
                        "operator": "atan",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "pi_from_atan2",
                      "expression": {
                        "operator": "atan2",
                        "type": {
                          "name": "decimal"
                        },
                        "y": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "x": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "sine_of_pi_is_not_zero",
                      "expression": {
                        "operator": "sin",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "pi",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "cosine_of_pi",
                      "expression": {
                        "operator": "cos",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "pi",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "in_degrees",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "asin",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.5"
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
                      "alias": "of_degrees",
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
                              "type": {
                                "name": "integer"
                              },
                              "value": 30
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
                                  "value": 180
                                }
                              ]
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "nullable",
                      "expression": {
                        "operator": "cos",
                        "type": {
                          "name": "decimal",
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
                      }
                    },
                    {
                      "alias": "nullable_point",
                      "expression": {
                        "operator": "atan2",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "x": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "two_pi",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          },
                          {
                            "operator": "pi",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "area",
                      "expression": {
                        "operator": "multiply",
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
                            "operator": "power",
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
                            "exponent": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ]
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
            new QueryJson(new TrigonometricIdentitiesQuery().Value).TextValue
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
                      "name": "sine_squared_plus_cosine_squared",
                      "type": "double"
                    },
                    {
                      "name": "tangent",
                      "type": "double"
                    },
                    {
                      "name": "sine_over_cosine",
                      "type": "double"
                    },
                    {
                      "name": "angle_of_its_sine",
                      "type": "double"
                    },
                    {
                      "name": "angle_of_its_tangent",
                      "type": "double"
                    },
                    {
                      "name": "half_pi",
                      "type": "double"
                    },
                    {
                      "name": "quarter_pi",
                      "type": "double"
                    },
                    {
                      "name": "pi_from_atan2",
                      "type": "double"
                    },
                    {
                      "name": "sine_of_pi_is_not_zero",
                      "type": "double"
                    },
                    {
                      "name": "cosine_of_pi",
                      "type": "double"
                    },
                    {
                      "name": "in_degrees",
                      "type": "double"
                    },
                    {
                      "name": "of_degrees",
                      "type": "double"
                    },
                    {
                      "name": "nullable",
                      "type": "double"
                    },
                    {
                      "name": "nullable_point",
                      "type": "double"
                    },
                    {
                      "name": "two_pi",
                      "type": "double"
                    },
                    {
                      "name": "area",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "1",
                      "1.574669883709823",
                      "1.574669883709823",
                      "1.005",
                      "1.005",
                      "1.5707963267948966",
                      "0.7853981633974483",
                      "3.141592653589793",
                      "3E-28",
                      "-1",
                      "30",
                      "0.5",
                      "0.15425144988758405",
                      "1.5374753309166493",
                      "6.283185307179586",
                      "31730.871199420308"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "1",
                      "0.5463024898437905",
                      "0.5463024898437905",
                      "0.5",
                      "0.5",
                      "1.5707963267948966",
                      "0.7853981633974483",
                      "3.141592653589793",
                      "3E-28",
                      "-1",
                      "30",
                      "0.5",
                      "0.15425144988758405",
                      "1.5374753309166493",
                      "6.283185307179586",
                      "7853.981633974483"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "1",
                      "-2.185039863261519",
                      "-2.185039863261519",
                      "1.1415926535897933",
                      "-1.1415926535897933",
                      "1.5707963267948966",
                      "0.7853981633974483",
                      "3.141592653589793",
                      "3E-28",
                      "-1",
                      "30",
                      "0.5",
                      "",
                      "",
                      "6.283185307179586",
                      "125663.70614359173"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "1",
                      "0.9362770506601118",
                      "0.9362770506601118",
                      "0.7525",
                      "0.7525",
                      "1.5707963267948966",
                      "0.7853981633974483",
                      "3.141592653589793",
                      "3E-28",
                      "-1",
                      "30",
                      "0.5",
                      "0.15425144988758405",
                      "1.5374753309166493",
                      "6.283185307179586",
                      "17789.464750493054"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "1",
                      "-0.1425465430742778",
                      "-0.1425465430742778",
                      "0.14159265358979323",
                      "-0.14159265358979323",
                      "1.5707963267948966",
                      "0.7853981633974483",
                      "3.141592653589793",
                      "3E-28",
                      "-1",
                      "30",
                      "0.5",
                      "0.15425144988758405",
                      "1.5374753309166493",
                      "6.283185307179586",
                      "282743.3388230814"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "1",
                      "1.574669883709823",
                      "1.574669883709823",
                      "1.005",
                      "1.005",
                      "1.5707963267948966",
                      "0.7853981633974483",
                      "3.141592653589793",
                      "3E-28",
                      "-1",
                      "30",
                      "0.5",
                      "",
                      "",
                      "6.283185307179586",
                      "31730.871199420308"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TrigonometricIdentitiesQuery().Result).TextValue
        );
    }
}
