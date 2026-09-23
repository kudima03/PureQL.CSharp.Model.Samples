using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeBooleanArrayEqualityOfTwoEqualLiteralArraysQueryTests
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
                        "name": "booleanArray"
                      },
                      "value": [
                        true,
                        false,
                        true
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "booleanArray"
                      },
                      "value": [
                        true,
                        false,
                        true
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeBooleanArrayEqualityOfTwoEqualLiteralArraysQuery().Value
            ).TextValue
        );
    }
}
