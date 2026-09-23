using PureQL.CSharp.Model.Samples.Queries.Errors;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Errors;

public sealed record WhereEachFieldUnknownEntityQueryTests
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
                    "operator": "eachEqual",
                    "left": {
                      "entity": "shop.nonexistent_entity",
                      "field": "whatever",
                      "type": {
                        "name": "string"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": "shipped"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new WhereEachFieldUnknownEntityQuery().Value).TextValue
        );
    }
}
