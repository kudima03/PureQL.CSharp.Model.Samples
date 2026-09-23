using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record SumAndCountOfEachDivideGroupedByOrderUserIdQueryTests
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
                    },
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
                            "entity": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "number"
                            }
                          }
                        ]
                      },
                      "alias": "sumRatio"
                    },
                    {
                      "operator": "count",
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
                            "entity": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "number"
                            }
                          }
                        ]
                      },
                      "alias": "ratioCount"
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
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SumAndCountOfEachDivideGroupedByOrderUserIdQuery().Value
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "sumRatio",
                      "type": "double"
                    },
                    {
                      "name": "ratioCount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "5.016666666666667",
                      "2"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "",
                      "0"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "",
                      "0"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "12.508333333333333",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SumAndCountOfEachDivideGroupedByOrderUserIdQuery().Result
            ).TextValue
        );
    }
}
