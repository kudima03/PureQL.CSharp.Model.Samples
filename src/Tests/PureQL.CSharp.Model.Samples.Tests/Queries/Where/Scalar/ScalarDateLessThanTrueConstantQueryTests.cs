using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarDateLessThanTrueConstantQueryTests
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
                    "operator": "lessThan",
                    "left": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-01-01"
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-01-02"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarDateLessThanTrueConstantQuery().Value).TextValue
        );
    }
}
