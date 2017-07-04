// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Internal.Protocol;
using Microsoft.AspNetCore.Sockets;
using Microsoft.AspNetCore.Sockets.Internal.Formatters;

namespace Microsoft.AspNetCore.SignalR
{
    public class KLHubLifetimeManager<THub> : HubLifetimeManager<THub>
    {
        private long _nextInvocationId = 0;
        private readonly HubConnectionList _connections = new HubConnectionList();

        public override Task AddGroupAsync(string connectionId, string groupName)
        {
            var connection = _connections[connectionId];
            if (connection == null)
            {
                return Task.CompletedTask;
            }

            var groups = connection.Metadata.GetOrAdd(HubConnectionMetadataNames.Groups, _ => new HashSet<string>());
            lock (groups)
            {
                groups.Add(groupName);
            }

            return Task.CompletedTask;
        }

        public override Task RemoveGroupAsync(string connectionId, string groupName)
        {
            var connection = _connections[connectionId];
            if (connection == null)
            {
                return Task.CompletedTask;
            }

            var groups = connection.Metadata.Get<HashSet<string>>(HubConnectionMetadataNames.Groups);

            if (groups == null)
            {
                return Task.CompletedTask;
            }

            lock (groups)
            {
                groups.Remove(groupName);
            }

            return Task.CompletedTask;
        }

        public override Task InvokeAllAsync(string methodName, object[] args)
        {
            return InvokeAllWhere(methodName, args, c => true);
        }

        private Task InvokeAllWhere(string methodName, object[] args, Func<HubConnectionContext, bool> include)
        {
            var tasks = new List<Task>(_connections.Count);
            var message = new InvocationMessage(GetInvocationId(), nonBlocking: true, target: methodName, arguments: args);

            // TODO: serialize once per format by providing a different stream?
            foreach (var connection in _connections)
            {
                if (!include(connection))
                {
                    continue;
                }

                tasks.Add(WriteAsync(connection, message));
            }

            return Task.WhenAll(tasks);
        }

        public override Task InvokeConnectionAsync(string connectionId, string methodName, object[] args)
        {
            var connection = _connections[connectionId];

            var message = new InvocationMessage(GetInvocationId(), nonBlocking: true, target: methodName, arguments: args);

            return WriteAsync(connection, message);
        }

        public override Task InvokeGroupAsync(string groupName, string methodName, object[] args)
        {
            return InvokeAllWhere(methodName, args, connection =>
            {
                var groups = connection.Metadata.Get<HashSet<string>>(HubConnectionMetadataNames.Groups);
                return groups?.Contains(groupName) == true;
            });
        }

        public override Task InvokeUserAsync(string userId, string methodName, object[] args)
        {
            Guid userIdGuid = Guid.Parse(userId);

            return InvokeAllWhere(methodName, args, connection =>
            {
                return userId.Equals(connection.User.Claims
                                                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                                                        .Select(c => c.Value).FirstOrDefault());
            });
        }
        
       public override Task OnConnectedAsync(HubConnectionContext connection)
        {
            _connections.Add(connection);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(HubConnectionContext connection)
        {
            _connections.Remove(connection);
            return Task.CompletedTask;
        }

        private async Task WriteAsync(HubConnectionContext connection, HubMessage hubMessage)
        {
            var protocol = connection.Metadata.Get<IHubProtocol>(HubConnectionMetadataNames.HubProtocol);
            var payload = connection.Protocol.WriteToArray(hubMessage);
            
            while (await connection.Output.WaitToWriteAsync())
            {
                if (connection.Output.TryWrite(payload))
                {
                    break;
                }
            }
        }

        private string GetInvocationId()
        {
            var invocationId = Interlocked.Increment(ref _nextInvocationId);
            return invocationId.ToString();
        }
    }
}