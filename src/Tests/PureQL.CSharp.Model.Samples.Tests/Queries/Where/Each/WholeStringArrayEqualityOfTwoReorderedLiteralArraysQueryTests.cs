using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeStringArrayEqualityOfTwoReorderedLiteralArraysQueryTests
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
                        "name": "stringArray"
                      },
                      "value": [
                        "alpha",
                        "beta",
                        "gamma"
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "stringArray"
                      },
                      "value": [
                        "gamma",
                        "beta",
                        "alpha"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeStringArrayEqualityOfTwoReorderedLiteralArraysQuery().Value
            ).TextValue
        );
    }
}
