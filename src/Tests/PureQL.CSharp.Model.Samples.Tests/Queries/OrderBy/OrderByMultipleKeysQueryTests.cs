using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByMultipleKeysQueryTests
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
                      "alias": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_age",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByMultipleKeysQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "Eve"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Bob"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByMultipleKeysQuery().Result).TextValue
        );
    }
}
