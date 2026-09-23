using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record ScalarAlongsideWholeSetCountQueryTests
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
                      "type": {
                        "name": "string"
                      },
                      "value": "all",
                      "alias": "scope"
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "order_count"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ScalarAlongsideWholeSetCountQuery().Value).TextValue
        );
    }

    [Fact]
    public void ResultMatchesExpectedRows()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "name": "",
                  "columns": [
                    {
                      "name": "scope",
                      "type": "string"
                    },
                    {
                      "name": "order_count",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "all",
                      "6"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarAlongsideWholeSetCountQuery().Result).TextValue
        );
    }
}
