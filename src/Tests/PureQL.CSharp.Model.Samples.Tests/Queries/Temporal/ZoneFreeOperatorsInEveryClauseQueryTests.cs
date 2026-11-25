using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record ZoneFreeOperatorsInEveryClauseQueryTests
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
                      "name": "ranked",
                      "query": {
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
                            "alias": "rank_in_second",
                            "expression": {
                              "operator": "rowNumber",
                              "type": {
                                "name": "integer"
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "operator": "startOfSecond",
                                    "type": {
                                      "name": "datetime"
                                    },
                                    "operand": {
                                      "operator": "datetimeAddSeconds",
                                      "type": {
                                        "name": "datetime"
                                      },
                                      "operand": {
                                        "type": {
                                          "name": "datetime"
                                        },
                                        "value": "2024-06-01T10:00:00Z"
                                      },
                                      "seconds": {
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
                                ],
                                "orderBy": [
                                  {
                                    "expression": {
                                      "operator": "microsecond",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "operand": {
                                        "operator": "datetimeAddSeconds",
                                        "type": {
                                          "name": "datetime"
                                        },
                                        "operand": {
                                          "type": {
                                            "name": "datetime"
                                          },
                                          "value": "2024-06-01T10:00:00Z"
                                        },
                                        "seconds": {
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
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "ranked",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "ranked",
                          "field": "id",
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
                    },
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "alias": "shipped",
                      "on": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "shipped",
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
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "shipped",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "dateDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-07-05"
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      },
                      {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "microsecond",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "datetimeAddSeconds",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-01T10:00:00Z"
                            },
                            "seconds": {
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
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 700000
                        }
                      }
                    ]
                  },
                  "groupBy": [
                    {
                      "alias": "second",
                      "expression": {
                        "operator": "startOfSecond",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "operator": "datetimeAddSeconds",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-01T10:00:00Z"
                          },
                          "seconds": {
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
                      "alias": "renewal",
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
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "shipped_renewal",
                      "expression": {
                        "operator": "dateAddMonths",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "shipped",
                          "field": "placed_on",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "second",
                      "expression": {
                        "key": "second",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "renewal",
                      "expression": {
                        "key": "renewal",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "shipped_renewal",
                      "expression": {
                        "key": "shipped_renewal",
                        "type": {
                          "name": "date",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "latest_microsecond",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "microsecond",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "datetimeAddSeconds",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-01T10:00:00Z"
                            },
                            "seconds": {
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
                      }
                    },
                    {
                      "alias": "first_ranks",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "ranked",
                            "field": "rank_in_second",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_same_month",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "dateDiffMonths",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "source": "shipped",
                              "field": "placed_on",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
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
                      "operator": "dateDiffMonths",
                      "type": {
                        "name": "integer"
                      },
                      "left": {
                        "key": "renewal",
                        "type": {
                          "name": "date"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-06-02"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ZoneFreeOperatorsInEveryClauseQuery().Value).TextValue
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
                      "name": "second",
                      "type": "datetime"
                    },
                    {
                      "name": "renewal",
                      "type": "date"
                    },
                    {
                      "name": "shipped_renewal",
                      "type": "date"
                    },
                    {
                      "name": "latest_microsecond",
                      "type": "long"
                    },
                    {
                      "name": "first_ranks",
                      "type": "long"
                    },
                    {
                      "name": "shipped_same_month",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01T10:00:00Z",
                      "2024-07-02",
                      "",
                      "500000",
                      "1",
                      "0"
                    ],
                    [
                      "2024-06-01T10:00:02Z",
                      "2024-07-03",
                      "2024-07-03",
                      "0",
                      "1",
                      "1"
                    ],
                    [
                      "2024-06-01T10:00:03Z",
                      "2024-07-05",
                      "2024-07-05",
                      "0",
                      "1",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ZoneFreeOperatorsInEveryClauseQuery().Result).TextValue
        );
    }
}
