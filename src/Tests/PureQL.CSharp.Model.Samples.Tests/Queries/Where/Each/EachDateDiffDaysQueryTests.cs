using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachDateDiffDaysQueryTests
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
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachGreaterThan",
                    "left": {
                      "operator": "eachDateDiffDays",
                      "left": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-06-01"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 2
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachDateDiffDaysQuery().Value).TextValue
        );
    }
}
