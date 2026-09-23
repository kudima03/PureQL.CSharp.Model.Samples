using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record WideProjectionWithEighteenAliasedExpressionsFromOrdersQueryTests
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
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_0"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_1"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_2"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_3"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_4"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_5"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_6"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_7"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_8"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_9"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_10"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_11"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_12"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_13"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_14"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_15"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_16"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "wide_17"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new WideProjectionWithEighteenAliasedExpressionsFromOrdersQuery().Value
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
                      "name": "wide_0",
                      "type": "string"
                    },
                    {
                      "name": "wide_1",
                      "type": "double"
                    },
                    {
                      "name": "wide_2",
                      "type": "string"
                    },
                    {
                      "name": "wide_3",
                      "type": "double"
                    },
                    {
                      "name": "wide_4",
                      "type": "string"
                    },
                    {
                      "name": "wide_5",
                      "type": "double"
                    },
                    {
                      "name": "wide_6",
                      "type": "string"
                    },
                    {
                      "name": "wide_7",
                      "type": "double"
                    },
                    {
                      "name": "wide_8",
                      "type": "string"
                    },
                    {
                      "name": "wide_9",
                      "type": "double"
                    },
                    {
                      "name": "wide_10",
                      "type": "string"
                    },
                    {
                      "name": "wide_11",
                      "type": "double"
                    },
                    {
                      "name": "wide_12",
                      "type": "string"
                    },
                    {
                      "name": "wide_13",
                      "type": "double"
                    },
                    {
                      "name": "wide_14",
                      "type": "string"
                    },
                    {
                      "name": "wide_15",
                      "type": "double"
                    },
                    {
                      "name": "wide_16",
                      "type": "string"
                    },
                    {
                      "name": "wide_17",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5",
                      "shipped",
                      "100.5"
                    ],
                    [
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50",
                      "pending",
                      "50"
                    ],
                    [
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200",
                      "shipped",
                      "200"
                    ],
                    [
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25",
                      "cancelled",
                      "75.25"
                    ],
                    [
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300",
                      "shipped",
                      "300"
                    ],
                    [
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5",
                      "pending",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new WideProjectionWithEighteenAliasedExpressionsFromOrdersQuery().Result
            ).TextValue
        );
    }
}
