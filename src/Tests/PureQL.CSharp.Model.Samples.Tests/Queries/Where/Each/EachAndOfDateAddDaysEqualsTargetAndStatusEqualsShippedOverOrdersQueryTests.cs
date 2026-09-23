using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachAndOfDateAddDaysEqualsTargetAndStatusEqualsShippedOverOrdersQueryTests
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
                        "operator": "eachEqual",
                        "left": {
                          "operator": "eachDateAddDays",
                          "left": {
                            "entity": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "number"
                            },
                            "value": 1
                          }
                        },
                        "right": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-02"
                        }
                      },
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
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachAndOfDateAddDaysEqualsTargetAndStatusEqualsShippedOverOrdersQuery().Value
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
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new EachAndOfDateAddDaysEqualsTargetAndStatusEqualsShippedOverOrdersQuery().Result
            ).TextValue
        );
    }
}
