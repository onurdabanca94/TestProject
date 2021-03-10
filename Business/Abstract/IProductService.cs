using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        //DataAccess katmanı ile Entities katmanını Business katmanı referanslara eklemek gerekli
        IDataResult<List<Product>> GetAll(); //IResult void'tir. - IDataResult mesajı döndüreceği listeyi içeren nesne olacaktır. - T:List<Product> olmuş oluyor.
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails(); //Liste döndürüyor.
        IDataResult<Product> GetById(int productId); // T:Product olmuş oluyor.
        IResult Add(Product product); //void olduğu için bir şey döndürmüyor.
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product); //Uygulamalarda tutarlılığı sağlamak

        //RESTFUL --> HTTP --> TCP
    }
}
