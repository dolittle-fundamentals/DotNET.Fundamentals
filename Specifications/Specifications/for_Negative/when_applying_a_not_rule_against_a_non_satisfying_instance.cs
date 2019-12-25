// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Specifications.Specs.for_Negative
{
    [Subject(typeof(Specification<>))]
    public class when_applying_a_not_rule_against_a_non_satisfying_instance : given.rules_and_colored_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = Is.Not(squares).IsSatisfiedBy(green_circle);

        It should_be_satisfied = () => is_satisfied.ShouldBeTrue();
    }
}