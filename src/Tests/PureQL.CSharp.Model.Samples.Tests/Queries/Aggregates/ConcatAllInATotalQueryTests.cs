using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record ConcatAllInATotalQueryTests
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
                  "aggregate": [
                    {
                      "alias": "statuses",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "order_dates",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": "; "
                        },
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
                    {
                      "alias": "paid_totals",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " + "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "direction": "desc"
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
                        ],
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
                        }
                      }
                    },
                    {
                      "alias": "has_statuses",
                      "expression": {
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "concatAll",
                          "type": {
                            "name": "string"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "separator": {
                            "type": {
                              "name": "string"
                            },
                            "value": ", "
                          },
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ],
                          "distinct": true
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    },
                    {
                      "alias": "statuses_or_placeholder",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "left": {
                              "operator": "concatAll",
                              "type": {
                                "name": "string"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "separator": {
                                "type": {
                                  "name": "string"
                                },
                                "value": ", "
                              },
                              "orderBy": [
                                {
                                  "expression": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_status",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                }
                              ],
                              "distinct": true
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": ""
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "no orders"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "number_of_characters",
                      "expression": {
                        "operator": "length",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "concatAll",
                          "type": {
                            "name": "string"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "separator": {
                            "type": {
                              "name": "string"
                            },
                            "value": ", "
                          },
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ],
                          "distinct": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConcatAllInATotalQuery().Value).TextValue
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
                      "name": "statuses",
                      "type": "string"
                    },
                    {
                      "name": "order_dates",
                      "type": "string"
                    },
                    {
                      "name": "paid_totals",
                      "type": "string"
                    },
                    {
                      "name": "has_statuses",
                      "type": "bool"
                    },
                    {
                      "name": "statuses_or_placeholder",
                      "type": "string"
                    },
                    {
                      "name": "number_of_characters",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "pending, shipped",
                      "2024-06-01; 2024-06-02; 2024-06-03",
                      "200 + 100.5",
                      "True",
                      "pending, shipped",
                      "16"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConcatAllInATotalQuery().Result).TextValue
        );
    }
}
