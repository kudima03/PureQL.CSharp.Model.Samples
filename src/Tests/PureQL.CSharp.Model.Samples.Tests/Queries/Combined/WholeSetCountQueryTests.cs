using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record WholeSetCountQueryTests
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
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "total"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WholeSetCountQuery().Value).TextValue
        );
    }
}
