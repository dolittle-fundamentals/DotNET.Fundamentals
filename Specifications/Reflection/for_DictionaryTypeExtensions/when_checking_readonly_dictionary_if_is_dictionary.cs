// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using Machine.Specifications;

namespace Dolittle.Reflection.for_DictionaryTypeExtensions
{
    public class when_checking_readonly_dictionary_if_is_dictionary
    {
        static bool result;

        Because of = () => result = typeof(ReadOnlyDictionary<string, string>).IsDictionary();

        It should_be_considered_a_dictionary = () => result.ShouldBeTrue();
    }
}