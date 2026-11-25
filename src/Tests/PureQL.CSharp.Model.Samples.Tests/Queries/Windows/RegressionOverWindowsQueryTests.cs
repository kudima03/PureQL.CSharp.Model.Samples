using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RegressionOverWindowsQueryTests
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
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "trend_of_all_orders",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        }
                      }
                    },
                    {
                      "alias": "trend_of_the_customer",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rolling_trend",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                            "preceding": 11
                          }
                        }
                      }
                    },
                    {
                      "alias": "rolling_intercept",
                      "expression": {
                        "operator": "regressionIntercept",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                            "preceding": 11
                          }
                        }
                      }
                    },
                    {
                      "alias": "rolling_fit",
                      "expression": {
                        "operator": "regressionRSquared",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                            "preceding": 11
                          }
                        }
                      }
                    },
                    {
                      "alias": "trend_of_ninety_days",
                      "expression": {
                        "operator": "regressionSlope",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "y": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "x": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "days": 1
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "above_its_trend",
                      "expression": {
                        "operator": "greaterThan",
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
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "add",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "values": [
                                {
                                  "operator": "regressionIntercept",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "y": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "x": {
                                    "operator": "dateDiffDays",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_on",
                                      "type": {
                                        "name": "date"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "date"
                                      },
                                      "value": "2024-06-01"
                                    }
                                  },
                                  "over": {
                                    "partitionBy": [
                                      {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_tenant_id",
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
                                      "preceding": 11
                                    }
                                  }
                                },
                                {
                                  "operator": "multiply",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "values": [
                                    {
                                      "operator": "regressionSlope",
                                      "type": {
                                        "name": "decimal",
                                        "nullable": true
                                      },
                                      "y": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_total",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      "x": {
                                        "operator": "dateDiffDays",
                                        "type": {
                                          "name": "integer"
                                        },
                                        "left": {
                                          "source": "schema_with_foreign_keys.orders",
                                          "field": "placed_on",
                                          "type": {
                                            "name": "date"
                                          }
                                        },
                                        "right": {
                                          "type": {
                                            "name": "date"
                                          },
                                          "value": "2024-06-01"
                                        }
                                      },
                                      "over": {
                                        "partitionBy": [
                                          {
                                            "source": "schema_with_foreign_keys.orders",
                                            "field": "order_tenant_id",
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
                                          "preceding": 11
                                        }
                                      }
                                    },
                                    {
                                      "operator": "dateDiffDays",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "left": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "placed_on",
                                        "type": {
                                          "name": "date"
                                        }
                                      },
                                      "right": {
                                        "type": {
                                          "name": "date"
                                        },
                                        "value": "2024-06-01"
                                      }
                                    }
                                  ]
                                }
                              ]
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
            new QueryJson(new RegressionOverWindowsQuery().Value).TextValue
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
                      "name": "trend_of_all_orders",
                      "type": "double"
                    },
                    {
                      "name": "trend_of_the_customer",
                      "type": "double"
                    },
                    {
                      "name": "rolling_trend",
                      "type": "double"
                    },
                    {
                      "name": "rolling_intercept",
                      "type": "double"
                    },
                    {
                      "name": "rolling_fit",
                      "type": "double"
                    },
                    {
                      "name": "trend_of_ninety_days",
                      "type": "double"
                    },
                    {
                      "name": "above_its_trend",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "17.864285714285714",
                      "49.75",
                      "",
                      "",
                      "",
                      "",
                      "False"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "17.864285714285714",
                      "49.75",
                      "-50.5",
                      "100.5",
                      "1",
                      "-50.5",
                      "False"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "17.864285714285714",
                      "49.75",
                      "49.75",
                      "67.08333333333333",
                      "0.424897354830403",
                      "150",
                      "True"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "17.864285714285714",
                      "12.625",
                      "",
                      "",
                      "",
                      "",
                      "False"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "17.864285714285714",
                      "12.625",
                      "224.75",
                      "-599",
                      "1",
                      "224.75",
                      "False"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "17.864285714285714",
                      "12.625",
                      "12.625",
                      "108.08333333333333",
                      "0.010515006122844464",
                      "-199.5",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegressionOverWindowsQuery().Result).TextValue
        );
    }
}
