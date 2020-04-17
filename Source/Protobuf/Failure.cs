// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents a failure.
    /// </summary>
    public class Failure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="id"><see cref="FailureId" />.</param>
        /// <param name="reason"><see cref="FailureReason" />.</param>
        public Failure(FailureId id, FailureReason reason)
        {
            Id = id;
            Reason = reason;
        }

        /// <summary>
        /// Gets the <see cref="FailureId" />.
        /// </summary>
        public FailureId Id { get; }

        /// <summary>
        /// Gets the <see cref="FailureReason" />.
        /// </summary>
        public FailureReason Reason { get; }
    }
}