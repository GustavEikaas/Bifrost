﻿using System;
using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_processing_an_event_and_there_is_one_subscription_for_the_event : given.an_event_subscription_manager_with_one_subscriber_from_repository_and_matching_in_process
    {
        static EventSubscription actual_subscription;
        static SomeEvent @event;
        static EventAndEnvelope event_and_envelope;
        static SomeEventSubscriber event_subscriber;

        Establish context = () =>
        {
            var event_source_id = Guid.NewGuid();
            @event = new SomeEvent(event_source_id);
            event_and_envelope = new EventAndEnvelope(new Mock<IEventEnvelope>().Object, new SomeEvent(event_source_id));
            
            event_subscriptions_mock.Setup(e => e.Save(Moq.It.IsAny<EventSubscription>())).Callback((EventSubscription s) => actual_subscription = s);
            event_subscriber = new SomeEventSubscriber();
            container_mock.Setup(c=>c.Get(typeof(SomeEventSubscriber))).Returns(event_subscriber);
        };

        Because of = () => event_subscription_manager.Process(event_and_envelope);

        It should_update_subscription = () => actual_subscription.ShouldEqual(subscription);
        It should_update_subscriptions_last_event_id_with_event_id = () => actual_subscription.LastEventId.ShouldEqual(event_and_envelope.Envelope.EventId);
        It should_call_subscription_method = () => event_subscriber.ProcessCalled.ShouldBeTrue();
        It should_ensure_events_are_persisted_in_a_localized_scope = () => localizer_mock.Verify(s => s.BeginScope(), Times.Once());
    }
}
