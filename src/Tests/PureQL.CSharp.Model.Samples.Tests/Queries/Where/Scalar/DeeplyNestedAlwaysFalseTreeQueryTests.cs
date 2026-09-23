using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record DeeplyNestedAlwaysFalseTreeQueryTests
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
                      },
                      {
                        "operator": "and",
                        "conditions": [
                          {
                            "type": {
                              "name": "boolean"
                            },
                            "value": false
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
                        "operator": "not",
                        "condition": {
                          "operator": "or",
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
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new DeeplyNestedAlwaysFalseTreeQuery().Value).TextValue
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
            new DataSetJson(new DeeplyNestedAlwaysFalseTreeQuery().Result).TextValue
        );
    }
}
