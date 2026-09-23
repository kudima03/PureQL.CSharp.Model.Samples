using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachEqualDateMultiElementLiteralArrayQueryTests
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
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "dateArray"
                      },
                      "value": [
                        "2024-06-01",
                        "2024-06-02",
                        "2024-06-03"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachEqualDateMultiElementLiteralArrayQuery().Value
            ).TextValue
        );
    }
}
