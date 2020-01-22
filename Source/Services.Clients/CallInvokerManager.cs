// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Reflection;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="ICallInvokerManager"/>.
    /// </summary>
    [Singleton]
    public class CallInvokerManager : ICallInvokerManager
    {
        readonly IKnownClients _knownClients;
        readonly ClientEndpointsConfiguration _configuration;
        readonly IMetadataProviders _metadataProviders;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallInvokerManager"/> class.
        /// </summary>
        /// <param name="knownClients">All the <see cref="IKnownClients"/>.</param>
        /// <param name="configuration"><see cref="ClientEndpointsConfiguration">Configuration</see>.</param>
        /// <param name="metadataProviders"><see cref="IMetadataProviders"/> for providing metadata to calls.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public CallInvokerManager(
            IKnownClients knownClients,
            ClientEndpointsConfiguration configuration,
            IMetadataProviders metadataProviders,
            ILogger logger)
        {
            _knownClients = knownClients;
            _configuration = configuration;
            _metadataProviders = metadataProviders;
            _logger = logger;
        }

        /// <inheritdoc/>
        public CallInvoker GetFor(Type type)
        {
            ThrowIfTypeDoesNotImplementClientBase(type);

            var client = _knownClients.GetFor(type);
            var endpointConfiguration = _configuration[client.Visibility];

            var keepAliveTime = new ChannelOption("grpc.keepalive_time", 1000);
            var keepAliveTimeout = new ChannelOption("grpc.keepalive_timeout_ms", 500);
            var keepAliveWithoutCalls = new ChannelOption("grpc.keepalive_permit_without_calls", 1);

            var channel = new Channel(
                endpointConfiguration.Host,
                endpointConfiguration.Port,
                ChannelCredentials.Insecure,
                new[] { keepAliveTime, keepAliveTimeout, keepAliveWithoutCalls });

            return channel.Intercept(_ =>
            {
                _metadataProviders.Provide().ForEach(_.Add);
                return _;
            });
        }

        void ThrowIfTypeDoesNotImplementClientBase(Type type)
        {
            if (!type.Implements(typeof(ClientBase)))
            {
                throw new TypeDoesNotImplementClientBase(type);
            }
        }
    }
}