using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record ScalarAlongsideFieldColumnQueryTests
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
                        "name": "string"
                      },
                      "value": "v2",
                      "alias": "release"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ScalarAlongsideFieldColumnQuery().Value).TextValue
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
                      "name": "release",
                      "type": "string"
                    },
                    {
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "v2",
                      "Ann"
                    ],
                    [
                      "v2",
                      "Bob"
                    ],
                    [
                      "v2",
                      "Cara"
                    ],
                    [
                      "v2",
                      "Dan"
                    ],
                    [
                      "v2",
                      "Eve"
                    ],
                    [
                      "v2",
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarAlongsideFieldColumnQuery().Result).TextValue
        );
    }
}
