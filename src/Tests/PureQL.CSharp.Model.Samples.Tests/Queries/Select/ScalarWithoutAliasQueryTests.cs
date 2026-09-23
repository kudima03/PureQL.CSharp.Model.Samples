using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record ScalarWithoutAliasQueryTests
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
                        "name": "number"
                      },
                      "value": 7
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ScalarWithoutAliasQuery().Value).TextValue
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
                      "name": "",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "7"
                    ],
                    [
                      "7"
                    ],
                    [
                      "7"
                    ],
                    [
                      "7"
                    ],
                    [
                      "7"
                    ],
                    [
                      "7"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarWithoutAliasQuery().Result).TextValue
        );
    }
}
