using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeTimeArrayEqualityOfLiteralAgainstFieldQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "equal",
                    "left": {
                      "type": {
                        "name": "timeArray"
                      },
                      "value": [
                        "09:00:00",
                        "08:00:00",
                        "11:30:00",
                        "09:00:00",
                        "10:00:00",
                        "09:00:00"
                      ]
                    },
                    "right": {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "shift_start",
                      "type": {
                        "name": "time"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeTimeArrayEqualityOfLiteralAgainstFieldQuery().Value
            ).TextValue
        );
    }
}
