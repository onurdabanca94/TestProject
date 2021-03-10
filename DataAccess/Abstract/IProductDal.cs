using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //DataAccess katmanı Core'a bağımlı dolayısı ile referans verdik.
    public interface IProductDal : IEntityRepository<Product> //IEntityRepository'i Product için yapılandırdın demektir.
    {
        List<ProductDetailDto> GetProductDetails();
    }
}

//Code Refactoring