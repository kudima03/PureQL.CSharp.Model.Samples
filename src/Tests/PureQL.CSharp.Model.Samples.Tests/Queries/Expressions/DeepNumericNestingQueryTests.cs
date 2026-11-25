using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record DeepNumericNestingQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new DeepExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "equal",
                        "type": {"name": "boolean"},
                        "left": {"source": "schema_with_foreign_keys.order_items", "field": "item_product_id", "type": {"name": "uuid"}},
                        "right": {"source": "schema_with_foreign_keys.products", "field": "product_id", "type": {"name": "uuid"}}
                      }
                    }
                  ],
                  "where": {
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "if",
                      "type": {
                        "name": "integer"
                      },
                      "condition": {
                        "operator": "equal",
                        "type": {"name": "boolean"},
                        "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"operator": "equal", "type": {"name": "boolean"}, "left": {"operator": "if", "type": {"name": "integer"}, "condition": {"source": "schema_with_foreign_keys.products", "field": "product_in_stock", "type": {"name": "boolean"}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}}, "right": {"type": {"name": "integer"}, "value": 0}}, "then": {"operator": "floor", "type": {"name": "integer"}, "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}}, "else": {"type": {"name": "integer"}, "value": 0}},
                        "right": {"type": {"name": "integer"}, "value": 0}
                      },
                      "then": {
                        "operator": "floor",
                        "type": {"name": "integer"},
                        "operand": {"source": "schema_with_foreign_keys.order_items", "field": "item_qty", "type": {"name": "decimal"}}
                      },
                      "else": {
                        "type": {"name": "integer"},
                        "value": 0
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
                    }
                  },
                  "select": [
                    {
                      "alias": "item_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {"name": "uuid"}
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new DeepQueryJson(new DeepNumericNestingQuery().Value).TextValue
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
                      "name": "item_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DeepNumericNestingQuery().Result).TextValue
        );
    }
}
