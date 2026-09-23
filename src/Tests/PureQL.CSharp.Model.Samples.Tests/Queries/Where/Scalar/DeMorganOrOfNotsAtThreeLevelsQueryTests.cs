using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record DeMorganOrOfNotsAtThreeLevelsQueryTests
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
                    "operator": "and",
                    "conditions": [
                      {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      },
                      {
                        "operator": "or",
                        "conditions": [
                          {
                            "operator": "not",
                            "condition": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "operator": "not",
                            "condition": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          }
                        ]
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new DeMorganOrOfNotsAtThreeLevelsQuery().Value).TextValue
        );
    }
}
