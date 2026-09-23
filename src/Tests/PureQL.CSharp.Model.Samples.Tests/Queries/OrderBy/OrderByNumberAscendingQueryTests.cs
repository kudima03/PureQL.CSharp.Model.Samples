using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByNumberAscendingQueryTests
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
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "number"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByNumberAscendingQuery().Value).TextValue
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
                      "50"
                    ],
                    [
                      "75.25"
                    ],
                    [
                      "100.5"
                    ],
                    [
                      "100.5"
                    ],
                    [
                      "200"
                    ],
                    [
                      "300"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByNumberAscendingQuery().Result).TextValue
        );
    }
}
