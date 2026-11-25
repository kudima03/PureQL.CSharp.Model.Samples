using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record BisectionOfARateQueryTests
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
                              "value": "000007d1-0000-0000-0000-000000000000"
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
                              "value": "-1500"
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
                              "value": "000007d1-0000-0000-0000-000000000000"
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
                              "value": "000007d1-0000-0000-0000-000000000000"
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
                              "value": "700"
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
                              "value": "000007d1-0000-0000-0000-000000000000"
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
                              "value": "600"
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
                              "value": "000007d2-0000-0000-0000-000000000000"
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
                              "value": "000007d2-0000-0000-0000-000000000000"
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
                              "value": "100"
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
                              "value": "000007d2-0000-0000-0000-000000000000"
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
                              "value": "000007d2-0000-0000-0000-000000000000"
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
                              "value": "1000"
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
                              "value": "000007d3-0000-0000-0000-000000000000"
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
                              "value": "-300"
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
                              "value": "000007d3-0000-0000-0000-000000000000"
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
                              "value": "150"
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
                              "value": "000007d3-0000-0000-0000-000000000000"
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
                              "value": "150"
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
                              "value": "000007d3-0000-0000-0000-000000000000"
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
                              "value": "150"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "bounds",
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
                              "alias": "low",
                              "expression": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "-0.99"
                              }
                            },
                            {
                              "alias": "high",
                              "expression": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "10"
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "bounds"
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
                                  "source": "bounds",
                                  "field": "investment_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            }
                          ],
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "bounds",
                              "field": "step",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 40
                            }
                          },
                          "groupBy": [
                            {
                              "alias": "investment_id",
                              "expression": {
                                "source": "bounds",
                                "field": "investment_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "step",
                              "expression": {
                                "source": "bounds",
                                "field": "step",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            },
                            {
                              "alias": "low",
                              "expression": {
                                "source": "bounds",
                                "field": "low",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            },
                            {
                              "alias": "high",
                              "expression": {
                                "source": "bounds",
                                "field": "high",
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
                              "operator": "subtract",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "key": "high",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "key": "low",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              ]
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.001"
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
                              "alias": "low",
                              "expression": {
                                "operator": "if",
                                "type": {
                                  "name": "decimal"
                                },
                                "condition": {
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
                                                "operator": "divide",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "add",
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "values": [
                                                      {
                                                        "source": "bounds",
                                                        "field": "low",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      },
                                                      {
                                                        "source": "bounds",
                                                        "field": "high",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 2
                                                  }
                                                ]
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
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 0
                                  }
                                },
                                "then": {
                                  "operator": "divide",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "operator": "add",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "values": [
                                        {
                                          "key": "low",
                                          "type": {
                                            "name": "decimal"
                                          }
                                        },
                                        {
                                          "key": "high",
                                          "type": {
                                            "name": "decimal"
                                          }
                                        }
                                      ]
                                    },
                                    {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 2
                                    }
                                  ]
                                },
                                "else": {
                                  "key": "low",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            },
                            {
                              "alias": "high",
                              "expression": {
                                "operator": "if",
                                "type": {
                                  "name": "decimal"
                                },
                                "condition": {
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
                                                "operator": "divide",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "add",
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "values": [
                                                      {
                                                        "source": "bounds",
                                                        "field": "low",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      },
                                                      {
                                                        "source": "bounds",
                                                        "field": "high",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 2
                                                  }
                                                ]
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
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 0
                                  }
                                },
                                "then": {
                                  "key": "high",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "else": {
                                  "operator": "divide",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "operator": "add",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "values": [
                                        {
                                          "key": "low",
                                          "type": {
                                            "name": "decimal"
                                          }
                                        },
                                        {
                                          "key": "high",
                                          "type": {
                                            "name": "decimal"
                                          }
                                        }
                                      ]
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
                            }
                          ]
                        },
                        "maxDepth": 50
                      }
                    }
                  ],
                  "from": {
                    "subquery": "bounds"
                  },
                  "groupBy": [
                    {
                      "alias": "investment_id",
                      "expression": {
                        "source": "bounds",
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
                      "alias": "low",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "bounds",
                          "field": "low",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "high",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "bounds",
                          "field": "high",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "steps",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "bounds",
                          "field": "step",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new BisectionOfARateQuery().Value).TextValue
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
                      "name": "low",
                      "type": "double"
                    },
                    {
                      "name": "high",
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
                      "000007d1-0000-0000-0000-000000000000",
                      "0.093974609375",
                      "0.0946453857421875",
                      "14"
                    ],
                    [
                      "000007d2-0000-0000-0000-000000000000",
                      "0.1026947021484375",
                      "0.103365478515625",
                      "14"
                    ],
                    [
                      "000007d3-0000-0000-0000-000000000000",
                      "0.23349609375",
                      "0.2341668701171875",
                      "14"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new BisectionOfARateQuery().Result).TextValue
        );
    }
}
