using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    public interface ICacheRepository
    {
        bool Add(int key, int cacheItem);
        object GetCacheItem(int key);
        bool Remove(int key);
    }

    public class LRUCacheNode
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public LRUCacheNode Next { get; set; }
        public LRUCacheNode Previous { get; set; }
    }

    public class LRUCacheRepository : ICacheRepository
    {
        public LRUCacheRepository(int numberOfCacheItems)
        {
            this.capacity = numberOfCacheItems;
            this.cacheMap = new ConcurrentDictionary<string, LRUCacheNode>();
        }
        private ConcurrentDictionary<string, LRUCacheNode> cacheMap;
        private int capacity { get; set; }
        private LRUCacheNode head;
        private LRUCacheNode tail;

        public bool Add(int key, int cacheItem)
        {
            LRUCacheNode cacheNode = null;
            if (cacheMap.TryGetValue(key.ToString(), out cacheNode))
            {
                return false;
            }
            cacheNode = new LRUCacheNode()
            {
                Key = key,
                Value = cacheItem
            };
            if (head == null)
            {
                head = cacheNode;
                tail = cacheNode;
            }
            else
            {
                if (cacheMap.Count >= capacity)
                {
                    this.RemoveTailNode();
                }
                cacheNode.Next = head;
                head.Previous = cacheNode;
                head = cacheNode;
            }
            return cacheMap.TryAdd(key.ToString(), cacheNode);
        }

        public object GetCacheItem(int key)
        {
            LRUCacheNode cacheNode;
            if (this.cacheMap.TryGetValue(key.ToString(), out cacheNode))
            {
                if (cacheNode.Previous != null)
                    cacheNode.Previous.Next = cacheNode.Next;

                if (cacheNode.Next != null)
                    cacheNode.Next.Previous = cacheNode.Previous;

                cacheNode.Next = head;
                head.Previous = cacheNode;
                head = cacheNode;
                return cacheNode.Value;
            }
            return null;
        }

        public bool Remove(int key)
        {
            LRUCacheNode cacheNode;
            if (this.cacheMap.TryGetValue(key.ToString(), out cacheNode))
            {
                if (cacheNode.Previous != null)
                    cacheNode.Previous.Next = cacheNode.Next;
                else
                    head = cacheNode.Next;

                if (cacheNode.Next != null)
                    cacheNode.Next.Previous = cacheNode.Previous;
                else
                    tail = cacheNode.Previous;

                cacheNode.Next = null;
                cacheNode.Previous = null;
                return this.cacheMap.TryRemove(key.ToString(), out cacheNode);
            }
            return false;
        }

        private void RemoveTailNode()
        {
            LRUCacheNode cacheNode;
            if (this.cacheMap.TryRemove(tail.Key.ToString(), out cacheNode))
            {
                tail.Previous.Next = null;
                tail = tail.Previous;
            }
        }

        public void DisplayCacheMap()
        {
            StringBuilder sb = new StringBuilder();
            LRUCacheNode cacheNode = head;
            while (cacheNode != null)
            {
                sb.Append(cacheNode.Key);
                cacheNode = cacheNode.Next;
                if (cacheNode != null)
                    sb.Append("-->");
            }
            Console.WriteLine(sb.ToString());
        }
    }

    
}
