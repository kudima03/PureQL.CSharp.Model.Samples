using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record EachDivideWithNullScoreOperandQueryTests
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
                    "operator": "eachGreaterThan",
                    "left": {
                      "operator": "eachDivide",
                      "values": [
                        {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "number"
                          }
                        },
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 2
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": -1000
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachDivideWithNullScoreOperandQuery().Value).TextValue
        );
    }
}
