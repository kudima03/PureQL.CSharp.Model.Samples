using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record WideProjectionWithTwentyAliasedExpressionsFromUsersQueryTests
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
                      },
                      "alias": "wide_0"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_1"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_2"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_3"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_4"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_5"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_6"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_7"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_8"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_9"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_10"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_11"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_12"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_13"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_14"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_15"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_16"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_17"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_18"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "wide_19"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new WideProjectionWithTwentyAliasedExpressionsFromUsersQuery().Value
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
                      "name": "wide_0",
                      "type": "string"
                    },
                    {
                      "name": "wide_1",
                      "type": "string"
                    },
                    {
                      "name": "wide_2",
                      "type": "string"
                    },
                    {
                      "name": "wide_3",
                      "type": "string"
                    },
                    {
                      "name": "wide_4",
                      "type": "string"
                    },
                    {
                      "name": "wide_5",
                      "type": "string"
                    },
                    {
                      "name": "wide_6",
                      "type": "string"
                    },
                    {
                      "name": "wide_7",
                      "type": "string"
                    },
                    {
                      "name": "wide_8",
                      "type": "string"
                    },
                    {
                      "name": "wide_9",
                      "type": "string"
                    },
                    {
                      "name": "wide_10",
                      "type": "string"
                    },
                    {
                      "name": "wide_11",
                      "type": "string"
                    },
                    {
                      "name": "wide_12",
                      "type": "string"
                    },
                    {
                      "name": "wide_13",
                      "type": "string"
                    },
                    {
                      "name": "wide_14",
                      "type": "string"
                    },
                    {
                      "name": "wide_15",
                      "type": "string"
                    },
                    {
                      "name": "wide_16",
                      "type": "string"
                    },
                    {
                      "name": "wide_17",
                      "type": "string"
                    },
                    {
                      "name": "wide_18",
                      "type": "string"
                    },
                    {
                      "name": "wide_19",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann",
                      "Ann"
                    ],
                    [
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob",
                      "Bob"
                    ],
                    [
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara",
                      "Cara"
                    ],
                    [
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan",
                      "Dan"
                    ],
                    [
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve",
                      "Eve"
                    ],
                    [
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay",
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new WideProjectionWithTwentyAliasedExpressionsFromUsersQuery().Result
            ).TextValue
        );
    }
}
