using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarFiveLevelAndRootedTreeQueryTests
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
                        "operator": "or",
                        "conditions": [
                          {
                            "operator": "not",
                            "condition": {
                              "operator": "and",
                              "conditions": [
                                {
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "value": true
                                },
                                {
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "value": false
                                }
                              ]
                            }
                          },
                          {
                            "type": {
                              "name": "boolean"
                            },
                            "value": true
                          }
                        ]
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
                              "value": false
                            }
                          },
                          {
                            "operator": "and",
                            "conditions": [
                              {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": true
                              },
                              {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": true
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarFiveLevelAndRootedTreeQuery().Value).TextValue
        );
    }
}
