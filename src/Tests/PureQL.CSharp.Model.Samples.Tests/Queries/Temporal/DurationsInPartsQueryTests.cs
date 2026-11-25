using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record DurationsInPartsQueryTests
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
                      "alias": "days",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
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
                              "value": "37.5"
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 86400
                        }
                      }
                    },
                    {
                      "alias": "hours",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "modulo",
                          "type": {
                            "name": "decimal"
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
                                "value": "37.5"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 86400
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3600
                        }
                      }
                    },
                    {
                      "alias": "minutes",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "modulo",
                          "type": {
                            "name": "decimal"
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
                                "value": "37.5"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 3600
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 60
                        }
                      }
                    },
                    {
                      "alias": "seconds",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
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
                              "value": "37.5"
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 60
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "integerDivide",
                              "type": {
                                "name": "integer"
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
                                    "value": "37.5"
                                  }
                                },
                                "right": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "last_login",
                                  "type": {
                                    "name": "datetime"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3600
                              }
                            }
                          },
                          {
                            "operator": "padStart",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "integerDivide",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "operator": "modulo",
                                  "type": {
                                    "name": "decimal"
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
                                        "value": "37.5"
                                      }
                                    },
                                    "right": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "last_login",
                                      "type": {
                                        "name": "datetime"
                                      }
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 3600
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 60
                                }
                              }
                            },
                            "length": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            "fill": {
                              "type": {
                                "name": "string"
                              },
                              "value": "0"
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
                        }
                      }
                    },
                    {
                      "alias": "quarter_hours_running",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "integerDivide",
                          "type": {
                            "name": "integer"
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
                                "value": "37.5"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 900
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
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
                              "value": "37.5"
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 86400
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
            new QueryJson(new DurationsInPartsQuery().Value).TextValue
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
                      "name": "days",
                      "type": "long"
                    },
                    {
                      "name": "hours",
                      "type": "long"
                    },
                    {
                      "name": "minutes",
                      "type": "long"
                    },
                    {
                      "name": "seconds",
                      "type": "double"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "quarter_hours_running",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "6",
                      "0",
                      "0",
                      "37.5",
                      "144:00",
                      "1052"
                    ],
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "0",
                      "1",
                      "30",
                      "37.5",
                      "1:30",
                      "6"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "1",
                      "2",
                      "30",
                      "37.5",
                      "26:30",
                      "112"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "1",
                      "2",
                      "45",
                      "37.5",
                      "26:45",
                      "107"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2",
                      "20",
                      "15",
                      "37.5",
                      "68:15",
                      "273"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "4",
                      "23",
                      "0",
                      "37.5",
                      "119:00",
                      "476"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DurationsInPartsQuery().Result).TextValue
        );
    }
}
