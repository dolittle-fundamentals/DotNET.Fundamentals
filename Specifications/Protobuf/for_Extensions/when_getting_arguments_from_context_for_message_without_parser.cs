// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Protobuf.for_Extensions
{
    public class when_getting_arguments_from_context_for_message_without_parser
    {
        static CallContext call_context;
        static Exception result;

        Establish context = () =>
        {
            call_context = new CallContext();
            call_context.RequestHeaders.Add(new Execution.Contracts.ExecutionContext().ToArgumentsMetadata());
        };

        Because of = () => result = Catch.Exception(() => call_context.GetArgumentsMessage<MyMessage>());

        It should_throw_missing_parser_for_message_type = () => result.ShouldBeOfExactType<MissingParserForMessageType>();
    }
}