using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupByMultipleKeysQueryTests
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
                  "groupBy": [
                    {
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "order_user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "key": 1,
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "count"
                      }
                    },
                    {
                      "alias": "revenue",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "sum",
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupByMultipleKeysQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "shipped",
                      "1",
                      "100.5"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "shipped",
                      "1",
                      "200"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "cancelled",
                      "1",
                      "75.25"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "pending",
                      "1",
                      "50"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "pending",
                      "1",
                      "100.5"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "shipped",
                      "1",
                      "300"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupByMultipleKeysQuery().Result).TextValue
        );
    }
}
