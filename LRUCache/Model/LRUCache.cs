namespace Program.Model
{
    public class LRUCache<TValue> where TValue : notnull
    {
        private readonly int _maxCapacity;
        private LinkedList<TValue> _list { get; } = new LinkedList<TValue>();
        private readonly Dictionary<TValue, LinkedListNode<TValue>> _dictionary;

        public LRUCache(int maxCapacity = 5)
        {
            _maxCapacity = maxCapacity;
            _dictionary = new Dictionary<TValue, LinkedListNode<TValue>>(_maxCapacity);
        }

        public bool TryGetValue(TValue value, out LinkedListNode<TValue>? linkedListNode)
        {
            linkedListNode = null;

            if (_dictionary.TryGetValue(value, out var existingNode))
            {
                _list.Remove(existingNode);
                _dictionary.Remove(value);

                var nodeCreated = _list.AddFirst(value);
                _dictionary.Add(value, nodeCreated);

                linkedListNode = nodeCreated;

                return true;
            }

            return false;
        }

        public void PutValue(TValue value)
        {
            // Try obtaining the value from the dictionary/list.
            // If it exists already, the reference will be updated by the TryGetValue, and the value will be moved to the beginning of the list.
            // If it doesn't exist, the value will be added to list.
            if (TryGetValue(value, out _))
            {
                return;
            }

            // If the dictionary has already reached it max capacity of storing, then it will be removing the last element from the list to proceed adding the newest.
            if (_dictionary.Count == _maxCapacity)
            {
                LinkedListNode<TValue> leastUsedNode = _list.Last!;
                _list.RemoveLast();
                _dictionary.Remove(leastUsedNode.Value);
            }

            var nodeCreated = _list.AddFirst(value);
            _dictionary.Add(value, nodeCreated);
        }

        public int GetListCount() => _list.Count;
        public int GetDictionaryCount() => _dictionary.Count;
        public TValue GetFirstValue() => _list.First!.Value is null? throw new InvalidOperationException("Empty cache") : _list.First.Value;
        public TValue GetLastValue() => _list.Last!.Value is null ? throw new InvalidOperationException("Empty cache") : _list.Last.Value;
    }
}
