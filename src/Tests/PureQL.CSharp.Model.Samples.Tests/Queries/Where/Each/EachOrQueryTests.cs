using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachOrQueryTests
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
                          "value": "cancelled"
                        }
                      },
                      {
                        "operator": "eachGreaterThanOrEqual",
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
                          "value": 300
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachOrQuery().Value).TextValue
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
                      "cancelled"
                    ],
                    [
                      "shipped"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new EachOrQuery().Result).TextValue
        );
    }
}
