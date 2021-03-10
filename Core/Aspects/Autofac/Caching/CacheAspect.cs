using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //Daha önce bellekte varsa cache'den getir yoksa veritabanından getir ve cache'e ekler.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //ReflectedType : namespace + interface ismi (class ismi), Method.Name: GetAll gibi. (Northwind.Business.IProductService.GetAll)
            var arguments = invocation.Arguments.ToList(); // arguments : parametreler, invocation: method - Methodun parametreleri varsa listeye çevir.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // x ?? y varsa x'i yoksa y'yi ekle demektir. arguments.Select -> listeye döndürür, string.Join ise virgül ile onları yanyana getirir.

            if (_cacheManager.IsAdd(key)) //bellekte var mı?
            {
                invocation.ReturnValue = _cacheManager.Get(key); // manuel bir return oluşturuyoruz. - Cache'deki data gelsin
                return;
            }
            invocation.Proceed(); // yoksa metodu devam ettir. method çalıştığında veritabanına gider. datayı getirir.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // 3 parametre buraya eklenmiş olur.
        }
    }
}
