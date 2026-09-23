using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeDateArrayEqualityOfLiteralAgainstFieldQueryTests
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
                        "name": "dateArray"
                      },
                      "value": [
                        "2024-06-06",
                        "2024-06-05",
                        "2024-06-04",
                        "2024-06-03",
                        "2024-06-02",
                        "2024-06-01"
                      ]
                    },
                    "right": {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeDateArrayEqualityOfLiteralAgainstFieldQuery().Value
            ).TextValue
        );
    }
}
