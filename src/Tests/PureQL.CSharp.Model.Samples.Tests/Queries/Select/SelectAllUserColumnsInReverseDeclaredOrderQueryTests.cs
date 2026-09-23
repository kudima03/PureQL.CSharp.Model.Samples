using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectAllUserColumnsInReverseDeclaredOrderQueryTests
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
                      "field": "shift_start",
                      "type": {
                        "name": "time"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "last_login",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "signup_date",
                      "type": {
                        "name": "date"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_age",
                      "type": {
                        "name": "number"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    },
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
                new SelectAllUserColumnsInReverseDeclaredOrderQuery().Value
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
                      "name": "shift_start",
                      "type": "time"
                    },
                    {
                      "name": "last_login",
                      "type": "datetime"
                    },
                    {
                      "name": "signup_date",
                      "type": "date"
                    },
                    {
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "user_age",
                      "type": "double"
                    },
                    {
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "user_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "09:00:00",
                      "2024-06-01T08:30:00",
                      "2020-01-15",
                      "True",
                      "30",
                      "Ann",
                      "00000001-0000-0000-0000-000000000000"
                    ],
                    [
                      "10:00:00",
                      "2024-06-02T09:15:00",
                      "2021-03-20",
                      "False",
                      "25",
                      "Bob",
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "09:00:00",
                      "2024-05-30T14:00:00",
                      "2019-07-10",
                      "True",
                      "30",
                      "Cara",
                      "00000003-0000-0000-0000-000000000000"
                    ],
                    [
                      "11:30:00",
                      "2024-06-03T18:45:00",
                      "2022-11-05",
                      "True",
                      "42",
                      "Dan",
                      "00000004-0000-0000-0000-000000000000"
                    ],
                    [
                      "08:00:00",
                      "2024-06-04T07:05:00",
                      "2023-02-28",
                      "False",
                      "25",
                      "Eve",
                      "00000005-0000-0000-0000-000000000000"
                    ],
                    [
                      "09:00:00",
                      "2024-06-01T08:30:00",
                      "2020-01-15",
                      "True",
                      "28",
                      "Fay",
                      "00000006-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectAllUserColumnsInReverseDeclaredOrderQuery().Result
            ).TextValue
        );
    }
}
