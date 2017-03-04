﻿using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_getting_last_committed_version_without_any_aggregate_with_that_id : given.a_saga
    {
        static EventSourceVersion version;
 
        Because of = () => version = saga.GetLastCommittedVersionFor(Moq.It.IsAny<EventSource>());

        It should_return_a_zero_version = () => version.ShouldEqual(EventSourceVersion.Zero);
    }
}
