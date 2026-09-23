using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SingleValueArithmeticWithParameterOperandInSelectQueryTests
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
                      "operator": "add",
                      "arguments": [
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 1
                        },
                        {
                          "name": "bonus",
                          "type": {
                            "name": "number"
                          }
                        }
                      ],
                      "alias": "sum"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SingleValueArithmeticWithParameterOperandInSelectQuery().Value
            ).TextValue
        );
    }
}
