﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Execution.for_WeakDelegate
{
    public class when_dynamically_invoking_instance_delegate_matching_signature
    {
        static ClassWithMethod target;
        static WeakDelegate weak_delegate;
        static object result;

        Establish context = () =>
        {
            target = new ClassWithMethod();
            Func<string, double, int> @delegate = target.SomeMethod;
            weak_delegate = new WeakDelegate(@delegate);
        };

        Because of = () => result = weak_delegate.DynamicInvoke("Something", 42.42);

        It should_call_delegate_and_return_the_result = () => result.ShouldEqual(ClassWithMethod.ReturnValue);
    }
}
