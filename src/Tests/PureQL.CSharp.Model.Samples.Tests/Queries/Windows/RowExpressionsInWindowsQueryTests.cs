using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RowExpressionsInWindowsQueryTests
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
                  "where": {
                    "source": "schema_with_foreign_keys.users",
                    "field": "user_active",
                    "type": {
                      "name": "boolean"
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
                      "alias": "local_month",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000385-0000-0000-0000-000000000000"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Asia/Tokyo"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Pacific/Auckland"
                          }
                        }
                      }
                    },
                    {
                      "alias": "month_revenue",
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
                            },
                            {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000385-0000-0000-0000-000000000000"
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Auckland"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "share_of_month",
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
                                  },
                                  {
                                    "operator": "startOfMonth",
                                    "type": {
                                      "name": "datetime"
                                    },
                                    "operand": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_at",
                                      "type": {
                                        "name": "datetime"
                                      }
                                    },
                                    "zone": {
                                      "operator": "if",
                                      "type": {
                                        "name": "string"
                                      },
                                      "condition": {
                                        "operator": "equal",
                                        "type": {
                                          "name": "boolean"
                                        },
                                        "left": {
                                          "source": "schema_with_foreign_keys.users",
                                          "field": "user_tenant_id",
                                          "type": {
                                            "name": "uuid"
                                          }
                                        },
                                        "right": {
                                          "type": {
                                            "name": "uuid"
                                          },
                                          "value": "00000385-0000-0000-0000-000000000000"
                                        }
                                      },
                                      "then": {
                                        "type": {
                                          "name": "string"
                                        },
                                        "value": "Asia/Tokyo"
                                      },
                                      "else": {
                                        "type": {
                                          "name": "string"
                                        },
                                        "value": "Pacific/Auckland"
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "last_seven_local_days",
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
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "asDate",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_tenant_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "uuid"
                                      },
                                      "value": "00000385-0000-0000-0000-000000000000"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Asia/Tokyo"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Pacific/Auckland"
                                  }
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "days": 6
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "days_since_previous",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Pacific/Auckland"
                            }
                          }
                        },
                        "right": {
                          "operator": "previous",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Auckland"
                              }
                            }
                          },
                          "default": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Pacific/Auckland"
                              }
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
                      "alias": "next_local_day",
                      "expression": {
                        "operator": "next",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Pacific/Auckland"
                            }
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
                      "alias": "rank_in_local_day",
                      "expression": {
                        "operator": "rank",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "asDate",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000385-0000-0000-0000-000000000000"
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Pacific/Auckland"
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
                                      "value": 0
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "0.01"
                                  }
                                ]
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
            new QueryJson(new RowExpressionsInWindowsQuery().Value).TextValue
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
                      "name": "local_month",
                      "type": "datetime"
                    },
                    {
                      "name": "month_revenue",
                      "type": "double"
                    },
                    {
                      "name": "share_of_month",
                      "type": "double"
                    },
                    {
                      "name": "last_seven_local_days",
                      "type": "double"
                    },
                    {
                      "name": "days_since_previous",
                      "type": "long"
                    },
                    {
                      "name": "next_local_day",
                      "type": "date"
                    },
                    {
                      "name": "rank_in_local_day",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-05-31T15:00:00Z",
                      "150.5",
                      "0.6677740863787376",
                      "100.5",
                      "0",
                      "2024-06-02",
                      "1"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-05-31T15:00:00Z",
                      "150.5",
                      "0.33222591362126247",
                      "150.5",
                      "1",
                      "",
                      "1"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-05-31T15:00:00Z",
                      "375.25",
                      "0.2005329780146569",
                      "75.25",
                      "5",
                      "2024-06-05",
                      "1"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-05-31T15:00:00Z",
                      "375.25",
                      "0.7994670219853431",
                      "375.25",
                      "1",
                      "",
                      "1"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-05-31T12:00:00Z",
                      "100.5",
                      "1",
                      "100.5",
                      "3",
                      "",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RowExpressionsInWindowsQuery().Result).TextValue
        );
    }
}
