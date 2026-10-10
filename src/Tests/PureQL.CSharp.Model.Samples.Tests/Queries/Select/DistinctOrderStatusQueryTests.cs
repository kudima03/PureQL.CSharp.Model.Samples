using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record DistinctOrderStatusQueryTests
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
                      "alias": "order_status",
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
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(new DistinctOrderStatusQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled"
                    ],
                    [
                      "pending"
                    ],
                    [
                      "shipped"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DistinctOrderStatusQuery().Result).TextValue
        );
    }
}
