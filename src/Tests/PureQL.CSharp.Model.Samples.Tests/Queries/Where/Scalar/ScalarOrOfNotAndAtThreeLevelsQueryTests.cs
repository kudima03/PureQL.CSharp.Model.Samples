using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarOrOfNotAndAtThreeLevelsQueryTests
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
                    "operator": "or",
                    "conditions": [
                      {
                        "operator": "not",
                        "condition": {
                          "operator": "and",
                          "conditions": [
                            {
                              "operator": "greaterThan",
                              "left": {
                                "type": {
                                  "name": "number"
                                },
                                "value": 5
                              },
                              "right": {
                                "type": {
                                  "name": "number"
                                },
                                "value": 3
                              }
                            },
                            {
                              "operator": "equal",
                              "left": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "x"
                              },
                              "right": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "y"
                              }
                            }
                          ]
                        }
                      },
                      {
                        "type": {
                          "name": "boolean"
                        },
                        "value": false
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarOrOfNotAndAtThreeLevelsQuery().Value).TextValue
        );
    }
}
