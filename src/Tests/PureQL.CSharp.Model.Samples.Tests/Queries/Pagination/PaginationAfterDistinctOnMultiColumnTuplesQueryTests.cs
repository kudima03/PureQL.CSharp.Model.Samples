using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record PaginationAfterDistinctOnMultiColumnTuplesQueryTests
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
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_age",
                        "type": {
                          "name": "number"
                        }
                      }
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_active",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": 2
                  },
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(
                new PaginationAfterDistinctOnMultiColumnTuplesQuery().Value
            ).TextValue
        );
    }
}
