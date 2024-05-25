using System.Collections.Generic;

namespace AdvisorApp.Infrastructure.Caching
{
    public class MRUCache<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> _cache;
        private readonly LinkedList<KeyValuePair<TKey, TValue>> _list;

        public MRUCache(int capacity = 5)
        {
            _capacity = capacity;
            _cache = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
            _list = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        public TValue Get(TKey key)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                _list.Remove(node);
                _list.AddLast(node);
                return node.Value.Value;
            }

            return default;
        }

        public void Put(TKey key, TValue value)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                _list.Remove(node);
            }
            else if (_cache.Count == _capacity)
            {
                var lruNode = _list.First;
                _cache.Remove(lruNode.Value.Key);
                _list.RemoveFirst();
            }

            var newNode = _list.AddLast(new KeyValuePair<TKey, TValue>(key, value));
            _cache[key] = newNode;
        }

        public void Delete(TKey key)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                _list.Remove(node);
                _cache.Remove(key);
            }
        }
    }
}
