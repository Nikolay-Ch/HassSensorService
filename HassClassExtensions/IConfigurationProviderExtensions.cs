using Microsoft.Extensions.Configuration;

namespace HassSensorServiceExtensions
{
    public static class IConfigurationProviderExtensions
    {
        public static HashSet<string> GetFullKeyNames(this IConfigurationProvider provider, string? rootKey, HashSet<string> initialKeys)
        {
            foreach (var key in provider.GetChildKeys([], rootKey))
            {
                string surrogateKey = key;
                if (rootKey != null)
                {
                    surrogateKey = rootKey + ":" + key;
                }

                GetFullKeyNames(provider, surrogateKey, initialKeys);

                if (!initialKeys.Any(k => k.StartsWith(surrogateKey)))
                {
                    initialKeys.Add(surrogateKey);
                }
            }

            return initialKeys;
        }
    }
}
