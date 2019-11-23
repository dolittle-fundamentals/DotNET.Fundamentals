/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Rules.for_Cause
{
    public class when_creating_with_two_arguments_and_title_and_description_using_them
    {
        const string answer = "fourty two";
        const string question = "what is the meaning of life";
        const string title = "The answer is {Answer}, the question is {Question}. Does that {Answer}?";
        static string expected_title = $"The answer is {answer}, the question is {question}. Does that {answer}?";
        const string description = "The long answer is {Answer} with the longer question is {Question}. Does that {Answer}?";
        static string expected_description = $"The long answer is {answer} with the longer question is {question}. Does that {answer}?";

        static Reason reason = Reason.Create("d05cb07b-55f7-4c80-b306-408c69a0ea9d", title, description);
        static Cause instance;
        Because of = () => instance = reason.WithArgs(new{Answer=answer,Question=question});
        It should_have_the_correct_interpolated_title = () => instance.Title.ShouldEqual(expected_title);
        It should_have_the_correct_interpolated_description = () => instance.Description.ShouldEqual(expected_description);
        It should_hold_the_answer_key_and_value = () => instance.Arguments["Answer"].ShouldEqual(answer);
        It should_hold_the_question_key_and_value = () => instance.Arguments["Question"].ShouldEqual(question);
    }
}