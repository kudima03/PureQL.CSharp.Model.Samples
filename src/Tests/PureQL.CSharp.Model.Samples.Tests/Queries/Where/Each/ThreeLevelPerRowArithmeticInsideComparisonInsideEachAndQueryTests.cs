using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record ThreeLevelPerRowArithmeticInsideComparisonInsideEachAndQueryTests
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
                    "operator": "eachAnd",
                    "conditions": [
                      {
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
                              "value": 50
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 150
                        }
                      },
                      {
                        "operator": "eachOr",
                        "conditions": [
                          {
                            "operator": "eachEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          },
                          {
                            "operator": "eachLessThan",
                            "left": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "number"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 60
                            }
                          }
                        ]
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new ThreeLevelPerRowArithmeticInsideComparisonInsideEachAndQuery().Value
            ).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new ThreeLevelPerRowArithmeticInsideComparisonInsideEachAndQuery().Result
            ).TextValue
        );
    }
}
