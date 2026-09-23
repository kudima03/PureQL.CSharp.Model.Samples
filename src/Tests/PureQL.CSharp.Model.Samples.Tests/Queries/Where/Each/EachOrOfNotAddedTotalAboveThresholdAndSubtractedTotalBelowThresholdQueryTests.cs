using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachOrOfNotAddedTotalAboveThresholdAndSubtractedTotalBelowThresholdQueryTests
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
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachOr",
                    "conditions": [
                      {
                        "operator": "eachNot",
                        "condition": {
                          "operator": "eachGreaterThan",
                          "left": {
                            "operator": "eachAdd",
                            "values": [
                              {
                                "entity": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "number"
                                }
                              },
                              {
                                "type": {
                                  "name": "number"
                                },
                                "value": 20
                              }
                            ]
                          },
                          "right": {
                            "type": {
                              "name": "number"
                            },
                            "value": 150
                          }
                        }
                      },
                      {
                        "operator": "eachLessThan",
                        "left": {
                          "operator": "eachSubtract",
                          "values": [
                            {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "number"
                              }
                            },
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 30
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 175
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachOrOfNotAddedTotalAboveThresholdAndSubtractedTotalBelowThresholdQuery().Value
            ).TextValue
        );
    }
}
