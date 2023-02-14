using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Domain.ObjectSync.Synchronization
{
    internal sealed class ConsoleMemorySynchronizationAuthority : MemorySynchronizationAuthority
    {
        public ConsoleMemorySynchronizationAuthority()
        {
        }

        private void Log(string message, params object?[] parameters)
        {
            parameters = parameters
                .Select(p=>p is SyncInfo info ? $"{info.TypeId[..3]}.{info.FieldName}[{info.SourceInstanceId[..3]}]":p)
                .Select(p => Guid.TryParse(p?.ToString()??String.Empty, out var id) ? id.ToString()[..3] : p).ToArray();
            Console.WriteLine(message, parameters);
        }

        protected override void Unsubscribe(SyncInfo syncInfo)
        {
            base.Unsubscribe(syncInfo);

            Log("Unsubscribed {0} from {1}",
                syncInfo.InstanceId, syncInfo);
        }

        protected override void Subscribe<TField>(SyncInfo syncInfo, Action<TField> callback)
        {
            base.Subscribe(syncInfo, callback);

            Log("Subscribed {0} to {1}",
                syncInfo.InstanceId, syncInfo);
        }

        protected override void Push<TField>(SyncInfo syncInfo, TField value)
        {
            base.Push(syncInfo, value);

            Log("Pushed {0} for {1} : {2}",
                syncInfo, syncInfo.InstanceId, value);
        }

        protected override TField Pull<TField>(SyncInfo syncInfo)
        {
            var result = base.Pull<TField>(syncInfo);

            Log("Pulled {0} for {1} : {2}",
                syncInfo, syncInfo.InstanceId, result);

            return result;
        }
    }
}
