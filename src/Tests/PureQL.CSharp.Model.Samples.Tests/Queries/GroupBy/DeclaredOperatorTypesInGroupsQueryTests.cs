using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record DeclaredOperatorTypesInGroupsQueryTests
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
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "add",
                      "type": {
                        "name": "integer"
                      },
                      "values": [
                        {
                          "operator": "if",
                          "type": {
                            "name": "integer"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "else": {
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
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "2"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "key": "user_id",
                        "type": {
                          "name": "uuid"
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
                      "alias": "items",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "integer"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "else": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "avg_items",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "integer"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
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
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "else": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "smallest",
                      "expression": {
                        "operator": "min",
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
                    },
                    {
                      "alias": "smallest_overall",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "decimal"
                        },
                        "over": "all",
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "points",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "max",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "selector": {
                              "operator": "if",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "condition": {
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
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000002-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "integer",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "else": {
                                "operator": "integerDivide",
                                "type": {
                                  "name": "integer"
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
                                  "value": 25
                                }
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0.5"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "best",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "condition": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
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
                            "value": 1
                          }
                        },
                        "then": {
                          "operator": "max",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "operator": "if",
                            "type": {
                              "name": "integer"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_tenant_id",
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
                                "name": "integer"
                              },
                              "value": 1
                            },
                            "else": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        },
                        "else": {
                          "operator": "average",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "if",
                            "type": {
                              "name": "decimal",
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
                                "value": "pending"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "value": null
                            },
                            "else": {
                              "operator": "multiply",
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
                                  "value": "0.1"
                                }
                              ]
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "rounded",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DeclaredOperatorTypesInGroupsQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "items",
                      "type": "long"
                    },
                    {
                      "name": "avg_items",
                      "type": "double"
                    },
                    {
                      "name": "smallest",
                      "type": "double"
                    },
                    {
                      "name": "smallest_overall",
                      "type": "double"
                    },
                    {
                      "name": "points",
                      "type": "double"
                    },
                    {
                      "name": "best",
                      "type": "double"
                    },
                    {
                      "name": "rounded",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2",
                      "2",
                      "1",
                      "50",
                      "50",
                      "4",
                      "1",
                      "0"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "1",
                      "200",
                      "50",
                      "0.5",
                      "20",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DeclaredOperatorTypesInGroupsQuery().Result).TextValue
        );
    }
}
