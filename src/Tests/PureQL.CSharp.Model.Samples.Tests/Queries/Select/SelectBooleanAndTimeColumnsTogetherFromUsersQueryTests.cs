using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectBooleanAndTimeColumnsTogetherFromUsersQueryTests
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
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
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
            new QueryJson(
                new SelectBooleanAndTimeColumnsTogetherFromUsersQuery().Value
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
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "shift_start",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "09:00:00"
                    ],
                    [
                      "False",
                      "10:00:00"
                    ],
                    [
                      "True",
                      "09:00:00"
                    ],
                    [
                      "True",
                      "11:30:00"
                    ],
                    [
                      "False",
                      "08:00:00"
                    ],
                    [
                      "True",
                      "09:00:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectBooleanAndTimeColumnsTogetherFromUsersQuery().Result
            ).TextValue
        );
    }
}
