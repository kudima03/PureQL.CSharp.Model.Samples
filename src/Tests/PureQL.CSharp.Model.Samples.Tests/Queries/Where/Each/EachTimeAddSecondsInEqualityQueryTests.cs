using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachTimeAddSecondsInEqualityQueryTests
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
                    "operator": "eachEqual",
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
                        "value": 3600
                      }
                    },
                    "right": {
                      "type": {
                        "name": "time"
                      },
                      "value": "10:00:00"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachTimeAddSecondsInEqualityQuery().Value).TextValue
        );
    }
}
