using System.Collections.Generic;

namespace HungVuong_WPF_C2_B1
{
    interface IRepositoryBase<T>
    {
        int Length();

        List<T> Gets();
        T GetByIndex(int index);
        void Add(T entity);
    }
}
