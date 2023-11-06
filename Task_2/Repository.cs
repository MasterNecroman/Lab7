using System;
using System.Collections.Generic;

namespace task_2
{
    class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> Find(Predicate<T> criteria)
        {
            return items.FindAll(criteria);
        }

        public List<T> GetAll()
        {
            return items;
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}