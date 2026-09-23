using PureQL.CSharp.Model.Samples.Queries.Parameters;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Parameters;

public sealed record NumberParameterInEachEqualityQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_age",
                      "type": {
                        "name": "number"
                      }
                    },
                    "right": {
                      "name": "age",
                      "type": {
                        "name": "number"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new NumberParameterInEachEqualityQuery().Value).TextValue
        );
    }
}
