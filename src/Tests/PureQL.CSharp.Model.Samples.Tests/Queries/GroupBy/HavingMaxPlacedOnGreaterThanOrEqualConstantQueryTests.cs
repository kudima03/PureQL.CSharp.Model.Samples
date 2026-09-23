using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingMaxPlacedOnGreaterThanOrEqualConstantQueryTests
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
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThanOrEqual",
                    "left": {
                      "operator": "max_date",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-01"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new HavingMaxPlacedOnGreaterThanOrEqualConstantQuery().Value
            ).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new HavingMaxPlacedOnGreaterThanOrEqualConstantQuery().Result
            ).TextValue
        );
    }
}
