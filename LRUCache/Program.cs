using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            LRUCacheRepository cacheRepository = new LRUCacheRepository(3);
            cacheRepository.Add(1,1);
            cacheRepository.DisplayCacheMap();
            cacheRepository.Add(2,2);
            cacheRepository.DisplayCacheMap();
            object obj = cacheRepository.GetCacheItem(1);
            cacheRepository.DisplayCacheMap();
            cacheRepository.Add(3,3);
            cacheRepository.DisplayCacheMap();            
            obj = cacheRepository.GetCacheItem(2);
            cacheRepository.DisplayCacheMap();
            cacheRepository.Add(4,4);
            cacheRepository.DisplayCacheMap();
            obj = cacheRepository.GetCacheItem(1);
            cacheRepository.DisplayCacheMap();
            obj = cacheRepository.GetCacheItem(3);
            cacheRepository.DisplayCacheMap();
            obj = cacheRepository.GetCacheItem(4);
            cacheRepository.DisplayCacheMap();

            Console.ReadLine();
        }
    }
}
