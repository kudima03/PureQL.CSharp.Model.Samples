using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record BoolColumnQueryTests
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new BoolColumnQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True"
                    ],
                    [
                      "False"
                    ],
                    [
                      "True"
                    ],
                    [
                      "True"
                    ],
                    [
                      "False"
                    ],
                    [
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new BoolColumnQuery().Result).TextValue
        );
    }
}
