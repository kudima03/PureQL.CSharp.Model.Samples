using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record JoinThenWhereThenOrderByThenPaginateQueryTests
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
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachGreaterThan",
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
                      "value": 75
                    }
                  },
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
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "number"
                        }
                      },
                      "direction": "desc"
                    }
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(new JoinThenWhereThenOrderByThenPaginateQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "300"
                    ],
                    [
                      "200"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new JoinThenWhereThenOrderByThenPaginateQuery().Result
            ).TextValue
        );
    }
}
