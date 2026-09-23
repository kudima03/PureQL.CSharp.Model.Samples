using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingMaxShiftStartEqualExistingValueQueryTests
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
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ],
                  "having": {
                    "operator": "equal",
                    "left": {
                      "operator": "max_time",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "time"
                      },
                      "value": "11:30:00"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new HavingMaxShiftStartEqualExistingValueQuery().Value
            ).TextValue
        );
    }
}
