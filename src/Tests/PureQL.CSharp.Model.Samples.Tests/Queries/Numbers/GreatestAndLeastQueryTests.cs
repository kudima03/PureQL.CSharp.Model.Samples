using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record GreatestAndLeastQueryTests
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
                      "kind": "left",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "greatest",
                      "type": {
                        "name": "datetime"
                      },
                      "values": [
                        {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        {
                          "operator": "coalesce",
                          "type": {
                            "name": "datetime"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          ]
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-06-06T00:00:00Z"
                    }
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
                      "alias": "billable",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 60
                          }
                        ]
                      }
                    },
                    {
                      "alias": "covered_by_credit",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "multiply",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "alias": "clamped_total",
                      "expression": {
                        "operator": "least",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "greatest",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 60
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 150
                          }
                        ]
                      }
                    },
                    {
                      "alias": "last_activity",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime",
                              "nullable": true
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "last_activity_or_order",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "datetime"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          {
                            "operator": "coalesce",
                            "type": {
                              "name": "datetime"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "last_login",
                                "type": {
                                  "name": "datetime",
                                  "nullable": true
                                }
                              },
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "alias": "days_since_signup_at_least_one",
                      "expression": {
                        "operator": "greatest",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "dateDiffDays",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
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
            new QueryJson(new GreatestAndLeastQuery().Value).TextValue
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
                      "name": "billable",
                      "type": "double"
                    },
                    {
                      "name": "covered_by_credit",
                      "type": "double"
                    },
                    {
                      "name": "clamped_total",
                      "type": "double"
                    },
                    {
                      "name": "last_activity",
                      "type": "datetime"
                    },
                    {
                      "name": "last_activity_or_order",
                      "type": "datetime"
                    },
                    {
                      "name": "days_since_signup_at_least_one",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "100.5",
                      "90",
                      "100.5",
                      "2024-06-01T10:00:00Z",
                      "2024-06-01T10:00:00Z",
                      "1599"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "60",
                      "50",
                      "60",
                      "2024-06-02T11:00:00Z",
                      "2024-06-02T11:00:00Z",
                      "1600"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      "",
                      "150",
                      "",
                      "2024-06-03T12:00:00Z",
                      ""
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "75.25",
                      "75.25",
                      "75.25",
                      "2024-06-04T13:00:00Z",
                      "2024-06-04T13:00:00Z",
                      "1791"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      "90",
                      "150",
                      "2024-06-05T14:00:00Z",
                      "2024-06-05T14:00:00Z",
                      "1792"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GreatestAndLeastQuery().Result).TextValue
        );
    }
}
