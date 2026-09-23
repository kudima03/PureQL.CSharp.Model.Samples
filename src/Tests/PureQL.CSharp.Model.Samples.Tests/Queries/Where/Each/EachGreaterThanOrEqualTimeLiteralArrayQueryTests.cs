using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachGreaterThanOrEqualTimeLiteralArrayQueryTests
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
                    "operator": "eachGreaterThanOrEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "shift_start",
                      "type": {
                        "name": "time"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "timeArray"
                      },
                      "value": [
                        "09:00:00"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachGreaterThanOrEqualTimeLiteralArrayQuery().Value
            ).TextValue
        );
    }
}
