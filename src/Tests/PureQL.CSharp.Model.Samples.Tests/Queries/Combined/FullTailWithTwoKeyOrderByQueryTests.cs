using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record FullTailWithTwoKeyOrderByQueryTests
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
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "orderCount"
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
                      "alias": "totalSum"
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
                    "operator": "greaterThanOrEqual",
                    "left": {
                      "operator": "sum",
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
                      "value": 150
                    }
                  },
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "orderCount",
                        "type": {
                          "name": "number"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "totalSum",
                        "type": {
                          "name": "number"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 2
                  },
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(new FullTailWithTwoKeyOrderByQuery().Value).TextValue
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
                      "name": "orderCount",
                      "type": "double"
                    },
                    {
                      "name": "totalSum",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2",
                      "150.5"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2",
                      "375.25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FullTailWithTwoKeyOrderByQuery().Result).TextValue
        );
    }
}
