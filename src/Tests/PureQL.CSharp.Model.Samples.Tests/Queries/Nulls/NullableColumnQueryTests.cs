using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullableColumnQueryTests
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
                      "alias": "user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "user_score",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_score",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullableColumnQuery().Value).TextValue
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
            new DataSetJson(new NullableColumnQuery().Result).TextValue
        );
    }
}
