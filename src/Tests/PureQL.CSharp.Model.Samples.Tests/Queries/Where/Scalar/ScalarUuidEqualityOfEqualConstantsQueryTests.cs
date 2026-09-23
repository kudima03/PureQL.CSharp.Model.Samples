using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarUuidEqualityOfEqualConstantsQueryTests
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
                        "name": "uuid"
                      },
                      "value": "00000001-0000-0000-0000-000000000000"
                    },
                    "right": {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "00000001-0000-0000-0000-000000000000"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarUuidEqualityOfEqualConstantsQuery().Value).TextValue
        );
    }
}
