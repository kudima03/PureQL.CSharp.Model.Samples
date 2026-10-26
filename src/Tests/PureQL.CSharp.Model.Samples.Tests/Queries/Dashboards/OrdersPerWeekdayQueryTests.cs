using PureQL.CSharp.Model.Samples.Queries.Dashboards;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Dashboards;

public sealed record OrdersPerWeekdayQueryTests
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
                    "operator": "notEqual",
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
                      "value": "cancelled"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "weekday",
                      "expression": {
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
                            "operator": "modulo",
                            "type": {
                              "name": "integer"
                            },
                            "left": {
                              "operator": "dateDiffDays",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-05-27"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
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
                            "name": "string"
                          },
                          "value": "1 Monday"
                        },
                        "else": {
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
                              "operator": "modulo",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "operator": "dateDiffDays",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-05-27"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 7
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
                            "type": {
                              "name": "string"
                            },
                            "value": "2 Tuesday"
                          },
                          "else": {
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
                                "operator": "modulo",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "operator": "dateDiffDays",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_on",
                                    "type": {
                                      "name": "date"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "date"
                                    },
                                    "value": "2024-05-27"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 7
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 2
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "3 Wednesday"
                            },
                            "else": {
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
                                  "operator": "modulo",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "left": {
                                    "operator": "dateDiffDays",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_on",
                                      "type": {
                                        "name": "date"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "date"
                                      },
                                      "value": "2024-05-27"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 7
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 3
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "4 Thursday"
                              },
                              "else": {
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
                                    "operator": "modulo",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "left": {
                                      "operator": "dateDiffDays",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "left": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "placed_on",
                                        "type": {
                                          "name": "date"
                                        }
                                      },
                                      "right": {
                                        "type": {
                                          "name": "date"
                                        },
                                        "value": "2024-05-27"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 7
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 4
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "5 Friday"
                                },
                                "else": {
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
                                      "operator": "modulo",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "left": {
                                        "operator": "dateDiffDays",
                                        "type": {
                                          "name": "integer"
                                        },
                                        "left": {
                                          "source": "schema_with_foreign_keys.orders",
                                          "field": "placed_on",
                                          "type": {
                                            "name": "date"
                                          }
                                        },
                                        "right": {
                                          "type": {
                                            "name": "date"
                                          },
                                          "value": "2024-05-27"
                                        }
                                      },
                                      "right": {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 7
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 5
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "6 Saturday"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "7 Sunday"
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "weekday",
                      "expression": {
                        "key": "weekday",
                        "type": {
                          "name": "string"
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
                      "alias": "tenant_385_share_pct",
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
                                    }
                                  },
                                  {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
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
                        "key": "weekday",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrdersPerWeekdayQuery().Value).TextValue
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
                      "name": "weekday",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "tenant_385_share_pct",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1 Monday",
                      "1",
                      "100"
                    ],
                    [
                      "3 Wednesday",
                      "1",
                      "0"
                    ],
                    [
                      "4 Thursday",
                      "1",
                      "0"
                    ],
                    [
                      "6 Saturday",
                      "1",
                      "100"
                    ],
                    [
                      "7 Sunday",
                      "1",
                      "100"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrdersPerWeekdayQuery().Result).TextValue
        );
    }
}
