using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachNotOfTripledAgeAboveThresholdQueryTests
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
                    "operator": "eachNot",
                    "condition": {
                      "operator": "eachGreaterThan",
                      "left": {
                        "operator": "eachMultiply",
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
                            "value": 3
                          }
                        ]
                      },
                      "right": {
                        "type": {
                          "name": "number"
                        },
                        "value": 120
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachNotOfTripledAgeAboveThresholdQuery().Value).TextValue
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
                      "00000003-0000-0000-0000-000000000000"
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
            new DataSetJson(new EachNotOfTripledAgeAboveThresholdQuery().Result).TextValue
        );
    }
}
