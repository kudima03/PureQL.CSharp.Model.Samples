using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeArrayEqualityOfTwoEqualLiteralArraysQueryTests
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
                        "name": "numberArray"
                      },
                      "value": [
                        1,
                        2,
                        3
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "numberArray"
                      },
                      "value": [
                        1,
                        2,
                        3
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeArrayEqualityOfTwoEqualLiteralArraysQuery().Value
            ).TextValue
        );
    }
}
