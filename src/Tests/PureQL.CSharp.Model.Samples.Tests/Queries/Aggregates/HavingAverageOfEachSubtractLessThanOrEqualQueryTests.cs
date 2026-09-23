using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record HavingAverageOfEachSubtractLessThanOrEqualQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    },
                    {
                      "operator": "average_number",
                      "arg": {
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
                            "entity": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "number"
                            }
                          }
                        ]
                      },
                      "alias": "meanDiff"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ],
                  "having": {
                    "operator": "lessThanOrEqual",
                    "left": {
                      "operator": "average_number",
                      "arg": {
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
                            "entity": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "number"
                            }
                          }
                        ]
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 100
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new HavingAverageOfEachSubtractLessThanOrEqualQuery().Value
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
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "meanDiff",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "92.85"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new HavingAverageOfEachSubtractLessThanOrEqualQuery().Result
            ).TextValue
        );
    }
}
