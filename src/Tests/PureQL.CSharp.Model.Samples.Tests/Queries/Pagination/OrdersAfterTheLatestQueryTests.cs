using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record OrdersAfterTheLatestQueryTests
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
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_tenant_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "00000386-0000-0000-0000-000000000000"
                    }
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "placed_at",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "order_total",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": 1
                  }
                }
                """
            ).TextValue,
            new QueryJson(new OrdersAfterTheLatestQuery().Value).TextValue
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
                    },
                    {
                      "name": "placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05T14:00:00Z",
                      "300"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04T13:00:00Z",
                      "75.25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrdersAfterTheLatestQuery().Result).TextValue
        );
    }
}
