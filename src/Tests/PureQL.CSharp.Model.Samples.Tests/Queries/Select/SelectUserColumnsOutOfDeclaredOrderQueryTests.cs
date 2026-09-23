using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectUserColumnsOutOfDeclaredOrderQueryTests
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
                      "field": "user_age",
                      "type": {
                        "name": "number"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SelectUserColumnsOutOfDeclaredOrderQuery().Value).TextValue
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
                      "name": "user_age",
                      "type": "double"
                    },
                    {
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "user_active",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "30",
                      "00000001-0000-0000-0000-000000000000",
                      "True"
                    ],
                    [
                      "25",
                      "00000002-0000-0000-0000-000000000000",
                      "False"
                    ],
                    [
                      "30",
                      "00000003-0000-0000-0000-000000000000",
                      "True"
                    ],
                    [
                      "42",
                      "00000004-0000-0000-0000-000000000000",
                      "True"
                    ],
                    [
                      "25",
                      "00000005-0000-0000-0000-000000000000",
                      "False"
                    ],
                    [
                      "28",
                      "00000006-0000-0000-0000-000000000000",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectUserColumnsOutOfDeclaredOrderQuery().Result
            ).TextValue
        );
    }
}
