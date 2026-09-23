using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record NumberScalarQueryTests
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
                      "value": 5,
                      "alias": "version"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NumberScalarQuery().Value).TextValue
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
                      "name": "version",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "5"
                    ],
                    [
                      "5"
                    ],
                    [
                      "5"
                    ],
                    [
                      "5"
                    ],
                    [
                      "5"
                    ],
                    [
                      "5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NumberScalarQuery().Result).TextValue
        );
    }
}
