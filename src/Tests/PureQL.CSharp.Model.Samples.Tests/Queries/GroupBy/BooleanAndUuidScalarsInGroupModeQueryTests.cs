using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record BooleanAndUuidScalarsInGroupModeQueryTests
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
                      "type": {
                        "name": "boolean"
                      },
                      "value": true,
                      "alias": "flag"
                    },
                    {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "9b2b1f6e-3c86-4c50-8f6a-2f6d1a8f2c11",
                      "alias": "marker"
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "order_count"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new BooleanAndUuidScalarsInGroupModeQuery().Value).TextValue
        );
    }
}
