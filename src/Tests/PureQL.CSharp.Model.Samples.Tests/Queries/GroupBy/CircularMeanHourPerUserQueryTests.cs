using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record CircularMeanHourPerUserQueryTests
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
                  "groupBy": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
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
                            "operator": "sum",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
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
                            "operator": "sum",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
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
                          "exponent": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      ]
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
                      "alias": "user_id",
                      "expression": {
                        "key": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "mean_hour_angle",
                      "expression": {
                        "operator": "atan2",
                        "type": {
                          "name": "decimal"
                        },
                        "y": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                        "x": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                        }
                      }
                    },
                    {
                      "alias": "mean_hour",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
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
                                  "operator": "atan2",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "y": {
                                    "operator": "sum",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "selector": {
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
                                  "x": {
                                    "operator": "sum",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "selector": {
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
                                      "value": 12
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
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 24
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 24
                        }
                      }
                    },
                    {
                      "alias": "concentration",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "sqrt",
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
                                  "operator": "power",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "operand": {
                                    "operator": "sum",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "selector": {
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
                                    "operator": "sum",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "selector": {
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
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "average_sine",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
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
                      }
                    },
                    {
                      "alias": "midday_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "lessThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "-0.9"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "atan2",
                        "type": {
                          "name": "decimal"
                        },
                        "y": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                        "x": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CircularMeanHourPerUserQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "mean_hour_angle",
                      "type": "double"
                    },
                    {
                      "name": "mean_hour",
                      "type": "double"
                    },
                    {
                      "name": "concentration",
                      "type": "double"
                    },
                    {
                      "name": "average_sine",
                      "type": "double"
                    },
                    {
                      "name": "midday_orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "-2.748893571891069",
                      "13.5",
                      "0.9914448613738104",
                      "-0.37940952255126037",
                      "1"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2.748893571891069",
                      "10.5",
                      "0.9914448613738104",
                      "0.37940952255126037",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CircularMeanHourPerUserQuery().Result).TextValue
        );
    }
}
