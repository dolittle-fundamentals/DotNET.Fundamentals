// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Protobuf;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallClientManager"/>.
    /// </summary>
    public class ReverseCallClientManager : IReverseCallClientManager
    {
        /// <inheritdoc/>
        public Task Handle<TResponse, TRequest>(
            AsyncDuplexStreamingCall<TResponse, TRequest> call,
            Expression<Func<TResponse, Contracts.ReverseCallResponseContext>> responseContextProperty,
            Expression<Func<TRequest, Contracts.ReverseCallRequestContext>> requestContextProperty,
            Func<ReverseCall<TResponse, TRequest>, Task> callback,
            CancellationToken token)
            where TResponse : IMessage
            where TRequest : IMessage
        {
            return Task.Run(
                async () =>
                {
                    while (await call.ResponseStream.MoveNext(token).ConfigureAwait(false))
                    {
                        var request = call.ResponseStream.Current;
                        var callId = requestContextProperty.Compile()(request).CallId.To<ReverseCallId>();
                        var reverseCall = new ReverseCall<TResponse, TRequest>(call.ResponseStream.Current, call.RequestStream, callId, responseContextProperty);
                        await callback(reverseCall).ConfigureAwait(false);
                    }
                }, token);
        }
    }
}