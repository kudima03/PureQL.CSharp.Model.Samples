using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record LiteralArithmeticDivideByZeroQueryTests
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
                      "operator": "divide",
                      "arguments": [
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 1
                        },
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 0
                        }
                      ],
                      "alias": "result"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LiteralArithmeticDivideByZeroQuery().Value).TextValue
        );
    }
}
