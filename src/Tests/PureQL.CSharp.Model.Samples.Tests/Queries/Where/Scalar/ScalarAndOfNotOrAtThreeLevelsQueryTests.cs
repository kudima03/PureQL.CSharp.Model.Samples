using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarAndOfNotOrAtThreeLevelsQueryTests
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
                        "operator": "not",
                        "condition": {
                          "operator": "or",
                          "conditions": [
                            {
                              "operator": "lessThan",
                              "left": {
                                "type": {
                                  "name": "number"
                                },
                                "value": 1
                              },
                              "right": {
                                "type": {
                                  "name": "number"
                                },
                                "value": 2
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
                        "value": true
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarAndOfNotOrAtThreeLevelsQuery().Value).TextValue
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
                  "rows": []
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarAndOfNotOrAtThreeLevelsQuery().Result).TextValue
        );
    }
}
