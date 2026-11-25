using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record TemporalMeansOverWindowsQueryTests
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
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "average_date_of_all",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "tenant_average_date",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "tenant_average_so_far",
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
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "tenant_median_start_so_far",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "time"
                        },
                        "selector": {
                          "operator": "asTime",
                          "type": {
                            "name": "time"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "tenant_median_of_last_two",
                      "expression": {
                        "operator": "median",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          ],
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
                          ],
                          "frame": {
                            "preceding": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "after_tenant_average",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "operator": "average",
                          "type": {
                            "name": "date"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            ]
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
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
                """
            ).TextValue,
            new QueryJson(new TemporalMeansOverWindowsQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "average_date_of_all",
                      "type": "date"
                    },
                    {
                      "name": "tenant_average_date",
                      "type": "date"
                    },
                    {
                      "name": "tenant_average_so_far",
                      "type": "datetime"
                    },
                    {
                      "name": "tenant_median_start_so_far",
                      "type": "time"
                    },
                    {
                      "name": "tenant_median_of_last_two",
                      "type": "date"
                    },
                    {
                      "name": "after_tenant_average",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-02",
                      "2024-06-01T10:00:00Z",
                      "10:00:00",
                      "2024-06-01",
                      "False"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-02",
                      "2024-06-01T22:30:00Z",
                      "10:30:00",
                      "2024-06-02",
                      "False"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-02",
                      "2024-06-02T11:00:00Z",
                      "11:00:00",
                      "2024-06-03",
                      "True"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-05",
                      "2024-06-04T13:00:00Z",
                      "13:00:00",
                      "2024-06-04",
                      "False"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-05",
                      "2024-06-05T01:30:00Z",
                      "13:30:00",
                      "2024-06-05",
                      "False"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-04",
                      "2024-06-05",
                      "2024-06-05T14:00:00Z",
                      "14:00:00",
                      "2024-06-06",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TemporalMeansOverWindowsQuery().Result).TextValue
        );
    }
}
