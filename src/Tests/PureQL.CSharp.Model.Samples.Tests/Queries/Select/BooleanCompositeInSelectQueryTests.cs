using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record BooleanCompositeInSelectQueryTests
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
                      "operator": "greaterThan",
                      "left": {
                        "type": {
                          "name": "number"
                        },
                        "value": 2
                      },
                      "right": {
                        "type": {
                          "name": "number"
                        },
                        "value": 1
                      },
                      "alias": "flag"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new BooleanCompositeInSelectQuery().Value).TextValue
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
                      "True"
                    ],
                    [
                      "True"
                    ],
                    [
                      "True"
                    ],
                    [
                      "True"
                    ],
                    [
                      "True"
                    ],
                    [
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new BooleanCompositeInSelectQuery().Result).TextValue
        );
    }
}
