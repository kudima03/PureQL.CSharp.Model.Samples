using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record MultipleAggregatesOfDifferentTypesQueryTests
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
                    },
                    {
                      "operator": "min_date",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      },
                      "alias": "earliestPlacedOn"
                    },
                    {
                      "operator": "max_string",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      },
                      "alias": "maxStatus"
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
            new QueryJson(new MultipleAggregatesOfDifferentTypesQuery().Value).TextValue
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
                    },
                    {
                      "name": "earliestPlacedOn",
                      "type": "date"
                    },
                    {
                      "name": "maxStatus",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2",
                      "150.5",
                      "2024-06-01",
                      "shipped"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "1",
                      "200",
                      "2024-06-03",
                      "shipped"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "1",
                      "100.5",
                      "2024-06-06",
                      "pending"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2",
                      "375.25",
                      "2024-06-04",
                      "shipped"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new MultipleAggregatesOfDifferentTypesQuery().Result
            ).TextValue
        );
    }
}
