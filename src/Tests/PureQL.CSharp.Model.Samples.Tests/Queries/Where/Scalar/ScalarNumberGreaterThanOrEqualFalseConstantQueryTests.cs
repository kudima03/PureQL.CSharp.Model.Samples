using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarNumberGreaterThanOrEqualFalseConstantQueryTests
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
                    "operator": "greaterThanOrEqual",
                    "left": {
                      "type": {
                        "name": "number"
                      },
                      "value": 1
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
            new QueryJson(
                new ScalarNumberGreaterThanOrEqualFalseConstantQuery().Value
            ).TextValue
        );
    }
}
