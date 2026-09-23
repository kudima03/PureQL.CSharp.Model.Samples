using PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Scalar;

public sealed record ScalarDateTimeGreaterThanOrEqualTrueConstantQueryTests
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
                    }
                  ],
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "left": {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-01-01T12:00:00"
                    },
                    "right": {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-01-01T12:00:00"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new ScalarDateTimeGreaterThanOrEqualTrueConstantQuery().Value
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
                      "name": "order_status",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped"
                    ],
                    [
                      "pending"
                    ],
                    [
                      "shipped"
                    ],
                    [
                      "cancelled"
                    ],
                    [
                      "shipped"
                    ],
                    [
                      "pending"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new ScalarDateTimeGreaterThanOrEqualTrueConstantQuery().Result
            ).TextValue
        );
    }
}
