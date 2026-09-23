using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record FalseBooleanScalarQueryTests
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
                      "type": {
                        "name": "boolean"
                      },
                      "value": false,
                      "alias": "flag"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FalseBooleanScalarQuery().Value).TextValue
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
                      "name": "flag",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "False"
                    ],
                    [
                      "False"
                    ],
                    [
                      "False"
                    ],
                    [
                      "False"
                    ],
                    [
                      "False"
                    ],
                    [
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FalseBooleanScalarQuery().Result).TextValue
        );
    }
}
