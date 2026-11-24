using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record ActivityTimelineQueryTests
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
                      "name": "events",
                      "query": {
                        "unionAll": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "at",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                }
                              },
                              {
                                "alias": "kind",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "placed"
                                }
                              },
                              {
                                "alias": "amount",
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "audit.logins"
                            },
                            "select": [
                              {
                                "alias": "at",
                                "expression": {
                                  "source": "audit.logins",
                                  "field": "login_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                }
                              },
                              {
                                "alias": "kind",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "logged in"
                                }
                              },
                              {
                                "alias": "amount",
                                "expression": {
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "value": null
                                }
                              }
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.users"
                            },
                            "select": [
                              {
                                "alias": "at",
                                "expression": {
                                  "operator": "asDatetime",
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "signup_date",
                                    "type": {
                                      "name": "date"
                                    }
                                  }
                                }
                              },
                              {
                                "alias": "kind",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "signed up"
                                }
                              },
                              {
                                "alias": "amount",
                                "expression": {
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  },
                                  "value": null
                                }
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "events"
                  },
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "events",
                      "field": "at",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2022-01-01T00:00:00Z"
                    }
                  },
                  "select": [
                    {
                      "alias": "at",
                      "expression": {
                        "source": "events",
                        "field": "at",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "kind",
                      "expression": {
                        "source": "events",
                        "field": "kind",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "amount",
                      "expression": {
                        "source": "events",
                        "field": "amount",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "events",
                        "field": "at",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "events",
                        "field": "kind",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "take": 11
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ActivityTimelineQuery().Value).TextValue
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
                      "name": "at",
                      "type": "datetime"
                    },
                    {
                      "name": "kind",
                      "type": "string"
                    },
                    {
                      "name": "amount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-06T15:00:00Z",
                      "placed",
                      "100.5"
                    ],
                    [
                      "2024-06-05T14:00:00Z",
                      "placed",
                      "300"
                    ],
                    [
                      "2024-06-04T13:00:00Z",
                      "placed",
                      "75.25"
                    ],
                    [
                      "2024-06-04T06:45:00Z",
                      "logged in",
                      ""
                    ],
                    [
                      "2024-06-03T12:00:00Z",
                      "placed",
                      "200"
                    ],
                    [
                      "2024-06-03T08:00:00Z",
                      "logged in",
                      ""
                    ],
                    [
                      "2024-06-02T11:00:00Z",
                      "placed",
                      "50"
                    ],
                    [
                      "2024-06-02T07:30:00Z",
                      "logged in",
                      ""
                    ],
                    [
                      "2024-06-01T10:00:00Z",
                      "placed",
                      "100.5"
                    ],
                    [
                      "2024-06-01T07:00:00Z",
                      "logged in",
                      ""
                    ],
                    [
                      "2023-02-28T00:00:00Z",
                      "signed up",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ActivityTimelineQuery().Result).TextValue
        );
    }
}
