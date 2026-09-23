using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByStatusAscThenIdAscQueryTests
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
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByStatusAscThenIdAscQuery().Value).TextValue
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
                    },
                    {
                      "name": "order_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "00000068-0000-0000-0000-000000000000"
                    ],
                    [
                      "pending",
                      "00000066-0000-0000-0000-000000000000"
                    ],
                    [
                      "pending",
                      "0000006a-0000-0000-0000-000000000000"
                    ],
                    [
                      "shipped",
                      "00000065-0000-0000-0000-000000000000"
                    ],
                    [
                      "shipped",
                      "00000067-0000-0000-0000-000000000000"
                    ],
                    [
                      "shipped",
                      "00000069-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByStatusAscThenIdAscQuery().Result).TextValue
        );
    }
}
