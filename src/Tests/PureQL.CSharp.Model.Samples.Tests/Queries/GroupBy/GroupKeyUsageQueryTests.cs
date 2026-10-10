using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupKeyUsageQueryTests
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
                      "type": "left",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "and",
                        "conditions": [
                          {
                            "operator": "equal",
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
                  "groupBy": [
                    {
                      "alias": "order_user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "buyer",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "days_since_signup",
                      "type": {
                        "name": "integer",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "dateDiffDays",
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
                      }
                    }
                  ],
                  "having": {
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "notEqual",
                        "left": {
                          "key": 1,
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      },
                      {
                        "operator": "lessThanOrEqual",
                        "left": {
                          "key": 2,
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1600
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "buyer",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "operator": "coalesce",
                        "values": [
                          {
                            "key": 1,
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "unknown"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "hours_since_signup",
                      "type": {
                        "name": "integer",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "multiply",
                        "values": [
                          {
                            "key": 2,
                            "type": {
                              "name": "integer",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 24
                          }
                        ]
                      }
                    },
                    {
                      "alias": "orders",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "count"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": 2,
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "expression": {
                        "operator": "count"
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupKeyUsageQuery().Value).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "buyer",
                      "type": "string"
                    },
                    {
                      "name": "hours_since_signup",
                      "type": "long"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "13896",
                      "1"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "38376",
                      "1"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "38400",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupKeyUsageQuery().Result).TextValue
        );
    }
}
