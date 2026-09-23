using System.Text.Json.Nodes;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Storage.Abstractions;

namespace PureQL.CSharp.Model.Samples.Tests;

// The compact JSON document of a stored table data set: its table name, its
// columns (name and type, in schema order), how many indexes it declares, and its
// rows (cell text per column, in row order). Comparing it against the expected
// document asserts everything a sample's Result holds.
internal sealed record DataSetJson
{
    private readonly IStoredTableDataSet _dataSet;

    public DataSetJson(IStoredTableDataSet dataSet)
    {
        _dataSet = dataSet;
    }

    public string TextValue => Document().ToJsonString();

    private JsonObject Document()
    {
        IReadOnlyList<IColumn> columns = [.. _dataSet.TableSchema.Columns];

        JsonArray rows = [];

        foreach (IRow row in _dataSet)
        {
            rows.Add(
                new JsonArray([
                    .. columns.Select(column =>
                        (JsonNode)row.Cells[column].Value.TextValue
                    ),
                ])
            );
        }

        return new JsonObject
        {
            ["name"] = _dataSet.TableSchema.Name.TextValue,
            ["columns"] = new JsonArray([
                .. columns.Select(column =>
                    (JsonNode)
                        new JsonObject
                        {
                            ["name"] = column.Name.TextValue,
                            ["type"] = column.Type.Name.TextValue,
                        }
                ),
            ]),
            ["indexes"] = _dataSet.TableSchema.Indexes.Count(),
            ["rows"] = rows,
        };
    }
}
