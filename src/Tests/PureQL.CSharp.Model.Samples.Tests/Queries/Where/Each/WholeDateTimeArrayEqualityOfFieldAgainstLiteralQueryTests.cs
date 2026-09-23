using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeDateTimeArrayEqualityOfFieldAgainstLiteralQueryTests
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
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_at",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "datetimeArray"
                      },
                      "value": [
                        "2024-06-06T15:00:00",
                        "2024-06-05T14:00:00",
                        "2024-06-04T13:00:00",
                        "2024-06-03T12:00:00",
                        "2024-06-02T11:00:00",
                        "2024-06-01T10:00:00"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeDateTimeArrayEqualityOfFieldAgainstLiteralQuery().Value
            ).TextValue
        );
    }
}
