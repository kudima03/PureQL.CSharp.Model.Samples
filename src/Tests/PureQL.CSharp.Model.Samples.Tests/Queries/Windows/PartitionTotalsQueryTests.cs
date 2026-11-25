using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record PartitionTotalsQueryTests
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
                      "alias": "order_user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_status",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "order_total",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "user_total",
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
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "share_of_user_total",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
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
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_user_id",
                                    "type": {
                                      "name": "uuid"
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "user_orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "user_largest_total",
                      "expression": {
                        "operator": "max",
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
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "is_user_largest",
                      "expression": {
                        "operator": "equal",
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
                          "operator": "max",
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
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "user_orders_in_status",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "user_shipped_total",
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
                        },
                        "predicate": {
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
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "all_total",
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
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PartitionTotalsQuery().Value).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "user_total",
                      "type": "double"
                    },
                    {
                      "name": "share_of_user_total",
                      "type": "double"
                    },
                    {
                      "name": "user_orders",
                      "type": "long"
                    },
                    {
                      "name": "user_largest_total",
                      "type": "double"
                    },
                    {
                      "name": "is_user_largest",
                      "type": "bool"
                    },
                    {
                      "name": "user_orders_in_status",
                      "type": "long"
                    },
                    {
                      "name": "user_shipped_total",
                      "type": "double"
                    },
                    {
                      "name": "all_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "shipped",
                      "100.5",
                      "150.5",
                      "0.6677740863787376",
                      "2",
                      "100.5",
                      "True",
                      "1",
                      "100.5",
                      "826.25"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "pending",
                      "50",
                      "150.5",
                      "0.33222591362126247",
                      "2",
                      "100.5",
                      "False",
                      "1",
                      "100.5",
                      "826.25"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "shipped",
                      "200",
                      "200",
                      "1",
                      "1",
                      "200",
                      "True",
                      "1",
                      "200",
                      "826.25"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "cancelled",
                      "75.25",
                      "375.25",
                      "0.2005329780146569",
                      "2",
                      "300",
                      "False",
                      "1",
                      "300",
                      "826.25"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "shipped",
                      "300",
                      "375.25",
                      "0.7994670219853431",
                      "2",
                      "300",
                      "True",
                      "1",
                      "300",
                      "826.25"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "00000004-0000-0000-0000-000000000000",
                      "pending",
                      "100.5",
                      "100.5",
                      "1",
                      "1",
                      "100.5",
                      "True",
                      "1",
                      "0",
                      "826.25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PartitionTotalsQuery().Result).TextValue
        );
    }
}
