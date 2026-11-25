using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupByMonthQueryTests
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
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.users",
                      "field": "signup_date",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "operator": "startOfYear",
                      "type": {
                        "name": "date"
                      },
                      "operand": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2020-06-15"
                      }
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "signup_month",
                      "expression": {
                        "operator": "startOfMonth",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "signup_month",
                      "expression": {
                        "key": "signup_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "signups",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
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
                      "alias": "last_login_hour",
                      "expression": {
                        "operator": "startOfHour",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "operator": "max",
                          "type": {
                            "name": "datetime"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "signup_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupByMonthQuery().Value).TextValue
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
                      "name": "signup_month",
                      "type": "date"
                    },
                    {
                      "name": "signups",
                      "type": "long"
                    },
                    {
                      "name": "total_age",
                      "type": "double"
                    },
                    {
                      "name": "last_login_hour",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2020-01-01",
                      "2",
                      "58",
                      "2024-06-01T08:00:00Z"
                    ],
                    [
                      "2021-03-01",
                      "1",
                      "25",
                      "2024-06-02T09:00:00Z"
                    ],
                    [
                      "2022-11-01",
                      "1",
                      "42",
                      "2024-06-03T18:00:00Z"
                    ],
                    [
                      "2023-02-01",
                      "1",
                      "25",
                      "2024-06-04T07:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupByMonthQuery().Result).TextValue
        );
    }
}
