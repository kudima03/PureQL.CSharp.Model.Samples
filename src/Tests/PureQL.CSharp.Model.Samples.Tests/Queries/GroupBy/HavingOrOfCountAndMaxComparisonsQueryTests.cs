using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingOrOfCountAndMaxComparisonsQueryTests
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
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "having": {
                    "operator": "or",
                    "conditions": [
                      {
                        "operator": "greaterThan",
                        "left": {
                          "operator": "count",
                          "arg": {
                            "entity": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 1
                        }
                      },
                      {
                        "operator": "greaterThanOrEqual",
                        "left": {
                          "operator": "max_number",
                          "arg": {
                            "entity": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "number"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 200
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new HavingOrOfCountAndMaxComparisonsQuery().Value).TextValue
        );
    }
}
