using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled - bir bağımlılık var ama soyuta bağlılık.
        //naming convention -> _productService vb.
        //Constructor'a ver aşağıda kullan işi JavaScript'te geçerlidir.
        //IoC Container -- Inversion of Control Container -- Bellekteki bir liste gibidir. (İçerisine referanslar konulabilir yani; new ProductManager(), new EfProductDal() gibi) -- ProductsController bir IProductService'e ihtiyacı varsa bellekte bir adet new'leyip bize verir.

        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //Swagger -- hazır dokümantasyon yani bu API şu şekilde kullanılır, şu durumda şu kullanılır sen de ona göre yapılandır arayüzünü der gibi.
            //Dependency chain -- bağımlılık zinciri

            Thread.Sleep(5000);

            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpPost("Update")]
        //public IActionResult Update(Product product)
        //{
        //    var result = _productService.Update(product);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
    }
}
