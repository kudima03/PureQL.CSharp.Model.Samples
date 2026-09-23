using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record NumberParameterInSelectQueryTests
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
                      "name": "limit",
                      "type": {
                        "name": "number"
                      },
                      "alias": "limit"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NumberParameterInSelectQuery().Value).TextValue
        );
    }
}
