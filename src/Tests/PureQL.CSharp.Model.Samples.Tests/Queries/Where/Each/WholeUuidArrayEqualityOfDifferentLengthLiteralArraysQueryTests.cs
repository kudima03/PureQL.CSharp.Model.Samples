using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeUuidArrayEqualityOfDifferentLengthLiteralArraysQueryTests
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
                    "operator": "equal",
                    "left": {
                      "type": {
                        "name": "uuidArray"
                      },
                      "value": [
                        "00000065-0000-0000-0000-000000000000",
                        "00000066-0000-0000-0000-000000000000",
                        "00000067-0000-0000-0000-000000000000"
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "uuidArray"
                      },
                      "value": [
                        "00000065-0000-0000-0000-000000000000",
                        "00000066-0000-0000-0000-000000000000"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeUuidArrayEqualityOfDifferentLengthLiteralArraysQuery().Value
            ).TextValue
        );
    }
}
