using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record FullPageQueryTests
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
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 6
                  }
                }
                """
            ).TextValue,
            new QueryJson(new FullPageQuery().Value).TextValue
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
            new DataSetJson(new FullPageQuery().Result).TextValue
        );
    }
}
