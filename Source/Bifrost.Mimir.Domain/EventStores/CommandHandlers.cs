﻿#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Mimir.Domain.EventStores.Commands;

namespace Bifrost.Mimir.Domain.EventStores
{
    public class CommandHandlers : IHandleCommands
    {
        IAggregateRootRepository<EventStore> _repository;

        public CommandHandlers(IAggregateRootRepository<EventStore> repository)
        {
            _repository = repository;
        }

        public void Handle(ReplayAll command)
        {
            var eventStore = _repository.Get(EventStore.SystemEventStoreId);
            eventStore.ReplayAll();
        }
    }
}
