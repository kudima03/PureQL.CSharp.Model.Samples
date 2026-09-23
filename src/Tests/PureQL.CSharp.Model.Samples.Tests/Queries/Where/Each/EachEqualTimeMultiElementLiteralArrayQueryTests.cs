using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachEqualTimeMultiElementLiteralArrayQueryTests
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
                        "09:00:00",
                        "11:30:00",
                        "08:00:00"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachEqualTimeMultiElementLiteralArrayQuery().Value
            ).TextValue
        );
    }
}
