using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record SelectOfCollidingColumnViaUndeclaredAliasQueryTests
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
                      "entity": "sp",
                      "field": "id",
                      "type": {
                        "name": "uuid"
                      },
                      "alias": "specId"
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
            new QueryJson(
                new SelectOfCollidingColumnViaUndeclaredAliasQuery().Value
            ).TextValue
        );
    }
}
