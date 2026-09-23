using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingFourLevelNestedNotOrAndNotQueryTests
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
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "having": {
                    "operator": "not",
                    "condition": {
                      "operator": "or",
                      "conditions": [
                        {
                          "operator": "and",
                          "conditions": [
                            {
                              "operator": "greaterThan",
                              "left": {
                                "operator": "count",
                                "arg": {
                                  "entity": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "number"
                                },
                                "value": 1
                              }
                            },
                            {
                              "operator": "greaterThanOrEqual",
                              "left": {
                                "operator": "max_number",
                                "arg": {
                                  "entity": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "number"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "number"
                                },
                                "value": 200
                              }
                            }
                          ]
                        },
                        {
                          "operator": "not",
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "left": {
                              "operator": "min_number",
                              "arg": {
                                "entity": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "number"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 100
                            }
                          }
                        }
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new HavingFourLevelNestedNotOrAndNotQuery().Value).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new HavingFourLevelNestedNotOrAndNotQuery().Result).TextValue
        );
    }
}
