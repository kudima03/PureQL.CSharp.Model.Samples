using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectAllUserColumnsQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
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
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
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
                      "field": "last_login",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "shift_start",
                      "type": {
                        "name": "time"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SelectAllUserColumnsQuery().Value).TextValue
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
                    },
                    {
                      "name": "user_age",
                      "type": "double"
                    },
                    {
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "signup_date",
                      "type": "date"
                    },
                    {
                      "name": "last_login",
                      "type": "datetime"
                    },
                    {
                      "name": "shift_start",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "30",
                      "True",
                      "2020-01-15",
                      "2024-06-01T08:30:00",
                      "09:00:00"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Bob",
                      "25",
                      "False",
                      "2021-03-20",
                      "2024-06-02T09:15:00",
                      "10:00:00"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara",
                      "30",
                      "True",
                      "2019-07-10",
                      "2024-05-30T14:00:00",
                      "09:00:00"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "42",
                      "True",
                      "2022-11-05",
                      "2024-06-03T18:45:00",
                      "11:30:00"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "Eve",
                      "25",
                      "False",
                      "2023-02-28",
                      "2024-06-04T07:05:00",
                      "08:00:00"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay",
                      "28",
                      "True",
                      "2020-01-15",
                      "2024-06-01T08:30:00",
                      "09:00:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SelectAllUserColumnsQuery().Result).TextValue
        );
    }
}
