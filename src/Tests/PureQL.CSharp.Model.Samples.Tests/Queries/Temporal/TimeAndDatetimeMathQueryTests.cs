using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record TimeAndDatetimeMathQueryTests
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
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-01T00:00:00Z"
                        }
                      },
                      {
                        "operator": "lessThan",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "10:00:00"
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "shift_end",
                      "type": {
                        "name": "time"
                      },
                      "expression": {
                        "operator": "timeAddSeconds",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "right": {
                          "operator": "multiply",
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 16
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3600
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "seconds_since_june",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "datetimeDiffSeconds",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-01T00:00:00Z"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TimeAndDatetimeMathQuery().Value).TextValue
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
                      "name": "shift_end",
                      "type": "time"
                    },
                    {
                      "name": "seconds_since_june",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "01:00:00",
                      "30600"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "00:00:00",
                      "284700"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "01:00:00",
                      "30600"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TimeAndDatetimeMathQuery().Result).TextValue
        );
    }
}
