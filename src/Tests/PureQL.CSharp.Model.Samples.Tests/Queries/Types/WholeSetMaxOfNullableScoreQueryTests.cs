using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record WholeSetMaxOfNullableScoreQueryTests
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
                      "operator": "max_number",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_score",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "max_score"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WholeSetMaxOfNullableScoreQuery().Value).TextValue
        );
    }
}
