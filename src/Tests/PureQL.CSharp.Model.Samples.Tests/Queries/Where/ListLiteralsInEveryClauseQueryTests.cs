using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record ListLiteralsInEveryClauseQueryTests
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
                            "operator": "in",
                            "type": {
                              "name": "boolean"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "list": {
                              "type": {
                                "name": "stringList"
                              },
                              "value": [
                                "Ann",
                                "Cara",
                                "Dan"
                              ]
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "in",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    },
                    "list": {
                      "type": {
                        "name": "stringList"
                      },
                      "value": [
                        "shipped",
                        "pending"
                      ]
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "is_open",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "list": {
                          "type": {
                            "name": "stringList"
                          },
                          "value": [
                            "pending",
                            "cancelled"
                          ]
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "in",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "operator": "count",
                      "type": {
                        "name": "integer"
                      }
                    },
                    "list": {
                      "type": {
                        "name": "integerList"
                      },
                      "value": [
                        1,
                        2,
                        3
                      ]
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
                      "alias": "is_open",
                      "expression": {
                        "key": "is_open",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "large",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "in",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "list": {
                            "param": "round_totals",
                            "type": {
                              "name": "decimalList"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "count_is_listed",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "list": {
                          "param": "counts",
                          "type": {
                            "name": "integerList"
                          }
                        }
                      }
                    },
                    {
                      "alias": "tenant_is_listed",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "key": "tenant_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "list": {
                          "type": {
                            "name": "uuidList"
                          },
                          "value": [
                            "00000385-0000-0000-0000-000000000000"
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "key": "tenant_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "list": {
                          "type": {
                            "name": "uuidList"
                          },
                          "value": [
                            "00000385-0000-0000-0000-000000000000",
                            "00000386-0000-0000-0000-000000000000"
                          ]
                        }
                      }
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
                        "key": "is_open",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ListLiteralsInEveryClauseQuery().Value).TextValue
        );
    }
}
