using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record FirstPageWithoutSkipQueryTests
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
                    "take": 4
                  }
                }
                """
            ).TextValue,
            new QueryJson(new FirstPageWithoutSkipQuery().Value).TextValue
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
                      "name": "order_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "100.5"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "75.25"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FirstPageWithoutSkipQuery().Result).TextValue
        );
    }
}
