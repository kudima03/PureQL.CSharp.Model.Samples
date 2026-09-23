using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record ScalarInGroupModeQueryTests
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
                        "name": "number"
                      },
                      "value": 1,
                      "alias": "version"
                    },
                    {
                      "operator": "sum",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "status_total"
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
            new QueryJson(new ScalarInGroupModeQuery().Value).TextValue
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
                      "name": "version",
                      "type": "double"
                    },
                    {
                      "name": "status_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "75.25"
                    ],
                    [
                      "1",
                      "150.5"
                    ],
                    [
                      "1",
                      "600.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarInGroupModeQuery().Result).TextValue
        );
    }
}
