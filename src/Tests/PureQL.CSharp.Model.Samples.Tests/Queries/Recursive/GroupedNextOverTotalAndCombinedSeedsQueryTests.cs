using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record GroupedNextOverTotalAndCombinedSeedsQueryTests
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
                              "value": "420"
                            }
                          }
                        ],
                        [
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
                              "value": "680"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "totals",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "cash_flows"
                          },
                          "aggregate": [
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
                              "alias": "balance",
                              "expression": {
                                "operator": "sum",
                                "type": {
                                  "name": "decimal"
                                },
                                "selector": {
                                  "source": "cash_flows",
                                  "field": "amount",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "totals"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "cash_flows",
                              "on": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "cash_flows",
                                  "field": "period",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "right": {
                                  "source": "totals",
                                  "field": "step",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            }
                          ],
                          "groupBy": [
                            {
                              "alias": "step",
                              "expression": {
                                "source": "totals",
                                "field": "step",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            }
                          ],
                          "select": [
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
                              "alias": "balance",
                              "expression": {
                                "operator": "sum",
                                "type": {
                                  "name": "decimal"
                                },
                                "selector": {
                                  "source": "cash_flows",
                                  "field": "amount",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 5
                      }
                    },
                    {
                      "name": "from_a_union",
                      "recursive": {
                        "seed": {
                          "unionAll": [
                            {
                              "from": {
                                "subquery": "cash_flows"
                              },
                              "select": [
                                {
                                  "alias": "period",
                                  "expression": {
                                    "source": "cash_flows",
                                    "field": "period",
                                    "type": {
                                      "name": "integer"
                                    }
                                  }
                                },
                                {
                                  "alias": "amount",
                                  "expression": {
                                    "source": "cash_flows",
                                    "field": "amount",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                }
                              ]
                            },
                            {
                              "from": {
                                "subquery": "totals"
                              },
                              "select": [
                                {
                                  "alias": "period",
                                  "expression": {
                                    "source": "totals",
                                    "field": "step",
                                    "type": {
                                      "name": "integer"
                                    }
                                  }
                                },
                                {
                                  "alias": "amount",
                                  "expression": {
                                    "source": "totals",
                                    "field": "balance",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  }
                                }
                              ]
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "from_a_union"
                          },
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "from_a_union",
                              "field": "period",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 12
                            }
                          },
                          "groupBy": [
                            {
                              "alias": "period",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "from_a_union",
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
                          ],
                          "having": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "from_a_union",
                                "field": "amount",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          },
                          "select": [
                            {
                              "alias": "period",
                              "expression": {
                                "key": "period",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            },
                            {
                              "alias": "amount",
                              "expression": {
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
                                      "source": "from_a_union",
                                      "field": "amount",
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
                            }
                          ]
                        },
                        "maxDepth": 12
                      }
                    },
                    {
                      "name": "without_aggregates",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "totals"
                          },
                          "select": [
                            {
                              "alias": "step",
                              "expression": {
                                "source": "totals",
                                "field": "step",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "without_aggregates"
                          },
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "without_aggregates",
                              "field": "step",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          "select": [
                            {
                              "alias": "step",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "without_aggregates",
                                    "field": "step",
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
                        },
                        "maxDepth": 3
                      }
                    }
                  ],
                  "from": {
                    "subquery": "totals"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "from_a_union",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "from_a_union",
                          "field": "period",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "source": "totals",
                          "field": "step",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "without_aggregates",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "without_aggregates",
                          "field": "step",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "source": "totals",
                          "field": "step",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "step",
                      "expression": {
                        "source": "totals",
                        "field": "step",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "balance",
                      "expression": {
                        "source": "totals",
                        "field": "balance",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "amount",
                      "expression": {
                        "source": "from_a_union",
                        "field": "amount",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "plain_step",
                      "expression": {
                        "source": "without_aggregates",
                        "field": "step",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupedNextOverTotalAndCombinedSeedsQuery().Value).TextValue
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
                      "name": "step",
                      "type": "long"
                    },
                    {
                      "name": "balance",
                      "type": "double"
                    },
                    {
                      "name": "amount",
                      "type": "double"
                    },
                    {
                      "name": "plain_step",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0",
                      "400",
                      "-1000",
                      "0"
                    ],
                    [
                      "0",
                      "400",
                      "400",
                      "0"
                    ],
                    [
                      "1",
                      "1400",
                      "300",
                      "1"
                    ],
                    [
                      "1",
                      "1400",
                      "300",
                      "1"
                    ],
                    [
                      "1",
                      "1400",
                      "1400",
                      "1"
                    ],
                    [
                      "1",
                      "1400",
                      "1400",
                      "1"
                    ],
                    [
                      "2",
                      "1100",
                      "420",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "420",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "420",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "850",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "850",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "850",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "1100",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "1100",
                      "2"
                    ],
                    [
                      "2",
                      "1100",
                      "1100",
                      "2"
                    ],
                    [
                      "3",
                      "680",
                      "425",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "425",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "425",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "425",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "680",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "760",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "760",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "760",
                      "3"
                    ],
                    [
                      "3",
                      "680",
                      "760",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new GroupedNextOverTotalAndCombinedSeedsQuery().Result
            ).TextValue
        );
    }
}
