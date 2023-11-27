using System;
using System.Collections.Generic;
using System.Linq;

namespace HungVuong_WPF_C2_B1
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public List<T> Items { get; set; }

        public RepositoryBase()
        {
            Items = new List<T>();
        }

        public int Length()
        {
            return Items.Count();
        }

        public List<T> Gets()
        {
            return Items;
        }

        public T GetByIndex(int index)
        {
            return Items[index];
        }

        public void Add(T entity)
        {
            Items.Add(entity);
        }

        public void BulkInsert(List<T> entities)
        {
            Items.AddRange(entities);
        }
    }
}
