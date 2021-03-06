﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Security
{
    /// <summary>
    /// Defines a security descriptor.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered.
    /// You most likely want to subclass <see cref="SecurityDescriptor"/>.
    /// </remarks>
    public interface ISecurityDescriptor
    {
        /// <summary>
        /// Gets the entry point for builidng a <see cref="ISecurityDescriptor"/>.
        /// </summary>
        ISecurityDescriptorBuilder When { get; }

        /// <summary>
        /// Gets the <see cref="ISecurityAction">action builders</see>.
        /// </summary>
        IEnumerable<ISecurityAction> Actions { get; }

        /// <summary>
        /// Add a <see cref="ISecurityAction"/> to the <see cref="ISecurityDescriptor"/>.
        /// </summary>
        /// <param name="securityAction"><see cref="ISecurityAction"/> to add.</param>
        void AddAction(ISecurityAction securityAction);

        /// <summary>
        /// Indicates whether this security descriptor can authorize this particular object.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ISecurityAction"/> that we wish to authorize.</typeparam>
        /// <param name="instanceToAuthorize">Instance of the object that we wish to authorize.</param>
        /// <returns>True if this descriptor can authorize, False otherwise.</returns>
        bool CanAuthorize<T>(object instanceToAuthorize)
            where T : ISecurityAction;

        /// <summary>
        /// Authorizes an object that represents a particular action being undertaken.
        /// </summary>
        /// <param name="instanceToAuthorize">instance of the action being undertaken.</param>
        /// <returns>An <see cref="AuthorizeDescriptorResult"/> indicating the result of the authorization attempt.</returns>
        AuthorizeDescriptorResult Authorize(object instanceToAuthorize);
    }
}
