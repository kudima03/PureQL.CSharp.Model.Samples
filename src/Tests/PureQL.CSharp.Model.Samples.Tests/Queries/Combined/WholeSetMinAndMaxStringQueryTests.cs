using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record WholeSetMinAndMaxStringQueryTests
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
                      "operator": "min_string",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      },
                      "alias": "minStatus"
                    },
                    {
                      "operator": "max_string",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      },
                      "alias": "maxStatus"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WholeSetMinAndMaxStringQuery().Value).TextValue
        );
    }
}
