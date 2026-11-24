using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullIfEqualQueryTests
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
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "divide",
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
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "left": {
                            "operator": "subtract",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 25
                              }
                            ]
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 50
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
                      "alias": "known_status",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "string",
                          "nullable": true
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
                          "value": "pending"
                        }
                      }
                    },
                    {
                      "alias": "years_past_25",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "left": {
                          "operator": "subtract",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 25
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "total_per_year_past_25",
                      "expression": {
                        "operator": "divide",
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
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "operator": "subtract",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_age",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 25
                                }
                              ]
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "total_per_year_past_25_or_zero",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "divide",
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
                                "operator": "nullIfEqual",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
                                },
                                "left": {
                                  "operator": "subtract",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_age",
                                      "type": {
                                        "name": "decimal"
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 25
                                    }
                                  ]
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 0
                                }
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "0"
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
            new QueryJson(new NullIfEqualQuery().Value).TextValue
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
                      "name": "known_status",
                      "type": "string"
                    },
                    {
                      "name": "years_past_25",
                      "type": "double"
                    },
                    {
                      "name": "total_per_year_past_25",
                      "type": "double"
                    },
                    {
                      "name": "total_per_year_past_25_or_zero",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "shipped",
                      "5",
                      "20.1",
                      "20.1"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "",
                      "5",
                      "10",
                      "10"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "cancelled",
                      "5",
                      "15.05",
                      "15.05"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "",
                      "17",
                      "5.911764705882353",
                      "5.911764705882353"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullIfEqualQuery().Result).TextValue
        );
    }
}
