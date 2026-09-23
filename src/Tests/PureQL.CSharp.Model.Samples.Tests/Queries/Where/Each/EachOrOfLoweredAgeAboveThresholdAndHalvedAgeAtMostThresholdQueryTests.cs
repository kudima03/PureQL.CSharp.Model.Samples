using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachOrOfLoweredAgeAboveThresholdAndHalvedAgeAtMostThresholdQueryTests
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
                        "operator": "eachGreaterThan",
                        "left": {
                          "operator": "eachSubtract",
                          "values": [
                            {
                              "entity": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "number"
                              }
                            },
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 10
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 20
                        }
                      },
                      {
                        "operator": "eachLessThanOrEqual",
                        "left": {
                          "operator": "eachDivide",
                          "values": [
                            {
                              "entity": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "number"
                              }
                            },
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 2
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 13
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachOrOfLoweredAgeAboveThresholdAndHalvedAgeAtMostThresholdQuery().Value
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
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new EachOrOfLoweredAgeAboveThresholdAndHalvedAgeAtMostThresholdQuery().Result
            ).TextValue
        );
    }
}
