using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeBooleanArrayEqualityOfTwoReorderedLiteralArraysQueryTests
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
                        "name": "booleanArray"
                      },
                      "value": [
                        true,
                        false,
                        true
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "booleanArray"
                      },
                      "value": [
                        true,
                        true,
                        false
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeBooleanArrayEqualityOfTwoReorderedLiteralArraysQuery().Value
            ).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": []
                }
                """
            ).TextValue,
            new DataSetJson(
                new WholeBooleanArrayEqualityOfTwoReorderedLiteralArraysQuery().Result
            ).TextValue
        );
    }
}
