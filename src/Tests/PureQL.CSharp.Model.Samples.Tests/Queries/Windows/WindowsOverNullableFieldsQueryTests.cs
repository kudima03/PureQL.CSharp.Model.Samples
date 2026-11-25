using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record WindowsOverNullableFieldsQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            }
                          },
                          "right": {
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "total",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "average",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "largest_so_far",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            }
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_order_at",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              }
                            }
                          }
                        ],
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "average_score_in_tenant",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "lowest_score_in_tenant",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "earliest_sign_up_in_tenant",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "shipped_average",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1"
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
                              "name": "string",
                              "nullable": true
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
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "previous_total",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "previous_total_or_zero",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            }
                          ]
                        },
                        "default": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0"
                        }
                      }
                    },
                    {
                      "alias": "previous_sign_up",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "default": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "users_in_tenant_of_order_time",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "rank_by_placed_at",
                      "expression": {
                        "operator": "rank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                }
                              },
                              "direction": "desc"
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
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
                          "name": "datetime",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WindowsOverNullableFieldsQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "total",
                      "type": "double"
                    },
                    {
                      "name": "average",
                      "type": "double"
                    },
                    {
                      "name": "largest_so_far",
                      "type": "double"
                    },
                    {
                      "name": "first_order_at",
                      "type": "datetime"
                    },
                    {
                      "name": "average_score_in_tenant",
                      "type": "double"
                    },
                    {
                      "name": "lowest_score_in_tenant",
                      "type": "double"
                    },
                    {
                      "name": "earliest_sign_up_in_tenant",
                      "type": "date"
                    },
                    {
                      "name": "shipped_average",
                      "type": "double"
                    },
                    {
                      "name": "previous_total",
                      "type": "double"
                    },
                    {
                      "name": "previous_total_or_zero",
                      "type": "double"
                    },
                    {
                      "name": "previous_sign_up",
                      "type": "date"
                    },
                    {
                      "name": "users_in_tenant_of_order_time",
                      "type": "long"
                    },
                    {
                      "name": "rank_by_placed_at",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000065-0000-0000-0000-000000000000",
                      "2",
                      "150.5",
                      "75.25",
                      "100.5",
                      "2024-06-01T10:00:00Z",
                      "30",
                      "30",
                      "2019-07-10",
                      "1",
                      "",
                      "0",
                      "2019-07-10",
                      "1",
                      "6"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000066-0000-0000-0000-000000000000",
                      "2",
                      "150.5",
                      "75.25",
                      "100.5",
                      "2024-06-01T10:00:00Z",
                      "30",
                      "30",
                      "2019-07-10",
                      "1",
                      "100.5",
                      "100.5",
                      "2020-01-15",
                      "1",
                      "5"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "00000067-0000-0000-0000-000000000000",
                      "1",
                      "200",
                      "200",
                      "200",
                      "2024-06-03T12:00:00Z",
                      "30",
                      "30",
                      "2019-07-10",
                      "1",
                      "",
                      "0",
                      "2020-01-15",
                      "1",
                      "4"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "00000068-0000-0000-0000-000000000000",
                      "2",
                      "375.25",
                      "187.625",
                      "75.25",
                      "2024-06-04T13:00:00Z",
                      "30",
                      "30",
                      "2019-07-10",
                      "1",
                      "",
                      "0",
                      "2019-07-10",
                      "1",
                      "3"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "00000069-0000-0000-0000-000000000000",
                      "2",
                      "375.25",
                      "187.625",
                      "300",
                      "2024-06-04T13:00:00Z",
                      "30",
                      "30",
                      "2019-07-10",
                      "1",
                      "75.25",
                      "75.25",
                      "2019-07-10",
                      "1",
                      "2"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "0000006a-0000-0000-0000-000000000000",
                      "1",
                      "100.5",
                      "100.5",
                      "100.5",
                      "2024-06-06T15:00:00Z",
                      "19",
                      "10",
                      "2020-01-15",
                      "",
                      "",
                      "0",
                      "2021-03-20",
                      "1",
                      "1"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "",
                      "0",
                      "0",
                      "",
                      "",
                      "",
                      "19",
                      "10",
                      "2020-01-15",
                      "",
                      "",
                      "0",
                      "2022-11-05",
                      "2",
                      "7"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "",
                      "0",
                      "0",
                      "",
                      "",
                      "",
                      "19",
                      "10",
                      "2020-01-15",
                      "",
                      "",
                      "0",
                      "2020-01-15",
                      "2",
                      "7"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WindowsOverNullableFieldsQuery().Result).TextValue
        );
    }
}
