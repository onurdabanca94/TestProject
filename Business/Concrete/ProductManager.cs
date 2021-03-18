using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService; // IcategoryDal yerine çağırmamız gereken kural Service katmanından gelecek.
        //ILogger _logger;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add, admin")]
        [ValidationAspect(typeof(ProductValidator))] // Cross Cutting Concern örnekleri; Validation, Log, Cache, Transaction, Auth - Attribute'lara typeof vermemiz şart
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            // business codes - iş kodu
            // validation - doğrulama kodu - attribute ile anlamlandırdığımız için kodunu yazmamıza gerek kalmadı.
            
            //Aşağıda polimorfizm uygulanmıştır.
            IResult result = BusinessRules.Run(CheckIfProductNameIsSameAsAlready(product.ProductName),
                 CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                 CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            //IResult Result'ın referansını tutabilir. - İnterface'i çünkü - SuccessResult olarak değişti ve bir değişiklik yaparak içerisinde gönderilecek olan operasyonları diğer classlarda default olarak sunduk.
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect] // key, value -> KeyValuePair
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //Yetkisi var mı?
            if (DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
                //MaintenanceTime - Bakım zamanı olarak tanımdır, 05-05.59'a kadar Product listesini null döndürecek.
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
            //SuccessDataResult döndürüyoruz, çalışılan tip List<Product>, _productDal.GetAll() döndürülen datadır, işlem sonucu true ve bilgi mesajı da belirtilen string'dir.
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id)); //Product içerisinde CategoryId'si benim gönderdiğim id'ye eşitse onu filtrele
        }

        [CacheAspect]
        //[PerformanceAspect(5)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            // Product listesini belirtilen saate göre kapatıp ekrana mesaj veren if bloğu
            //if (DateTime.Now.Hour == 04)
            //{
            //    return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] //Bellekteki IProductService.Get içerisindekileri sil demektir.
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameIsSameAsAlready(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        //Aşağıdaki metod ile kod tekrarının önüne geçildi.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // Bir kategoride en fazla x ürün olabilir sınırı.
            //Aşağıdaki kod sql sorgusu olarak : Select count(*) from products where categoryId = 1 gibi bir sorgu döndürür.
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 99)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameIsSameAsAlready(string productName)
        {
            // Aşağıdaki kod sql sorgusu olarak : Select count(*) from products where categoryId = 1
            // Aynı isimde ürün eklenemez.
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //.Any() - var mı? demek. linq sorgusu bool'dur.
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

        //[TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);

            return null;
        }
    }
}
