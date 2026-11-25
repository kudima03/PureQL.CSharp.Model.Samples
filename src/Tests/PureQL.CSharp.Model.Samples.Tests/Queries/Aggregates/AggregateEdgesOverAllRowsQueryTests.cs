using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AggregateEdgesOverAllRowsQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "subqueries": [
                    {
                      "name": "facts",
                      "rows": [
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000065-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 10
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000066-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000067-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000068-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000069-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 20
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000006a-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "facts",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "facts",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "count_all",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "count_paid",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "source": "facts",
                          "field": "is_paid",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "points",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "facts",
                          "field": "bonus_points",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "rows",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "mean_time",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "datetime"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "all_paid",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "source": "facts",
                          "field": "is_paid",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "total_if_any",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
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
                            "value": 0
                          }
                        },
                        "then": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "else": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AggregateEdgesOverAllRowsQuery().Value).TextValue
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
                      "name": "count_all",
                      "type": "long"
                    },
                    {
                      "name": "count_paid",
                      "type": "long"
                    },
                    {
                      "name": "points",
                      "type": "long"
                    },
                    {
                      "name": "rows",
                      "type": "long"
                    },
                    {
                      "name": "mean_time",
                      "type": "datetime"
                    },
                    {
                      "name": "all_paid",
                      "type": "bool"
                    },
                    {
                      "name": "total_if_any",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6",
                      "4",
                      "35",
                      "6",
                      "2024-06-04T00:30:00Z",
                      "False",
                      "100.5"
                    ],
                    [
                      "6",
                      "4",
                      "35",
                      "6",
                      "2024-06-04T00:30:00Z",
                      "False",
                      "50"
                    ],
                    [
                      "6",
                      "4",
                      "35",
                      "6",
                      "2024-06-04T00:30:00Z",
                      "False",
                      "200"
                    ],
                    [
                      "6",
                      "4",
                      "35",
                      "6",
                      "2024-06-04T00:30:00Z",
                      "False",
                      "75.25"
                    ],
                    [
                      "6",
                      "4",
                      "35",
                      "6",
                      "2024-06-04T00:30:00Z",
                      "False",
                      "300"
                    ],
                    [
                      "6",
                      "4",
                      "35",
                      "6",
                      "2024-06-04T00:30:00Z",
                      "False",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AggregateEdgesOverAllRowsQuery().Result).TextValue
        );
    }
}
