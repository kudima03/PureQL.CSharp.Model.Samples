using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record ScalarGroupKeyFieldAndAggregateQueryTests
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
                      "type": {
                        "name": "string"
                      },
                      "value": "2024-06",
                      "alias": "period"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
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
                      "alias": "status_total"
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ScalarGroupKeyFieldAndAggregateQuery().Value).TextValue
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
                      "name": "period",
                      "type": "string"
                    },
                    {
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "status_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06",
                      "cancelled",
                      "75.25"
                    ],
                    [
                      "2024-06",
                      "pending",
                      "150.5"
                    ],
                    [
                      "2024-06",
                      "shipped",
                      "600.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarGroupKeyFieldAndAggregateQuery().Result).TextValue
        );
    }
}
