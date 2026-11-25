using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record InclusiveComparisonsInJoinsAndGroupsQueryTests
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
                      "kind": "inner",
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
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
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
                              }
                            },
                            "right": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "else": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-03"
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "groupBy": [
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
                      "alias": "trial_start",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "then": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "else": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-03"
                        },
                        "condition": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "max",
                      "type": {
                        "name": "date",
                        "nullable": true
                      },
                      "selector": {
                        "operator": "if",
                        "type": {
                          "name": "date",
                          "nullable": true
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
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date",
                        "nullable": true
                      },
                      "value": null
                    }
                  },
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
                      "alias": "shipped_on_trial_start",
                      "expression": {
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
                              "operator": "lessThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
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
                                }
                              },
                              "right": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "then": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "else": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-03"
                                },
                                "condition": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_active",
                                  "type": {
                                    "name": "boolean"
                                  }
                                }
                              }
                            },
                            {
                              "operator": "greaterThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
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
                                }
                              },
                              "right": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "then": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "else": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-03"
                                },
                                "condition": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_active",
                                  "type": {
                                    "name": "boolean"
                                  }
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "last_shipment_at_or_after_trial",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "max",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "if",
                            "type": {
                              "name": "date",
                              "nullable": true
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
                            }
                          }
                        },
                        "right": {
                          "key": "trial_start",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InclusiveComparisonsInJoinsAndGroupsQuery().Value).TextValue
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
                      "name": "shipped_on_trial_start",
                      "type": "long"
                    },
                    {
                      "name": "last_shipment_at_or_after_trial",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "True"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "True"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new InclusiveComparisonsInJoinsAndGroupsQuery().Result
            ).TextValue
        );
    }
}
