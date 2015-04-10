﻿#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System;
using System.Linq.Expressions;
using Bifrost.Rules;
using Bifrost.Extensions;
using System.Reflection;
using System.Collections.Generic;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents an implementation of <see cref="IQueryValidator"/>
    /// </summary>
    public class QueryValidator : IQueryValidator
    {
        IQueryValidationDescriptors _descriptors;
        IRuleContexts _ruleContexts;

        /// <summary>
        /// Initializes an instance of <see cref="QueryValidator"/>
        /// </summary>
        /// <param name="descriptors"><see cref="IQueryValidationDescriptors"/> for getting descriptors for queries for running through rules</param>
        /// <param name="ruleContexts"><see cref="IRuleContexts"/> used for getting <see cref="IRuleContext"/></param>
        public QueryValidator(IQueryValidationDescriptors descriptors, IRuleContexts ruleContexts)
        {
            _descriptors = descriptors;
            _ruleContexts = ruleContexts;
        }

#pragma warning disable 1591 // Xml Comments
        public QueryValidationResult Validate(IQuery query)
        {
            var brokenRules = new Dictionary<IRule, BrokenRule>();

            var ruleContext = _ruleContexts.GetFor(query);
            ruleContext.OnFailed(RuleFailed(ruleContext, brokenRules));

            var hasDescriptor = _descriptors.CallGenericMethod<bool, IQueryValidationDescriptors>(d => d.HasDescriptorFor<IQuery>, query.GetType());
            if (hasDescriptor)
            {
                var descriptor = _descriptors.CallGenericMethod<IQueryValidationDescriptor, IQueryValidationDescriptors>(d => d.GetDescriptorFor<IQuery>, query.GetType());
                descriptor.ArgumentRules.ForEach(r => {
                    var value = r.Property.GetValue(query);
                    r.Evaluate(ruleContext, value);
                });
            }

            var result = new QueryValidationResult(brokenRules.Values);
            return result;
        }

        RuleFailed RuleFailed(IRuleContext ruleContext, Dictionary<IRule, BrokenRule> brokenRules)
        {
            return (rule, instance, reason) =>
            {
                BrokenRule brokenRule;
                if (brokenRules.ContainsKey(rule)) brokenRule = brokenRules[rule];
                else
                {
                    brokenRule = new BrokenRule(rule, instance, ruleContext);
                    brokenRules[rule] = brokenRule;
                }
                brokenRule.AddReason(reason);
            };
        }
#pragma warning restore 1591 // Xml Comments
    }
}
