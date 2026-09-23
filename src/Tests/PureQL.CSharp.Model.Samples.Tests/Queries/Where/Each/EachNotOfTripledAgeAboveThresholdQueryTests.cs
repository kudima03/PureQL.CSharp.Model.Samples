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
}
