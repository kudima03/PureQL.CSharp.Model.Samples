using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record DeMorganNotOfAndAtThreeLevelsQueryTests
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
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new DeMorganNotOfAndAtThreeLevelsQuery().Value).TextValue
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
            new DataSetJson(new DeMorganNotOfAndAtThreeLevelsQuery().Result).TextValue
        );
    }
}
