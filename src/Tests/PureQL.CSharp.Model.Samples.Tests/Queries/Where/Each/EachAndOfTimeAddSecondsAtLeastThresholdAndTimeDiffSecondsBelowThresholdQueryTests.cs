using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachAndOfTimeAddSecondsAtLeastThresholdAndTimeDiffSecondsBelowThresholdQueryTests
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
                    "operator": "eachAnd",
                    "conditions": [
                      {
                        "operator": "eachGreaterThanOrEqual",
                        "left": {
                          "operator": "eachTimeAddSeconds",
                          "left": {
                            "entity": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "number"
                            },
                            "value": 1800
                          }
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "09:30:00"
                        }
                      },
                      {
                        "operator": "eachLessThan",
                        "left": {
                          "operator": "eachTimeDiffSeconds",
                          "left": {
                            "entity": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "time"
                            },
                            "value": "08:00:00"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 7200
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachAndOfTimeAddSecondsAtLeastThresholdAndTimeDiffSecondsBelowThresholdQuery().Value
            ).TextValue
        );
    }
}
