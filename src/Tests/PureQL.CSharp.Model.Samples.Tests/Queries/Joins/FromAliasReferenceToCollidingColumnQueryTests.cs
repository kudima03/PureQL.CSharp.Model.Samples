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
}
