using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record DistinctOnMultiColumnProjectionQueryTests
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
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ],
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(new DistinctOnMultiColumnProjectionQuery().Value).TextValue
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
                      "name": "user_active",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "28",
                      "True"
                    ],
                    [
                      "30",
                      "True"
                    ],
                    [
                      "42",
                      "True"
                    ],
                    [
                      "25",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DistinctOnMultiColumnProjectionQuery().Result).TextValue
        );
    }
}
