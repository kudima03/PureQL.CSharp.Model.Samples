using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record AggregateColumnsWithAliasesInMixedProjectionQueryTests
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
                      },
                      "alias": "buyer"
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
                      "alias": "purchases"
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
                      "alias": "spend"
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
                new AggregateColumnsWithAliasesInMixedProjectionQuery().Value
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
                      "name": "buyer",
                      "type": "uuid"
                    },
                    {
                      "name": "purchases",
                      "type": "double"
                    },
                    {
                      "name": "spend",
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
                      "00000002-0000-0000-0000-000000000000",
                      "1",
                      "200"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "1",
                      "100.5"
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
            new DataSetJson(
                new AggregateColumnsWithAliasesInMixedProjectionQuery().Result
            ).TextValue
        );
    }
}
