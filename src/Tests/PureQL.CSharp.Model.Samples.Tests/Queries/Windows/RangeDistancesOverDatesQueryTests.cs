using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record RangeDistancesOverDatesQueryTests
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
                      "alias": "date_key_days",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "days": 1
                            }
                          },
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
                      "alias": "date_key_following_days",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "following": {
                              "days": 2
                            }
                          },
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
                      "alias": "date_key_months",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "months": 11
                            }
                          },
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
                      "alias": "date_key_months_and_days",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "months": 1
                            },
                            "following": {
                              "days": 7
                            }
                          },
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
                      "alias": "date_key_all_before",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "nullable_date_key",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
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
                                    "value": "shipped"
                                  }
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
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "days": 3
                            }
                          },
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
                      "alias": "datetime_key_seconds",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "seconds": 90000
                            }
                          },
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
                      "alias": "datetime_key_fractional_seconds",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ],
                          "range": {
                            "following": {
                              "seconds": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "90000.5"
                              }
                            }
                          },
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
                      "alias": "datetime_key_seconds_beyond_a_day",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "seconds": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "172800.5"
                              }
                            }
                          },
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
                      "alias": "datetime_key_months",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "months": 1
                            }
                          },
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
                      "alias": "datetime_key_all_after",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          ],
                          "range": {
                            "following": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "month_key",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "startOfMonth",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              }
                            }
                          ],
                          "range": {
                            "preceding": {
                              "months": 2
                            },
                            "following": {
                              "months": 2
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
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
            new QueryJson(new RangeDistancesOverDatesQuery().Value).TextValue
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
                      "name": "date_key_days",
                      "type": "long"
                    },
                    {
                      "name": "date_key_following_days",
                      "type": "long"
                    },
                    {
                      "name": "date_key_months",
                      "type": "long"
                    },
                    {
                      "name": "date_key_months_and_days",
                      "type": "long"
                    },
                    {
                      "name": "date_key_all_before",
                      "type": "long"
                    },
                    {
                      "name": "nullable_date_key",
                      "type": "long"
                    },
                    {
                      "name": "datetime_key_seconds",
                      "type": "long"
                    },
                    {
                      "name": "datetime_key_fractional_seconds",
                      "type": "long"
                    },
                    {
                      "name": "datetime_key_seconds_beyond_a_day",
                      "type": "long"
                    },
                    {
                      "name": "datetime_key_months",
                      "type": "long"
                    },
                    {
                      "name": "datetime_key_all_after",
                      "type": "long"
                    },
                    {
                      "name": "month_key",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "1",
                      "3",
                      "1",
                      "3",
                      "1",
                      "1",
                      "1",
                      "2",
                      "1",
                      "1",
                      "6",
                      "6"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2",
                      "2",
                      "2",
                      "3",
                      "2",
                      "1",
                      "2",
                      "2",
                      "2",
                      "2",
                      "5",
                      "6"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2",
                      "1",
                      "3",
                      "3",
                      "3",
                      "2",
                      "2",
                      "1",
                      "2",
                      "3",
                      "4",
                      "6"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "1",
                      "3",
                      "1",
                      "3",
                      "4",
                      "2",
                      "1",
                      "2",
                      "1",
                      "1",
                      "3",
                      "6"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2",
                      "2",
                      "2",
                      "3",
                      "5",
                      "1",
                      "2",
                      "2",
                      "2",
                      "2",
                      "2",
                      "6"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2",
                      "1",
                      "3",
                      "3",
                      "6",
                      "2",
                      "2",
                      "1",
                      "2",
                      "3",
                      "1",
                      "6"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RangeDistancesOverDatesQuery().Result).TextValue
        );
    }
}
