using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectDateAndDateTimeColumnsTogetherFromOrdersQueryTests
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
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_at",
                      "type": {
                        "name": "datetime"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SelectDateAndDateTimeColumnsTogetherFromOrdersQuery().Value
            ).TextValue
        );
    }
}
