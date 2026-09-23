using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record InnerJoinOnPriceLessThanTotalQueryTests
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
                      "entity": "schema_with_foreign_keys.products",
                      "field": "product_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "eachLessThan",
                        "left": {
                          "entity": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "number"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "number"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InnerJoinOnPriceLessThanTotalQuery().Value).TextValue
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
                      "name": "product_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Widget"
                    ],
                    [
                      "Widget"
                    ],
                    [
                      "Widget"
                    ],
                    [
                      "Widget"
                    ],
                    [
                      "Widget"
                    ],
                    [
                      "Widget"
                    ],
                    [
                      "Gadget"
                    ],
                    [
                      "Gadget"
                    ],
                    [
                      "Gadget"
                    ],
                    [
                      "Gadget"
                    ],
                    [
                      "Gadget"
                    ],
                    [
                      "Gadget"
                    ],
                    [
                      "Gizmo"
                    ],
                    [
                      "Gizmo"
                    ],
                    [
                      "Gizmo"
                    ],
                    [
                      "Gizmo"
                    ],
                    [
                      "Gizmo"
                    ],
                    [
                      "Gizmo"
                    ],
                    [
                      "Deluxe"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InnerJoinOnPriceLessThanTotalQuery().Result).TextValue
        );
    }
}
