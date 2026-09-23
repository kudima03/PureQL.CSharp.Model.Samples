using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarBooleanEqualityOfEqualConstantsQueryTests
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
                    "operator": "equal",
                    "left": {
                      "type": {
                        "name": "boolean"
                      },
                      "value": true
                    },
                    "right": {
                      "type": {
                        "name": "boolean"
                      },
                      "value": true
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new ScalarBooleanEqualityOfEqualConstantsQuery().Value
            ).TextValue
        );
    }
}
