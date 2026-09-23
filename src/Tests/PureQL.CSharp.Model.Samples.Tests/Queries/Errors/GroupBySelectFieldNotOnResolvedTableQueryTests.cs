using PureQL.CSharp.Model.Samples.Queries.Errors;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Errors;

public sealed record GroupBySelectFieldNotOnResolvedTableQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "status"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_notes",
                      "type": {
                        "name": "string"
                      },
                      "alias": "notes"
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupBySelectFieldNotOnResolvedTableQuery().Value).TextValue
        );
    }
}
