using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record NullableScoreColumnQueryTests
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
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_score",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullableScoreColumnQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "user_score",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "30"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      ""
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "30"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      ""
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "10"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "28"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullableScoreColumnQuery().Result).TextValue
        );
    }
}
