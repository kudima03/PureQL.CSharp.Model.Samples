using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record InListParameterAndNotInListLiteralQueryTests
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
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "in",
                        "value": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "list": {
                          "param_name": "statuses",
                          "type": {
                            "name": "stringList"
                          }
                        }
                      },
                      {
                        "operator": "not",
                        "condition": {
                          "operator": "in",
                          "value": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "list": {
                            "type": {
                              "name": "uuidList"
                            },
                            "value": [
                              "00000002-0000-0000-0000-000000000000",
                              "00000004-0000-0000-0000-000000000000"
                            ]
                          }
                        }
                      }
                    ]
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InListParameterAndNotInListLiteralQuery().Value).TextValue
        );
    }
}
