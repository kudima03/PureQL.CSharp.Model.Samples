using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record PerUserMaxAndCountOverCrossSchemaLoginsQueryTests
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
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "operator": "max_datetime",
                      "arg": {
                        "entity": "audit.logins",
                        "field": "login_at",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "alias": "lastLoginAt"
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "audit.logins",
                        "field": "login_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "loginCount"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "audit.logins",
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
                          "entity": "audit.logins",
                          "field": "login_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new PerUserMaxAndCountOverCrossSchemaLoginsQuery().Value
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "lastLoginAt",
                      "type": "datetime"
                    },
                    {
                      "name": "loginCount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-02T07:30:00",
                      "2"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "2024-06-03T08:00:00",
                      "1"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "2024-06-04T06:45:00",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new PerUserMaxAndCountOverCrossSchemaLoginsQuery().Result
            ).TextValue
        );
    }
}
