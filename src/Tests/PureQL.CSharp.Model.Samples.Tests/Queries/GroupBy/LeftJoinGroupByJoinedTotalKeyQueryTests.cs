using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record LeftJoinGroupByJoinedTotalKeyQueryTests
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
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    },
                    {
                      "operator": "sum",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_age",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "ageSum"
                    }
                  ],
                  "joins": [
                    {
                      "type": "left",
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
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LeftJoinGroupByJoinedTotalKeyQuery().Value).TextValue
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
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "ageSum",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "",
                      "53"
                    ],
                    [
                      "200",
                      "25"
                    ],
                    [
                      "75.25",
                      "30"
                    ],
                    [
                      "300",
                      "30"
                    ],
                    [
                      "100.5",
                      "72"
                    ],
                    [
                      "50",
                      "30"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LeftJoinGroupByJoinedTotalKeyQuery().Result).TextValue
        );
    }
}
