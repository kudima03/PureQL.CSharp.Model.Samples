using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarTimeLessThanOrEqualTrueConstantQueryTests
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
                    "operator": "lessThanOrEqual",
                    "left": {
                      "type": {
                        "name": "time"
                      },
                      "value": "09:00:00"
                    },
                    "right": {
                      "type": {
                        "name": "time"
                      },
                      "value": "09:00:00"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new ScalarTimeLessThanOrEqualTrueConstantQuery().Value
            ).TextValue
        );
    }
}
