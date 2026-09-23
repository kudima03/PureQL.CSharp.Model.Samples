using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record FromAliasReferenceToCollidingColumnQueryTests
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
                    "entity": "schema_with_indexes.table_with_indexes",
                    "alias": "need"
                  },
                  "select": [
                    {
                      "entity": "need",
                      "field": "id",
                      "type": {
                        "name": "uuid"
                      },
                      "alias": "ownId"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_indexes.table_with_single_index",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_indexes.table_with_indexes",
                          "field": "tenant_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_indexes.table_with_single_index",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FromAliasReferenceToCollidingColumnQuery().Value).TextValue
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
                      "name": "ownId",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new FromAliasReferenceToCollidingColumnQuery().Result
            ).TextValue
        );
    }
}
