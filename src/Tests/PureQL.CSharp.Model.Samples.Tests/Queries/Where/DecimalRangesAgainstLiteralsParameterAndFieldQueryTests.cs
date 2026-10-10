using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record DecimalRangesAgainstLiteralsParameterAndFieldQueryTests
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
                  "where": {
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": 10.5
                        }
                      },
                      {
                        "operator": "lessThan",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1000
                        }
                      },
                      {
                        "operator": "greaterThan",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "param_name": "min_age",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      },
                      {
                        "operator": "lessThanOrEqual",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "user_age",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_age",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new DecimalRangesAgainstLiteralsParameterAndFieldQuery().Value
            ).TextValue
        );
    }
}
