using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record MonthsBetweenQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
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
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "integerDivide",
                      "type": {
                        "name": "integer"
                      },
                      "left": {
                        "operator": "dateDiffMonths",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-09-04"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      },
                      "right": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 12
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 2
                    }
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "whole_years_as_member",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "dateDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-09-04"
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        }
                      }
                    },
                    {
                      "alias": "months_since_shipped_order",
                      "expression": {
                        "operator": "dateDiffMonths",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-09-04"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "months_since_last_login",
                      "expression": {
                        "operator": "datetimeDiffMonths",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-09-01T08:30:00Z"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MonthsBetweenQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "whole_years_as_member",
                      "type": "long"
                    },
                    {
                      "name": "months_since_shipped_order",
                      "type": "long"
                    },
                    {
                      "name": "months_since_last_login",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "4",
                      "3",
                      "3"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "3",
                      "3",
                      "2"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "5",
                      "2",
                      "3"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "4",
                      "",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MonthsBetweenQuery().Result).TextValue
        );
    }
}
