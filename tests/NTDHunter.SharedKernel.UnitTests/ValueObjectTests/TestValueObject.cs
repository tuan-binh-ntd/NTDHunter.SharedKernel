namespace NTDHunter.SharedKernel.UnitTests.ValueObjectTests;

public class TestValueObject(int value) : ValueObject
{
    public int Value { get; } = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
