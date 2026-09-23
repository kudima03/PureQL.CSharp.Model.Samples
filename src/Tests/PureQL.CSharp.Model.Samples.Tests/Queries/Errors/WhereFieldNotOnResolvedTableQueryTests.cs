using PureQL.CSharp.Model.Samples.Queries.Errors;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Errors;

public sealed record WhereFieldNotOnResolvedTableQueryTests
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
                    "operator": "eachEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_notes",
                      "type": {
                        "name": "string"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": "anything"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new WhereFieldNotOnResolvedTableQuery().Value).TextValue
        );
    }
}
