﻿using System.Linq;
using Bifrost.Extensions;
using Bifrost.Validation;
using Bifrost.Validation.MetaData;
using FluentValidation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    [Subject(typeof(ValidationMetaDataGenerator))]
    public class when_generating_for_an_object_with_concept_on_it_and_a_model_rule : given.a_validation_meta_data_generator_with_common_rules
    {
        static ObjectWithConceptValidator    validator;
        static ValidationMetaData result;

        Establish context = () =>
        {
            ValidatorOptions.PropertyNameResolver = NameResolvers.PropertyNameResolver;
            validator = new ObjectWithConceptValidator();
        };

        Because of = () => result = generator.GenerateFrom(validator);

        It should_have_required_for_string_concept = () => result["stringConcept"]["required"].ShouldNotBeNull();
        It should_have_required_for_long_concept = () => result["longConcept"]["required"].ShouldNotBeNull();
        It should_have_required_for_non_concept = () => result["nonConceptObject"]["required"].ShouldNotBeNull();
        It should_not_have_any_model_rules = () => result.Properties.Keys.Any(k => k.Contains(ModelRule<string>.ModelRulePropertyName.ToCamelCase())).ShouldBeFalse();
    }
}
