using PureQL.CSharp.Model.Samples.Queries.Errors;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Errors;

public sealed record SelectUnknownEntityQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "select": [
                    {
                      "entity": "shop.nonexistent_entity",
                      "field": "whatever",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SelectUnknownEntityQuery().Value).TextValue
        );
    }
}
