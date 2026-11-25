using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalDayBucketsQueryTests
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
                      "name": "local_days",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.users"
                        },
                        "groupBy": [
                          {
                            "alias": "day_start",
                            "expression": {
                              "operator": "startOfDay",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "last_login",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "day_start",
                            "expression": {
                              "key": "day_start",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "alias": "total_age",
                            "expression": {
                              "operator": "sum",
                              "type": {
                                "name": "decimal"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "alias": "users",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "local_days"
                  },
                  "select": [
                    {
                      "alias": "day_start",
                      "expression": {
                        "source": "local_days",
                        "field": "day_start",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "local_day",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "local_days",
                          "field": "day_start",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Asia/Tokyo"
                        }
                      }
                    },
                    {
                      "alias": "total_age",
                      "expression": {
                        "source": "local_days",
                        "field": "total_age",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "total_age_of_the_local_month",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "local_days",
                          "field": "total_age",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "startOfMonth",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "local_days",
                                "field": "day_start",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "running_in_the_local_week",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "local_days",
                          "field": "total_age",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "startOfWeek",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "local_days",
                                "field": "day_start",
                                "type": {
                                  "name": "datetime"
                                }
                              },
                              "zone": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "local_days",
                                "field": "day_start",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "previous_day_start",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "operand": {
                          "source": "local_days",
                          "field": "day_start",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "local_days",
                                "field": "day_start",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "day_start"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalDayBucketsQuery().Value).TextValue
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
                      "name": "day_start",
                      "type": "datetime"
                    },
                    {
                      "name": "local_day",
                      "type": "date"
                    },
                    {
                      "name": "total_age",
                      "type": "double"
                    },
                    {
                      "name": "total_age_of_the_local_month",
                      "type": "double"
                    },
                    {
                      "name": "running_in_the_local_week",
                      "type": "double"
                    },
                    {
                      "name": "previous_day_start",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-05-29T15:00:00Z",
                      "2024-05-30",
                      "30",
                      "30",
                      "30",
                      ""
                    ],
                    [
                      "2024-05-31T15:00:00Z",
                      "2024-06-01",
                      "58",
                      "150",
                      "88",
                      "2024-05-29T15:00:00Z"
                    ],
                    [
                      "2024-06-01T15:00:00Z",
                      "2024-06-02",
                      "25",
                      "150",
                      "113",
                      "2024-05-31T15:00:00Z"
                    ],
                    [
                      "2024-06-03T15:00:00Z",
                      "2024-06-04",
                      "67",
                      "150",
                      "67",
                      "2024-06-01T15:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalDayBucketsQuery().Result).TextValue
        );
    }
}
