using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record TwoNumericAggregatesOverDifferentColumnsQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "operator": "sum",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "orderTotalSum"
                    },
                    {
                      "operator": "average_number",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_age",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "avgAge"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
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
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new TwoNumericAggregatesOverDifferentColumnsQuery().Value
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "orderTotalSum",
                      "type": "double"
                    },
                    {
                      "name": "avgAge",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "150.5",
                      "30"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "200",
                      "25"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "100.5",
                      "42"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "375.25",
                      "30"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new TwoNumericAggregatesOverDifferentColumnsQuery().Result
            ).TextValue
        );
    }
}
