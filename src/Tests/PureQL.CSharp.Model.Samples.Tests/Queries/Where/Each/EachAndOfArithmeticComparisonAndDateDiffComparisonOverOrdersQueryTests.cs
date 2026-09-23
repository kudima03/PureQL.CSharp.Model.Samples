using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachAndOfArithmeticComparisonAndDateDiffComparisonOverOrdersQueryTests
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
                              "value": 10
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
                        "operator": "eachLessThan",
                        "left": {
                          "operator": "eachDateDiffDays",
                          "left": {
                            "entity": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-01"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 4
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachAndOfArithmeticComparisonAndDateDiffComparisonOverOrdersQuery().Value
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
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new EachAndOfArithmeticComparisonAndDateDiffComparisonOverOrdersQuery().Result
            ).TextValue
        );
    }
}
