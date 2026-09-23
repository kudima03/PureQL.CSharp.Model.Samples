using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeDateArrayEqualityOfTwoReorderedLiteralArraysQueryTests
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
                        "2024-01-01",
                        "2024-02-01",
                        "2024-03-01"
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "dateArray"
                      },
                      "value": [
                        "2024-03-01",
                        "2024-02-01",
                        "2024-01-01"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeDateArrayEqualityOfTwoReorderedLiteralArraysQuery().Value
            ).TextValue
        );
    }
}
