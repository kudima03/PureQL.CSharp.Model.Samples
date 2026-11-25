using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record InclusiveComparisonsInATotalQueryTests
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
                  "aggregate": [
                    {
                      "alias": "at_or_after_from",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
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
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "after_from",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "greaterThan",
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
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "equal_to_from",
                      "expression": {
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
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "all_at_or_before_from",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
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
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "latest_is_at_or_after_latest_from",
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
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "any_coupon_at_or_after_tenant",
                      "expression": {
                        "operator": "any",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "if",
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            },
                            "then": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "else": {
                              "type": {
                                "name": "uuid",
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
                              "name": "uuid",
                              "nullable": true
                            },
                            "then": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "else": {
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
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
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InclusiveComparisonsInATotalQuery().Value).TextValue
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
                      "name": "at_or_after_from",
                      "type": "long"
                    },
                    {
                      "name": "after_from",
                      "type": "long"
                    },
                    {
                      "name": "equal_to_from",
                      "type": "long"
                    },
                    {
                      "name": "all_at_or_before_from",
                      "type": "bool"
                    },
                    {
                      "name": "latest_is_at_or_after_latest_from",
                      "type": "bool"
                    },
                    {
                      "name": "any_coupon_at_or_after_tenant",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "4",
                      "1",
                      "3",
                      "False",
                      "True",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InclusiveComparisonsInATotalQuery().Result).TextValue
        );
    }
}
