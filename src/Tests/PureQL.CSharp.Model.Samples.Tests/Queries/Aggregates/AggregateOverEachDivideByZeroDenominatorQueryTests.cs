using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AggregateOverEachDivideByZeroDenominatorQueryTests
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
                      "operator": "sum",
                      "arg": {
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
                                "value": 100.5
                              }
                            ]
                          }
                        ]
                      },
                      "alias": "sumRatio"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new AggregateOverEachDivideByZeroDenominatorQuery().Value
            ).TextValue
        );
    }
}
