using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachLeftJoinWithCrossEntityArithmeticQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachGreaterThan",
                    "left": {
                      "operator": "eachAdd",
                      "values": [
                        {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "number"
                          }
                        },
                        {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "number"
                          }
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 120
                    }
                  },
                  "joins": [
                    {
                      "type": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new EachLeftJoinWithCrossEntityArithmeticQuery().Value
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
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann"
                    ],
                    [
                      "Bob"
                    ],
                    [
                      "Cara"
                    ],
                    [
                      "Dan"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new EachLeftJoinWithCrossEntityArithmeticQuery().Result
            ).TextValue
        );
    }
}
