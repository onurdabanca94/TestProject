using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //hangi tipi döndüreceğini söylüyoruz. - IResult implementasyonu
    {
        T Data { get; } // Data = ürünler,müşteriler, kategoriler vs. - T her şey olabilir yani sınırlandırma yapılmıyor.
    }
}
