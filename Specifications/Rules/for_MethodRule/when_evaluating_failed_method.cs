/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Rules.for_MethodRule
{
    public class when_evaluating_failed_method
    {
        static Reason reason = Reason.Create("f2049be1-c421-45d1-bdd5-a9eb4530c3dc", "Title", "Description");
        static Cause cause;


        const string rule_name = "This is the rule";

        static Mock<IRuleContext> rule_context;
        static object instance;
        static MethodRule   rule;

        static RuleEvaluationResult RuleMethod()
        {
            return RuleEvaluationResult.Fail(instance, cause);
        }

        Establish context = () => 
        {
            rule_context = new Mock<IRuleContext>();
            instance = new object();
            rule = new MethodRule(rule_name, RuleMethod);
            cause = reason.NoArgs();
        };

        Because of = () => rule.Evaluate(rule_context.Object, instance);

        It should_notify_about_failure = () => rule_context.Verify(_ => _.Fail(rule, instance, cause), Moq.Times.Once);
    }
}