﻿#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
using System;
using System.Collections.Generic;
using Bifrost.Events;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptions"/> specific for the Azure DocumentDB
    /// </summary>
    public class EventSubscriptions : IEventSubscriptions
    {
#pragma warning disable 1591
        public IEnumerable<EventSubscription> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(EventSubscription subscription)
        {
            throw new NotImplementedException();
        }

        public void ResetLastEventForAllSubscriptions()
        {
            throw new NotImplementedException();
        }
#pragma warning restore 1591
    }
}
