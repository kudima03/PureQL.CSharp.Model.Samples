using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GrowthPerUserQueryTests
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
                            "operator": "sum",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
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
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "2.1"
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
                      "alias": "monthly_growth",
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
                                  "operator": "last",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
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
                                  ]
                                },
                                {
                                  "operator": "nullIfEqual",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "left": {
                                    "operator": "first",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "selector": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_total",
                                      "type": {
                                        "name": "decimal"
                                      }
                                    },
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
                                    ]
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
                                  "operator": "greatest",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "values": [
                                    {
                                      "operator": "dateDiffMonths",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "left": {
                                        "operator": "max",
                                        "type": {
                                          "name": "date"
                                        },
                                        "selector": {
                                          "source": "schema_with_foreign_keys.orders",
                                          "field": "placed_on",
                                          "type": {
                                            "name": "date"
                                          }
                                        }
                                      },
                                      "right": {
                                        "operator": "min",
                                        "type": {
                                          "name": "date"
                                        },
                                        "selector": {
                                          "source": "schema_with_foreign_keys.users",
                                          "field": "signup_date",
                                          "type": {
                                            "name": "date"
                                          }
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
                      "alias": "norm_of_totals",
                      "expression": {
                        "operator": "sqrt",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                        }
                      }
                    },
                    {
                      "alias": "log_revenue_share",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "ln",
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
                                  "operator": "sum",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2
                                }
                              ]
                            }
                          },
                          {
                            "operator": "ln",
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
                                  "operator": "sum",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "over": "all"
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "sum_of_logarithms",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "ln",
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
                      }
                    },
                    {
                      "alias": "large_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "sqrt",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                        }
                      },
                      "direction": "desc"
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
            new QueryJson(new GrowthPerUserQuery().Value).TextValue
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
                      "name": "monthly_growth",
                      "type": "double"
                    },
                    {
                      "name": "norm_of_totals",
                      "type": "double"
                    },
                    {
                      "name": "log_revenue_share",
                      "type": "double"
                    },
                    {
                      "name": "sum_of_logarithms",
                      "type": "double"
                    },
                    {
                      "name": "large_orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "0.024130798350772295",
                      "309.2936509209331",
                      "0.8824896563567544",
                      "10.024598378285186",
                      "1"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "0",
                      "200",
                      "0.7888042734606033",
                      "5.298317366548036",
                      "1"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "-0.013335945431202744",
                      "112.25083518620251",
                      "0.7464701025183629",
                      "8.522180732927277",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GrowthPerUserQuery().Result).TextValue
        );
    }
}
