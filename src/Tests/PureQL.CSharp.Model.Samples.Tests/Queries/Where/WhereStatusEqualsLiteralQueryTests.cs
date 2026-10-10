using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record WhereStatusEqualsLiteralQueryTests
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
                  "where": {
                    "operator": "equal",
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": "shipped"
                    }
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
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
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WhereStatusEqualsLiteralQuery().Value).TextValue
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
                      "00000065-0000-0000-0000-000000000000",
                      "100.5"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WhereStatusEqualsLiteralQuery().Result).TextValue
        );
    }
}
