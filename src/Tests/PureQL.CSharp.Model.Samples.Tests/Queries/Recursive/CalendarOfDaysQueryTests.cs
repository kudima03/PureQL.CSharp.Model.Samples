using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record CalendarOfDaysQueryTests
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
                      "name": "first_day",
                      "rows": [
                        [
                          {
                            "alias": "day",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-05-31"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "days",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "first_day"
                          },
                          "select": [
                            {
                              "alias": "day",
                              "expression": {
                                "source": "first_day",
                                "field": "day",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "days"
                          },
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "days",
                              "field": "day",
                              "type": {
                                "name": "date"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-07"
                            }
                          },
                          "select": [
                            {
                              "alias": "day",
                              "expression": {
                                "operator": "dateAddDays",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "days",
                                  "field": "day",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "days": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 1
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 366
                      }
                    },
                    {
                      "name": "daily",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "day",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "day",
                            "expression": {
                              "key": "day",
                              "type": {
                                "name": "date"
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
                            "alias": "revenue",
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
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "days"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "daily",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "days",
                          "field": "day",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "source": "daily",
                          "field": "day",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "day",
                      "expression": {
                        "source": "days",
                        "field": "day",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "source": "daily",
                            "field": "orders",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "daily",
                            "field": "revenue",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "days",
                        "field": "day",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CalendarOfDaysQuery().Value).TextValue
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
                      "name": "day",
                      "type": "date"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-05-31",
                      "0",
                      "0"
                    ],
                    [
                      "2024-06-01",
                      "1",
                      "100.5"
                    ],
                    [
                      "2024-06-02",
                      "1",
                      "50"
                    ],
                    [
                      "2024-06-03",
                      "1",
                      "200"
                    ],
                    [
                      "2024-06-04",
                      "1",
                      "75.25"
                    ],
                    [
                      "2024-06-05",
                      "1",
                      "300"
                    ],
                    [
                      "2024-06-06",
                      "1",
                      "100.5"
                    ],
                    [
                      "2024-06-07",
                      "0",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CalendarOfDaysQuery().Result).TextValue
        );
    }
}
