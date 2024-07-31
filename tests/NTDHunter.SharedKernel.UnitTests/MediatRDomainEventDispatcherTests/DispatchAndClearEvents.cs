namespace NTDHunter.SharedKernel.UnitTests.MediatRDomainEventDispatcherTests;

public class DispatchAndClearEvents
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
    public async void CallsPublishAndClearDomainEvents()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var domainEventDispatcher = new MediatRDomainEventDispatcher(mediatorMock.Object);
        var entity = new TestEntity();
        entity.AddTestDomainEvent();

        // Act
        await domainEventDispatcher.DispatchAndClearEvents(new List<EntityBase> { entity });

        // Assert
        mediatorMock.Verify(m => m.Publish(It.IsAny<DomainEventBase>(), It.IsAny<CancellationToken>()), Times.Once);
        entity.DomainEvents.Should().BeEmpty();
    }
}
