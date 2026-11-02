using Pure.Primitives.Abstractions.DateTime;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using String = Pure.Primitives.String.String;

namespace PureQL.CSharp.Model.Samples.Cells;

/// <summary>
/// A <c>datetime</c> cell of an expected result: the invariant text of a UTC instant
/// ending in <c>Z</c>, the one spelling the specification gives a <c>datetime</c>
/// result. A stored fixture cell is written without the designator and read as UTC.
/// </summary>
internal sealed record UtcDateTimeCell : ICell
{
    public UtcDateTimeCell(IDateTime value)
    {
        Value = new JoinedString(
            new EmptyString(),
            [new InvariantCellText(value), new String("Z")]
        );
    }

    public IString Value { get; }
}
