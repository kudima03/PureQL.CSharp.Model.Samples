using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record DateMathEverywhereQueryTests
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
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "dateDiffDays",
                      "type": {
                        "name": "integer"
                      },
                      "left": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-06-01"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 4
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "order_user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "max",
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
                    },
                    "right": {
                      "operator": "dateAddDays",
                      "type": {
                        "name": "date"
                      },
                      "left": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-06-30"
                      },
                      "right": {
                        "type": {
                          "name": "integer"
                        },
                        "value": -27
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "order_user_id",
                      "expression": {
                        "key": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "active_span_days",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "max",
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
                        },
                        "right": {
                          "operator": "min",
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
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DateMathEverywhereQuery().Value).TextValue
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
                      "name": "active_span_days",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "0"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DateMathEverywhereQuery().Result).TextValue
        );
    }
}
