using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record PaginationAfterJoinQueryTests
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
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.order_items",
                      "field": "item_qty",
                      "type": {
                        "name": "number"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachGreaterThan",
                    "left": {
                      "entity": "schema_with_foreign_keys.order_items",
                      "field": "item_qty",
                      "type": {
                        "name": "number"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 1
                    }
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.order_items",
                          "field": "item_product_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.products",
                          "field": "product_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.order_items",
                        "field": "item_qty",
                        "type": {
                          "name": "number"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": 1
                  }
                }
                """
            ).TextValue,
            new QueryJson(new PaginationAfterJoinQuery().Value).TextValue
        );
    }
}
