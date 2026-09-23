using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record CalendarEdgeValuesQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_edge_date",
                      "type": {
                        "name": "date"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_edge_datetime",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_edge_time",
                      "type": {
                        "name": "time"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CalendarEdgeValuesQuery().Value).TextValue
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
                      "name": "user_edge_date",
                      "type": "date"
                    },
                    {
                      "name": "user_edge_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "user_edge_time",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-02-29",
                      "2024-02-29T00:00:00",
                      "00:00:00"
                    ],
                    [
                      "2024-12-31",
                      "2024-12-31T23:59:59",
                      "23:59:59"
                    ],
                    [
                      "2024-03-10",
                      "2024-03-10T02:30:00",
                      "02:30:00"
                    ],
                    [
                      "2024-11-03",
                      "2024-11-03T01:30:00",
                      "01:30:00"
                    ],
                    [
                      "0001-01-01",
                      "0001-01-01T00:00:00",
                      "00:00:00"
                    ],
                    [
                      "9999-12-31",
                      "9999-12-31T23:59:59",
                      "23:59:59"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CalendarEdgeValuesQuery().Result).TextValue
        );
    }
}
