using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record MonthsWithoutOrdersFilledWithZeroQueryTests
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
                      "name": "months",
                      "rows": [
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 6
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 8
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 9
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 11
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          }
                        ],
                        [
                          {
                            "alias": "month",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 12
                            }
                          },
                          {
                            "alias": "quarter",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "first_months",
                      "query": {
                        "from": {
                          "subquery": "months"
                        },
                        "where": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "modulo",
                            "type": {
                              "name": "integer"
                            },
                            "left": {
                              "source": "months",
                              "field": "month",
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
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        },
                        "select": [
                          {
                            "alias": "month",
                            "expression": {
                              "source": "months",
                              "field": "month",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "monthly",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "month",
                            "expression": {
                              "operator": "month",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "month",
                            "expression": {
                              "key": "month",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "alias": "revenue",
                            "expression": {
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
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "every_month",
                      "query": {
                        "unionAll": [
                          {
                            "from": {
                              "subquery": "monthly"
                            },
                            "select": [
                              {
                                "alias": "month",
                                "expression": {
                                  "source": "monthly",
                                  "field": "month",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              },
                              {
                                "alias": "revenue",
                                "expression": {
                                  "source": "monthly",
                                  "field": "revenue",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "subquery": "months"
                            },
                            "where": {
                              "operator": "not",
                              "type": {
                                "name": "boolean"
                              },
                              "condition": {
                                "operator": "in",
                                "type": {
                                  "name": "boolean"
                                },
                                "operand": {
                                  "source": "months",
                                  "field": "month",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "list": {
                                  "subquery": "monthly",
                                  "field": "month",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              }
                            },
                            "select": [
                              {
                                "alias": "month",
                                "expression": {
                                  "source": "months",
                                  "field": "month",
                                  "type": {
                                    "name": "integer"
                                  }
                                }
                              },
                              {
                                "alias": "revenue",
                                "expression": {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "0"
                                }
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "every_month"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "months",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "every_month",
                          "field": "month",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "source": "months",
                          "field": "month",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "quarter",
                      "expression": {
                        "source": "months",
                        "field": "quarter",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "month",
                      "expression": {
                        "source": "months",
                        "field": "month",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "source": "every_month",
                        "field": "revenue",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "starts_a_quarter",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "months",
                          "field": "month",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "list": {
                          "subquery": "first_months",
                          "field": "month",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "months",
                        "field": "month",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MonthsWithoutOrdersFilledWithZeroQuery().Value).TextValue
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
                      "name": "quarter",
                      "type": "long"
                    },
                    {
                      "name": "month",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    },
                    {
                      "name": "starts_a_quarter",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "1",
                      "0",
                      "True"
                    ],
                    [
                      "1",
                      "2",
                      "0",
                      "False"
                    ],
                    [
                      "1",
                      "3",
                      "0",
                      "False"
                    ],
                    [
                      "2",
                      "4",
                      "0",
                      "True"
                    ],
                    [
                      "2",
                      "5",
                      "0",
                      "False"
                    ],
                    [
                      "2",
                      "6",
                      "826.25",
                      "False"
                    ],
                    [
                      "3",
                      "7",
                      "0",
                      "True"
                    ],
                    [
                      "3",
                      "8",
                      "0",
                      "False"
                    ],
                    [
                      "3",
                      "9",
                      "0",
                      "False"
                    ],
                    [
                      "4",
                      "10",
                      "0",
                      "True"
                    ],
                    [
                      "4",
                      "11",
                      "0",
                      "False"
                    ],
                    [
                      "4",
                      "12",
                      "0",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MonthsWithoutOrdersFilledWithZeroQuery().Result).TextValue
        );
    }
}
