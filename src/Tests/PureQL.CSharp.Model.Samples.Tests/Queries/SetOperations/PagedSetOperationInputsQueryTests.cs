using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record PagedSetOperationInputsQueryTests
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
                                  "value": "ordered"
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "column": "at",
                                "direction": "desc"
                              }
                            ],
                            "pagination": {
                              "take": 5
                            }
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "select": [
                              {
                                "alias": "at",
                                "expression": {
                                  "operator": "datetimeAddSeconds",
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "placed_at",
                                    "type": {
                                      "name": "datetime"
                                    }
                                  },
                                  "seconds": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 5430
                                  }
                                }
                              },
                              {
                                "alias": "kind",
                                "expression": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "column": "at",
                                "direction": "desc"
                              }
                            ],
                            "pagination": {
                              "take": 5
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "events"
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
                      "alias": "minute",
                      "expression": {
                        "operator": "startOfMinute",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "events",
                          "field": "at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "minute",
                      "direction": "desc"
                    },
                    {
                      "column": "kind"
                    },
                    {
                      "column": "at"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PagedSetOperationInputsQuery().Value).TextValue
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
                      "name": "minute",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-06T16:30:30Z",
                      "shipped",
                      "2024-06-06T16:30:00Z"
                    ],
                    [
                      "2024-06-06T15:00:00Z",
                      "ordered",
                      "2024-06-06T15:00:00Z"
                    ],
                    [
                      "2024-06-05T15:30:30Z",
                      "shipped",
                      "2024-06-05T15:30:00Z"
                    ],
                    [
                      "2024-06-05T14:00:00Z",
                      "ordered",
                      "2024-06-05T14:00:00Z"
                    ],
                    [
                      "2024-06-04T14:30:30Z",
                      "shipped",
                      "2024-06-04T14:30:00Z"
                    ],
                    [
                      "2024-06-04T13:00:00Z",
                      "ordered",
                      "2024-06-04T13:00:00Z"
                    ],
                    [
                      "2024-06-03T13:30:30Z",
                      "shipped",
                      "2024-06-03T13:30:00Z"
                    ],
                    [
                      "2024-06-03T12:00:00Z",
                      "ordered",
                      "2024-06-03T12:00:00Z"
                    ],
                    [
                      "2024-06-02T12:30:30Z",
                      "shipped",
                      "2024-06-02T12:30:00Z"
                    ],
                    [
                      "2024-06-02T11:00:00Z",
                      "ordered",
                      "2024-06-02T11:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PagedSetOperationInputsQuery().Result).TextValue
        );
    }
}
