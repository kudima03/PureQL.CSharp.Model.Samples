using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record ScalarWithPaginationQueryTests
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
                      "value": 9,
                      "alias": "page_marker"
                    }
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarWithPaginationQuery().Value).TextValue
        );
    }
}
