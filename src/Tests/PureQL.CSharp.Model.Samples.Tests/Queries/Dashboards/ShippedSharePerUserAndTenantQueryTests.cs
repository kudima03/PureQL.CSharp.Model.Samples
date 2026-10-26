using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record ShippedSharePerUserAndTenantQueryTests
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
                  "groupBy": [
                    {
                      "alias": "order_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "order_tenant_id",
                      "expression": {
                        "key": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "shipped_pct_ann",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                                "value": "Ann"
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
                        "then": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0"
                        },
                        "else": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              },
                              {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "predicate": {
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
                                        {
                                          "operator": "equal",
                                          "type": {
                                            "name": "boolean"
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
                                            "value": "Ann"
                                          }
                                        }
                                      ]
                                    }
                                  },
                                  {
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
                                        "value": "Ann"
                                      }
                                    }
                                  }
                                ]
                              }
                            ]
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_pct_cara",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        },
                        "then": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0"
                        },
                        "else": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              },
                              {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "predicate": {
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
                                        {
                                          "operator": "equal",
                                          "type": {
                                            "name": "boolean"
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
                                      ]
                                    }
                                  },
                                  {
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
                                ]
                              }
                            ]
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "shipped_pct_dan",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
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
                                "value": "Dan"
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
                        "then": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0"
                        },
                        "else": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              },
                              {
                                "operator": "divide",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "predicate": {
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
                                        {
                                          "operator": "equal",
                                          "type": {
                                            "name": "boolean"
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
                                            "value": "Dan"
                                          }
                                        }
                                      ]
                                    }
                                  },
                                  {
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
                                        "value": "Dan"
                                      }
                                    }
                                  }
                                ]
                              }
                            ]
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ShippedSharePerUserAndTenantQuery().Value).TextValue
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
                      "name": "order_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "shipped_pct_ann",
                      "type": "double"
                    },
                    {
                      "name": "shipped_pct_cara",
                      "type": "double"
                    },
                    {
                      "name": "shipped_pct_dan",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "50",
                      "0",
                      "0"
                    ],
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "0",
                      "50",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ShippedSharePerUserAndTenantQuery().Result).TextValue
        );
    }
}
