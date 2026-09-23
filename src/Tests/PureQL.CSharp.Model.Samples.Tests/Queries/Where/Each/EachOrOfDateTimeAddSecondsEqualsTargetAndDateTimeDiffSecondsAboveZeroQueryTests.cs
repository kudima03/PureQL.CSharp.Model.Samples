using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachOrOfDateTimeAddSecondsEqualsTargetAndDateTimeDiffSecondsAboveZeroQueryTests
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
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachOr",
                    "conditions": [
                      {
                        "operator": "eachEqual",
                        "left": {
                          "operator": "eachDatetimeAddSeconds",
                          "left": {
                            "entity": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "number"
                            },
                            "value": 3600
                          }
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-01T09:30:00"
                        }
                      },
                      {
                        "operator": "eachGreaterThan",
                        "left": {
                          "operator": "eachDatetimeDiffSeconds",
                          "left": {
                            "entity": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-02T00:00:00"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 0
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachOrOfDateTimeAddSecondsEqualsTargetAndDateTimeDiffSecondsAboveZeroQuery().Value
            ).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new EachOrOfDateTimeAddSecondsEqualsTargetAndDateTimeDiffSecondsAboveZeroQuery().Result
            ).TextValue
        );
    }
}
