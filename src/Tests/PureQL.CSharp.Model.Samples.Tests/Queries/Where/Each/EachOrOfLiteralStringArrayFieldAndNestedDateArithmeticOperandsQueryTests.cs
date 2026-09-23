using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachOrOfLiteralStringArrayFieldAndNestedDateArithmeticOperandsQueryTests
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
                    "operator": "eachOr",
                    "conditions": [
                      {
                        "operator": "eachEqual",
                        "left": {
                          "type": {
                            "name": "stringArray"
                          },
                          "value": [
                            "shipped",
                            "zzz",
                            "zzz",
                            "zzz",
                            "zzz",
                            "zzz"
                          ]
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        }
                      },
                      {
                        "operator": "eachGreaterThan",
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
                            "value": 30
                          }
                        },
                        "right": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-07-04"
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachOrOfLiteralStringArrayFieldAndNestedDateArithmeticOperandsQuery().Value
            ).TextValue
        );
    }
}
