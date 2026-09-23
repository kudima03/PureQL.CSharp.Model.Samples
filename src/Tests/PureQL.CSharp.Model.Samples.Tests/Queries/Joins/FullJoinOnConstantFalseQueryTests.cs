using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record FullJoinOnConstantFalseQueryTests
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
                  "joins": [
                    {
                      "type": "full",
                      "entity": "schema_with_foreign_keys.products",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": false
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FullJoinOnConstantFalseQuery().Value).TextValue
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
                      ""
                    ],
                    [
                      ""
                    ],
                    [
                      ""
                    ],
                    [
                      ""
                    ],
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
                    ],
                    [
                      "Eve"
                    ],
                    [
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FullJoinOnConstantFalseQuery().Result).TextValue
        );
    }
}
