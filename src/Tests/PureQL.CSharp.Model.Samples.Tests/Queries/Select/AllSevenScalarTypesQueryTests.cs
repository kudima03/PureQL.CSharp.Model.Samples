using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record AllSevenScalarTypesQueryTests
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
                      "type": {
                        "name": "boolean"
                      },
                      "value": true,
                      "alias": "active"
                    },
                    {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-12-31",
                      "alias": "release"
                    },
                    {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-12-31T23:59:58",
                      "alias": "built_at"
                    },
                    {
                      "type": {
                        "name": "number"
                      },
                      "value": 42.5,
                      "alias": "amount"
                    },
                    {
                      "type": {
                        "name": "string"
                      },
                      "value": "v2",
                      "alias": "label"
                    },
                    {
                      "type": {
                        "name": "time"
                      },
                      "value": "17:30:15",
                      "alias": "cutoff"
                    },
                    {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "0f8fad5b-d9cb-469f-a165-70867728950e",
                      "alias": "marker"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AllSevenScalarTypesQuery().Value).TextValue
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
                      "name": "active",
                      "type": "bool"
                    },
                    {
                      "name": "release",
                      "type": "date"
                    },
                    {
                      "name": "built_at",
                      "type": "datetime"
                    },
                    {
                      "name": "amount",
                      "type": "double"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "cutoff",
                      "type": "time"
                    },
                    {
                      "name": "marker",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "2024-12-31",
                      "2024-12-31T23:59:58",
                      "42.5",
                      "v2",
                      "17:30:15",
                      "0f8fad5b-d9cb-469f-a165-70867728950e"
                    ],
                    [
                      "True",
                      "2024-12-31",
                      "2024-12-31T23:59:58",
                      "42.5",
                      "v2",
                      "17:30:15",
                      "0f8fad5b-d9cb-469f-a165-70867728950e"
                    ],
                    [
                      "True",
                      "2024-12-31",
                      "2024-12-31T23:59:58",
                      "42.5",
                      "v2",
                      "17:30:15",
                      "0f8fad5b-d9cb-469f-a165-70867728950e"
                    ],
                    [
                      "True",
                      "2024-12-31",
                      "2024-12-31T23:59:58",
                      "42.5",
                      "v2",
                      "17:30:15",
                      "0f8fad5b-d9cb-469f-a165-70867728950e"
                    ],
                    [
                      "True",
                      "2024-12-31",
                      "2024-12-31T23:59:58",
                      "42.5",
                      "v2",
                      "17:30:15",
                      "0f8fad5b-d9cb-469f-a165-70867728950e"
                    ],
                    [
                      "True",
                      "2024-12-31",
                      "2024-12-31T23:59:58",
                      "42.5",
                      "v2",
                      "17:30:15",
                      "0f8fad5b-d9cb-469f-a165-70867728950e"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AllSevenScalarTypesQuery().Result).TextValue
        );
    }
}
