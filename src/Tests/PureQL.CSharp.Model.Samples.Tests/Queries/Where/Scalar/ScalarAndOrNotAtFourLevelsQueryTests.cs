using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarAndOrNotAtFourLevelsQueryTests
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
                                  "value": true
                                }
                              ]
                            }
                          },
                          {
                            "operator": "greaterThan",
                            "left": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-01-02"
                            },
                            "right": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-01-01"
                            }
                          }
                        ]
                      },
                      {
                        "operator": "equal",
                        "left": {
                          "type": {
                            "name": "time"
                          },
                          "value": "12:00:00"
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "12:00:00"
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarAndOrNotAtFourLevelsQuery().Value).TextValue
        );
    }

    [Fact]
    public void ResultMatchesExpectedRows()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "name": "",
                  "columns": [
                    {
                      "name": "order_status",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped"
                    ],
                    [
                      "pending"
                    ],
                    [
                      "shipped"
                    ],
                    [
                      "cancelled"
                    ],
                    [
                      "shipped"
                    ],
                    [
                      "pending"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarAndOrNotAtFourLevelsQuery().Result).TextValue
        );
    }
}
