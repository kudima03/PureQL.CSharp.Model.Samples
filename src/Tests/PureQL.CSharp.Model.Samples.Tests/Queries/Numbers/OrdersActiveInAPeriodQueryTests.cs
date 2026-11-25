using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record OrdersActiveInAPeriodQueryTests
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
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
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
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "shipped",
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
                      },
                      "alias": "shipped"
                    },
                    {
                      "kind": "left",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
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
                      "operator": "greatest",
                      "type": {
                        "name": "date"
                      },
                      "values": [
                        {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-03"
                        }
                      ]
                    },
                    "right": {
                      "operator": "least",
                      "type": {
                        "name": "date"
                      },
                      "values": [
                        {
                          "operator": "coalesce",
                          "type": {
                            "name": "date"
                          },
                          "values": [
                            {
                              "source": "shipped",
                              "field": "placed_on",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            },
                            {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-10"
                            }
                          ]
                        },
                        {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-05"
                        }
                      ]
                    }
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "active_from",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-03"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "active_to",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "operator": "coalesce",
                            "type": {
                              "name": "date"
                            },
                            "values": [
                              {
                                "source": "shipped",
                                "field": "placed_on",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                }
                              },
                              {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-10"
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-05"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "days_in_period",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "dateDiffDays",
                            "type": {
                              "name": "integer"
                            },
                            "left": {
                              "operator": "least",
                              "type": {
                                "name": "date"
                              },
                              "values": [
                                {
                                  "operator": "coalesce",
                                  "type": {
                                    "name": "date"
                                  },
                                  "values": [
                                    {
                                      "source": "shipped",
                                      "field": "placed_on",
                                      "type": {
                                        "name": "date",
                                        "nullable": true
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "date"
                                      },
                                      "value": "2024-06-10"
                                    }
                                  ]
                                },
                                {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-05"
                                }
                              ]
                            },
                            "right": {
                              "operator": "greatest",
                              "type": {
                                "name": "date"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-03"
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
                      "alias": "discount_applied",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "coalesce",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_score",
                                    "type": {
                                      "name": "decimal",
                                      "nullable": true
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 2
                                  }
                                ]
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
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
                    },
                    {
                      "alias": "payable",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
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
                                "operator": "least",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "coalesce",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal",
                                          "nullable": true
                                        },
                                        "values": [
                                          {
                                            "source": "schema_with_foreign_keys.users",
                                            "field": "user_score",
                                            "type": {
                                              "name": "decimal",
                                              "nullable": true
                                            }
                                          },
                                          {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": 2
                                          }
                                        ]
                                      },
                                      {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 0
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
                            ]
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    },
                    {
                      "alias": "discount_or_null",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_score",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 2
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
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-03"
                          }
                        ]
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
                """
            ).TextValue,
            new QueryJson(new OrdersActiveInAPeriodQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "active_from",
                      "type": "date"
                    },
                    {
                      "name": "active_to",
                      "type": "date"
                    },
                    {
                      "name": "days_in_period",
                      "type": "long"
                    },
                    {
                      "name": "discount_applied",
                      "type": "double"
                    },
                    {
                      "name": "payable",
                      "type": "double"
                    },
                    {
                      "name": "discount_or_null",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-03",
                      "2024-06-05",
                      "3",
                      "50",
                      "0",
                      "50"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-03",
                      "2024-06-03",
                      "1",
                      "0",
                      "200",
                      ""
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-05",
                      "2",
                      "60",
                      "15.25",
                      "60"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05",
                      "2024-06-05",
                      "1",
                      "60",
                      "240",
                      "60"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrdersActiveInAPeriodQuery().Result).TextValue
        );
    }
}
