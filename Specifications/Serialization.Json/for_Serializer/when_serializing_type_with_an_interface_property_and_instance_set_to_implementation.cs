﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Serialization.Json.Specs.for_Serializer
{
    public class when_serializing_type_with_an_interface_property_and_instance_set_to_implementation : given.a_serializer
    {
        const string expected_content_value = "Something";

        static ClassToSerialize class_to_serialize;

        static string result;

        Establish context = () =>
                                {
                                    class_to_serialize = new ClassToSerialize
                                    {
                                        Something = new SomethingImplementation { SomeValue = expected_content_value }
                                    };
                                };

        Because of = () => result = serializer.ToJson(class_to_serialize);

        It should_not_contain_type_information = () => result.ShouldNotContain(typeof(SomethingImplementation).Name);
    }
}
