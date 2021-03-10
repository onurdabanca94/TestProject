using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Core katmanı diğer katmanları referans almaz!!!(Entities, DataAccess vb.)
    //Generic constraint ile T ile verilecek entityleri sınırlandırıyoruz (int sadasd) yazamaz
    //class : referans tip
    //Aşağıda IEntity demek ile T bir referans tpi olmalı ve T ya IEntity olabilir ya da IEntity'den implemente olan bir nesne olmalı.
    //new() : new'lenebilir olmalı - Normalde interface'ler (IEntity) newlenemezdi ama burada o inceliğe izin veriyoruz.
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null); //Filtre vermiyorsa tüm dataları istiyor demektir. - Filtreleme işlemlerimizi generic hale getiriyor.
        T Get(Expression<Func<T,bool>>filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
