using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key); //--> Generic olmayan şekilde verilmesi.tip dönüşümü yapmak gereklidir.
        void Add(string key, object value, int duration);
        bool IsAdd(string key); //Cache'de var mı?
        void Remove(string key); //Cache'den siler misin?
        void RemoveByPattern(string pattern); //Başı sonu önemli değil grubu Category, Product, Get gibi olanlar için..
    }
}
