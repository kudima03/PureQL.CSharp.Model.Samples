using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record SumOfEachTimeDiffSecondsWholeSetQueryTests
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
                      "operator": "sum",
                      "arg": {
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
                      "alias": "totalGapSeconds"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SumOfEachTimeDiffSecondsWholeSetQuery().Value).TextValue
        );
    }
}
