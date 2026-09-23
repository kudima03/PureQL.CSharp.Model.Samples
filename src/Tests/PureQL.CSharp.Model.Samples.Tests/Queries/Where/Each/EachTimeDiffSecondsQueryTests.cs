using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachTimeDiffSecondsQueryTests
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
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachGreaterThan",
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
                      "value": 3600
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachTimeDiffSecondsQuery().Value).TextValue
        );
    }
}
