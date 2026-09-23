using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record BareEachMultiplyInSelectWithoutGroupByQueryTests
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
                      "operator": "eachMultiply",
                      "values": [
                        {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "number"
                          }
                        },
                        {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_precision_value",
                          "type": {
                            "name": "number"
                          }
                        }
                      ],
                      "alias": "product"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new BareEachMultiplyInSelectWithoutGroupByQuery().Value
            ).TextValue
        );
    }
}
