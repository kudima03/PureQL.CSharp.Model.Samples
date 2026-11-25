using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullIfEqualInEveryClauseOfAGroupedQueryTests
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
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "uuid"
                            },
                            "value": "00000004-0000-0000-0000-000000000000"
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
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "nullIfEqual",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "left": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "string"
                        },
                        "value": "cancelled"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "value": null
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "status",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "pending"
                        }
                      }
                    },
                    {
                      "alias": "user_name",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Cara"
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
                      "operator": "divide",
                      "type": {
                        "name": "decimal",
                        "nullable": true
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
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "left": {
                            "operator": "sum",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
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
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "0.5"
                    }
                  },
                  "select": [
                    {
                      "alias": "status",
                      "expression": {
                        "key": "status",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "user_name",
                      "expression": {
                        "key": "user_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "total_per_score",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
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
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_score",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
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
                        ]
                      }
                    },
                    {
                      "alias": "average_non_placeholder_total",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 50
                          }
                        }
                      }
                    },
                    {
                      "alias": "placeholder_totals",
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
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 50
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "divide",
                            "type": {
                              "name": "decimal",
                              "nullable": true
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
                                "operator": "nullIfEqual",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "left": {
                                  "operator": "sum",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "selector": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_score",
                                    "type": {
                                      "name": "decimal",
                                      "nullable": true
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
                            ]
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
                          }
                        ]
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "status",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "user_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullIfEqualInEveryClauseOfAGroupedQuery().Value).TextValue
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "total_per_score",
                      "type": "double"
                    },
                    {
                      "name": "average_non_placeholder_total",
                      "type": "double"
                    },
                    {
                      "name": "placeholder_totals",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped",
                      "",
                      "10",
                      "300",
                      "0"
                    ],
                    [
                      "shipped",
                      "Ann",
                      "3.35",
                      "100.5",
                      "0"
                    ],
                    [
                      "",
                      "Ann",
                      "1.6666666666666667",
                      "",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new NullIfEqualInEveryClauseOfAGroupedQuery().Result
            ).TextValue
        );
    }
}
