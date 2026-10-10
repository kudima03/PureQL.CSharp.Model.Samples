using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingCountQueryTests
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
                  "groupBy": [
                    {
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThanOrEqual",
                    "left": {
                      "operator": "count"
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 2
                    }
                  },
                  "select": [
                    {
                      "alias": "order_user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "count"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new HavingCountQuery().Value).TextValue
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
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new HavingCountQuery().Result).TextValue
        );
    }
}
