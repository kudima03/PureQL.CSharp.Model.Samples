using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachStringMultiElementLiteralArrayOperandQueryTests
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
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachEqual",
                    "left": {
                      "type": {
                        "name": "stringArray"
                      },
                      "value": [
                        "shipped",
                        "zzz",
                        "zzz",
                        "zzz",
                        "zzz",
                        "zzz"
                      ]
                    },
                    "right": {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new EachStringMultiElementLiteralArrayOperandQuery().Value
            ).TextValue
        );
    }
}
