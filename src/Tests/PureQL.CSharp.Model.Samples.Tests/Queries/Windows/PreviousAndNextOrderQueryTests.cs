using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record PreviousAndNextOrderQueryTests
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
                      "alias": "order_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "placed_at",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
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
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                      }
                    },
                    {
                      "alias": "change",
                      "expression": {
                        "operator": "subtract",
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
                            "operator": "previous",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "operand": {
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
                                  "field": "order_tenant_id",
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "change_from_zero",
                      "expression": {
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
                            "operator": "previous",
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
                            "default": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            },
                            "over": {
                              "partitionBy": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_tenant_id",
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "days_since_previous",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "operator": "previous",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_tenant_id",
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
                        }
                      }
                    },
                    {
                      "alias": "is_first_order",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "previous",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_tenant_id",
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
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "total_two_orders_ago",
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
                            "name": "decimal"
                          }
                        },
                        "offset": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                      }
                    },
                    {
                      "alias": "next_placed_at",
                      "expression": {
                        "operator": "next",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                      }
                    },
                    {
                      "alias": "next_status",
                      "expression": {
                        "operator": "next",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "default": {
                          "type": {
                            "name": "string"
                          },
                          "value": "none"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                      }
                    },
                    {
                      "alias": "previous_shipped_on",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
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
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
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
                        "default": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
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
            new QueryJson(new PreviousAndNextOrderQuery().Value).TextValue
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
                      "name": "order_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "previous_total",
                      "type": "double"
                    },
                    {
                      "name": "change",
                      "type": "double"
                    },
                    {
                      "name": "change_from_zero",
                      "type": "double"
                    },
                    {
                      "name": "days_since_previous",
                      "type": "long"
                    },
                    {
                      "name": "is_first_order",
                      "type": "bool"
                    },
                    {
                      "name": "total_two_orders_ago",
                      "type": "double"
                    },
                    {
                      "name": "next_placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "next_status",
                      "type": "string"
                    },
                    {
                      "name": "previous_shipped_on",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "2024-06-01T10:00:00Z",
                      "100.5",
                      "",
                      "",
                      "100.5",
                      "",
                      "True",
                      "",
                      "2024-06-02T11:00:00Z",
                      "pending",
                      "2024-06-01"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "2024-06-02T11:00:00Z",
                      "50",
                      "100.5",
                      "-50.5",
                      "-50.5",
                      "1",
                      "False",
                      "",
                      "2024-06-03T12:00:00Z",
                      "shipped",
                      "2024-06-01"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "2024-06-03T12:00:00Z",
                      "200",
                      "50",
                      "150",
                      "150",
                      "1",
                      "False",
                      "100.5",
                      "",
                      "none",
                      ""
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2024-06-04T13:00:00Z",
                      "75.25",
                      "",
                      "",
                      "75.25",
                      "",
                      "True",
                      "",
                      "2024-06-05T14:00:00Z",
                      "shipped",
                      "2024-06-04"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2024-06-05T14:00:00Z",
                      "300",
                      "75.25",
                      "224.75",
                      "224.75",
                      "1",
                      "False",
                      "",
                      "2024-06-06T15:00:00Z",
                      "pending",
                      ""
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00Z",
                      "100.5",
                      "300",
                      "-199.5",
                      "-199.5",
                      "1",
                      "False",
                      "75.25",
                      "",
                      "none",
                      "2024-06-05"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PreviousAndNextOrderQuery().Result).TextValue
        );
    }
}
