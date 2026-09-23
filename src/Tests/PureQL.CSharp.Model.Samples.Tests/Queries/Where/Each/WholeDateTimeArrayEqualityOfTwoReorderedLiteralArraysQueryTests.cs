using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeDateTimeArrayEqualityOfTwoReorderedLiteralArraysQueryTests
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
                        "name": "datetimeArray"
                      },
                      "value": [
                        "2024-01-01T08:00:00",
                        "2024-02-01T09:00:00",
                        "2024-03-01T10:00:00"
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "datetimeArray"
                      },
                      "value": [
                        "2024-03-01T10:00:00",
                        "2024-02-01T09:00:00",
                        "2024-01-01T08:00:00"
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeDateTimeArrayEqualityOfTwoReorderedLiteralArraysQuery().Value
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
                new WholeDateTimeArrayEqualityOfTwoReorderedLiteralArraysQuery().Result
            ).TextValue
        );
    }
}
