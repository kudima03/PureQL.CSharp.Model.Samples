using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record GroupKeyAndSumQueryTests
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
                      "operator": "sum",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "userTotal"
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
            new QueryJson(new GroupKeyAndSumQuery().Value).TextValue
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
                      "name": "userTotal",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "150.5"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "200"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "100.5"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "375.25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupKeyAndSumQuery().Result).TextValue
        );
    }
}
