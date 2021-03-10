using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceColletion)
        {
            serviceColletion.AddMemoryCache();//.Net'in kendisinin. - otomatik injection yapıyor. - Redis'e geçildiğinde bu kod bloğuna ihtiyaç kalmıyor.
            serviceColletion.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceColletion.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceColletion.AddSingleton<Stopwatch>();
            //serviceColletion.AddSingleton<ICacheManager, RedisCacheManager>(); //Sistemi Redis'e geçirmek istersek RedisCacheManager olarak değiştirmek yeterli olacaktır.
        }
    }
}
