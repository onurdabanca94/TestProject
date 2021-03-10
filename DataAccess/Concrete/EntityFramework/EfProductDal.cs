using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NortwindContext>, IProductDal
    // IProductDal için gerekli olan işlemler Base class'ımızda olduğu için sadece using verdik ve Product için NorthwindContext veritabanı nesnesi kullanılacak diyerek işlemi bitirdik. Business katmanında IProductDal'a bağımlı. - IProductDal'a ürün ile ilgili özel operasyonları yazacağız.
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NortwindContext context = new NortwindContext())
            {
                //Aşağıdaki tabloları join et diyoruz.
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId //burada = yazılmaz equals yazılır.
                             select new ProductDetailDto {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
