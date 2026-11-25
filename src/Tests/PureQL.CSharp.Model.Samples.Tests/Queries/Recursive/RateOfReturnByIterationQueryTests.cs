using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record RateOfReturnByIterationQueryTests
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
                      "name": "cash_flows",
                      "rows": [
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003e9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "-1000"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003e9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "300"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003e9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "400"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003e9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "500"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003ea-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "-500"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003ea-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "200"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003ea-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "200"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "investment_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000003ea-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "period",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "amount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "200"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "rate",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "cash_flows"
                          },
                          "groupBy": [
                            {
                              "alias": "investment_id",
                              "expression": {
                                "source": "cash_flows",
                                "field": "investment_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ],
                          "select": [
                            {
                              "alias": "investment_id",
                              "expression": {
                                "key": "investment_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "step",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "rate",
                              "expression": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "0.1"
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "rate"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "cash_flows",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "cash_flows",
                                  "field": "investment_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "source": "rate",
                                  "field": "investment_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            }
                          ],
                          "groupBy": [
                            {
                              "alias": "investment_id",
                              "expression": {
                                "source": "rate",
                                "field": "investment_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "step",
                              "expression": {
                                "source": "rate",
                                "field": "step",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            },
                            {
                              "alias": "rate",
                              "expression": {
                                "source": "rate",
                                "field": "rate",
                                "type": {
                                  "name": "decimal"
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
                              "operator": "abs",
                              "type": {
                                "name": "decimal"
                              },
                              "operand": {
                                "operator": "sum",
                                "type": {
                                  "name": "decimal"
                                },
                                "selector": {
                                  "operator": "divide",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "source": "cash_flows",
                                      "field": "amount",
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
                                            "source": "rate",
                                            "field": "rate",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          }
                                        ]
                                      },
                                      "exponent": {
                                        "source": "cash_flows",
                                        "field": "period",
                                        "type": {
                                          "name": "integer"
                                        }
                                      }
                                    }
                                  ]
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.0000001"
                            }
                          },
                          "select": [
                            {
                              "alias": "investment_id",
                              "expression": {
                                "key": "investment_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "step",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "key": "step",
                                    "type": {
                                      "name": "integer"
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
                              "alias": "rate",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "key": "rate",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "operator": "divide",
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
                                          "operator": "divide",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "values": [
                                            {
                                              "source": "cash_flows",
                                              "field": "amount",
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
                                                    "source": "rate",
                                                    "field": "rate",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  }
                                                ]
                                              },
                                              "exponent": {
                                                "source": "cash_flows",
                                                "field": "period",
                                                "type": {
                                                  "name": "integer"
                                                }
                                              }
                                            }
                                          ]
                                        }
                                      },
                                      {
                                        "operator": "sum",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "selector": {
                                          "operator": "divide",
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
                                                  "source": "cash_flows",
                                                  "field": "period",
                                                  "type": {
                                                    "name": "integer"
                                                  }
                                                },
                                                {
                                                  "source": "cash_flows",
                                                  "field": "amount",
                                                  "type": {
                                                    "name": "decimal"
                                                  }
                                                }
                                              ]
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
                                                    "source": "rate",
                                                    "field": "rate",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  }
                                                ]
                                              },
                                              "exponent": {
                                                "operator": "add",
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "values": [
                                                  {
                                                    "source": "cash_flows",
                                                    "field": "period",
                                                    "type": {
                                                      "name": "integer"
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
                                      }
                                    ]
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "maxDepth": 50
                      }
                    }
                  ],
                  "from": {
                    "subquery": "rate"
                  },
                  "select": [
                    {
                      "alias": "investment_id",
                      "expression": {
                        "source": "rate",
                        "field": "investment_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "internal_rate_of_return",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "source": "rate",
                          "field": "rate",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        }
                      }
                    },
                    {
                      "alias": "steps",
                      "expression": {
                        "source": "rate",
                        "field": "step",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ],
                  "distinctBy": [
                    "investment_id"
                  ],
                  "orderBy": [
                    {
                      "column": "investment_id"
                    },
                    {
                      "column": "steps",
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RateOfReturnByIterationQuery().Value).TextValue
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
                      "name": "investment_id",
                      "type": "uuid"
                    },
                    {
                      "name": "internal_rate_of_return",
                      "type": "double"
                    },
                    {
                      "name": "steps",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000003e9-0000-0000-0000-000000000000",
                      "0.088963",
                      "3"
                    ],
                    [
                      "000003ea-0000-0000-0000-000000000000",
                      "0.09701",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RateOfReturnByIterationQuery().Result).TextValue
        );
    }
}
