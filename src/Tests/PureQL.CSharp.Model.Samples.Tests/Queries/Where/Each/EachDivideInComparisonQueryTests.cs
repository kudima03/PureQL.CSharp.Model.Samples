using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachDivideInComparisonQueryTests
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
                      "value": 40
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachDivideInComparisonQuery().Value).TextValue
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
                      "pending"
                    ],
                    [
                      "cancelled"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new EachDivideInComparisonQuery().Result).TextValue
        );
    }
}
