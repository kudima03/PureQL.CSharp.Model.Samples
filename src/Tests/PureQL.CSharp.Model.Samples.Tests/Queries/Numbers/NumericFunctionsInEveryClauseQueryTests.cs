using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record NumericFunctionsInEveryClauseQueryTests
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
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "sign",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "operator": "subtract",
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
                                      "name": "decimal"
                                    },
                                    "value": "100"
                                  }
                                ]
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "abs",
                      "type": {
                        "name": "integer",
                        "nullable": true
                      },
                      "operand": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
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
                              "value": "shipped"
                            }
                          },
                          "then": {
                            "operator": "dateAddDays",
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
                            "days": {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
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
                                      "name": "decimal"
                                    },
                                    "value": "100"
                                  }
                                ]
                              }
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 3
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "hundreds",
                      "expression": {
                        "operator": "truncate",
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
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -2
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
                      "operator": "truncate",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "subtract",
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
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          ]
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
                      "alias": "tenant_id",
                      "expression": {
                        "key": "tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "hundreds",
                      "expression": {
                        "key": "hundreds",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "over_limit_by",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "abs",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "subtract",
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
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "whole_totals",
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
                            "operator": "truncate",
                            "type": {
                              "name": "integer"
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "operator": "abs",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "key": "hundreds",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NumericFunctionsInEveryClauseQuery().Value).TextValue
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
                      "name": "tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "hundreds",
                      "type": "double"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "over_limit_by",
                      "type": "double"
                    },
                    {
                      "name": "whole_totals",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "300",
                      "1",
                      "200",
                      "1"
                    ],
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "200",
                      "1",
                      "100",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NumericFunctionsInEveryClauseQuery().Result).TextValue
        );
    }
}
