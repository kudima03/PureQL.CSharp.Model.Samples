using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GreatestAndLeastOverAggregatesQueryTests
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
                      "alias": "tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "capped_hundreds",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
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
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "least",
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
                          "operator": "max",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          }
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 60
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
                      "alias": "capped_hundreds",
                      "expression": {
                        "key": "capped_hundreds",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "last_seen",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "datetime"
                        },
                        "values": [
                          {
                            "operator": "max",
                            "type": {
                              "name": "datetime"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "operator": "max",
                            "type": {
                              "name": "datetime"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "capped_revenue",
                      "expression": {
                        "operator": "least",
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
                              "name": "decimal"
                            },
                            "value": "250"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "share_of_all_capped",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
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
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              },
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
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.4"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "covered",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "least",
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
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_age",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 3
                                }
                              ]
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "over_limit_orders",
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
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_age",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 3
                                  }
                                ]
                              }
                            ]
                          },
                          "right": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "pages",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "integerDivide",
                            "type": {
                              "name": "integer"
                            },
                            "left": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
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
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "datetime"
                        },
                        "values": [
                          {
                            "operator": "max",
                            "type": {
                              "name": "datetime"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "operator": "max",
                            "type": {
                              "name": "datetime"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          }
                        ]
                      },
                      "direction": "desc"
                    },
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
                        "key": "capped_hundreds",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GreatestAndLeastOverAggregatesQuery().Value).TextValue
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
                      "name": "capped_hundreds",
                      "type": "double"
                    },
                    {
                      "name": "last_seen",
                      "type": "datetime"
                    },
                    {
                      "name": "capped_revenue",
                      "type": "double"
                    },
                    {
                      "name": "share_of_all_capped",
                      "type": "double"
                    },
                    {
                      "name": "covered",
                      "type": "double"
                    },
                    {
                      "name": "over_limit_orders",
                      "type": "long"
                    },
                    {
                      "name": "pages",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "100",
                      "2024-06-06T15:00:00Z",
                      "250",
                      "0.4",
                      "190.5",
                      "1",
                      "1"
                    ],
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "0",
                      "2024-06-04T13:00:00Z",
                      "75.25",
                      "0.09107413010590015",
                      "75.25",
                      "0",
                      "1"
                    ],
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "100",
                      "2024-06-03T12:00:00Z",
                      "250",
                      "0.3636913767019667",
                      "165",
                      "2",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GreatestAndLeastOverAggregatesQuery().Result).TextValue
        );
    }
}
