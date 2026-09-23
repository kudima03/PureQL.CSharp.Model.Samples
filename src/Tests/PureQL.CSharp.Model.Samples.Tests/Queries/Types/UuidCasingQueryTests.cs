using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record UuidCasingQueryTests
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
                    "entity": "schema_without_foreign_keys.table_without_indexes"
                  },
                  "select": [
                    {
                      "entity": "schema_without_foreign_keys.table_without_indexes",
                      "field": "name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachEqual",
                    "left": {
                      "entity": "schema_without_foreign_keys.table_without_indexes",
                      "field": "id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "0f9e8d7c-6b5a-4938-8271-605f4e3d2c1b"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new UuidCasingQuery().Value).TextValue
        );
    }
}
