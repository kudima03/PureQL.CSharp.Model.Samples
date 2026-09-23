using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachAndOfDividedTotalBelowThresholdAndAddedTotalAboveThresholdOverOrdersQueryTests
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
                        "operator": "eachLessThan",
                        "left": {
                          "operator": "eachDivide",
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
                              "value": 2
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 100
                        }
                      },
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
                              "value": 20
                            }
                          ]
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 90
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachAndOfDividedTotalBelowThresholdAndAddedTotalAboveThresholdOverOrdersQuery().Value
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
                      "00000068-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new EachAndOfDividedTotalBelowThresholdAndAddedTotalAboveThresholdOverOrdersQuery().Result
            ).TextValue
        );
    }
}
