using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Nodes;

namespace HassDeviceBaseWorkers
{
    /// <summary>
    /// Use cache to skip equal values in MQTT messages
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="cache"></param>
    /// <param name="payload"></param>
    /// <param name="logger"></param>
    public class JsonCachedPayload(string deviceId, IMemoryCache cache, JsonObject payload, ILogger logger)
    {
        protected string DeviceId { get; } = deviceId;
        protected JsonObject Payload { get; } = payload;
        protected IMemoryCache Cache { get; } = cache;
        protected ILogger Logger { get; } = logger;

        public void CachedAdd(string propertyName, object? value)
        {
            if (value == null)
                return;

            var key = $"{DeviceId}-{propertyName}";

            if (Cache.TryGetValue(key, out var cachedValue) && (cachedValue?.Equals(value) ?? false) )
            {
                Logger.LogTrace("Value '{Property}'='{Value}' in the cache and is equals to stored in the cache '{cachedValue}' - do not inserting into JSON.",
                    propertyName, value, cachedValue);

                return;
            }

            Cache.Set(key, value, TimeSpan.FromMinutes(30));
            Payload.Add(propertyName, JsonValue.Create(value));

            Logger.LogTrace("Value '{Property}'='{Value}' not in the cache or value not equal to stored in the cache '{cachedValue}' - inserting into JSON.",
                propertyName, value, cachedValue);
        }

        public override string ToString() => Payload.ToString();
    }
}
