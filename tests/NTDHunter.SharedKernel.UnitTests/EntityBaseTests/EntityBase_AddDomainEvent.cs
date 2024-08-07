namespace NTDHunter.SharedKernel.UnitTests.EntityBaseTests;

public class EntityBase_AddDomainEvent
{
    private class TestDomainEvent : DomainEventBase { }

    private class TestEntity : EntityBase
    {
        public void AddTestDomainEvent()
        {
            var domainEvent = new TestDomainEvent();
            RaiseDomainEvent(domainEvent);
        }
    }

    [Fact]
    public void AddsDomainEventToEntity()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        entity.AddTestDomainEvent();

        // Assert
        entity.DomainEvents.Should().HaveCount(1);
        entity.DomainEvents.Should().AllBeOfType<TestDomainEvent>();
    }
}
