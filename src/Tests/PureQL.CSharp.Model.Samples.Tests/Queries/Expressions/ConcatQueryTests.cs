using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record ConcatQueryTests
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
                    "entity": "schema_with_foreign_keys.products"
                  },
                  "select": [
                    {
                      "alias": "label",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "operator": "concat",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ": "
                          },
                          {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_description",
                            "type": {
                              "name": "string"
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConcatQuery().Value).TextValue
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
                      "name": "label",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Widget: Basic widget"
                    ],
                    [
                      "Gadget: Premium gadget"
                    ],
                    [
                      "Gizmo: Compact gizmo"
                    ],
                    [
                      "Deluxe: Deluxe bundle"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConcatQuery().Result).TextValue
        );
    }
}
