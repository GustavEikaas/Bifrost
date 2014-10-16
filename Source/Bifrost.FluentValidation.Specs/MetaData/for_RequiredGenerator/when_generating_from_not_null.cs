﻿using Machine.Specifications;
using FluentValidation.Validators;
using Bifrost.Validation.MetaData;

namespace Bifrost.Specs.Validation.MetaData.for_RequiredGenerator
{
    public class when_generating_from_not_null
    {
        static NotNullValidator validator;
        static RequiredGenerator generator;
        static Required result;

        Establish context = () =>
        {
            validator = new NotNullValidator();
            generator = new RequiredGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty",validator) as Required;

        It should_create_a_rule = () => result.ShouldNotBeNull();
    }
}
