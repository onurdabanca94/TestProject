using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect - metodun başında sonunda hata vermesini istediğimiz hamle gibi.
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //Attribute'da Type ile geçmek zorundayız. - Constructor
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) // Doğrulama metodun başında yapılır o sebeple yaptık.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Bir new'lemeyi çalışma anında yapmak vb. için bir reflectiondır bu.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //Örn; ProductValidator'ın AbstractValidator'ınıdaki Product nesnesi ile çalış - Product tipi
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //Onun parametrelerini bul (Product metodunun)
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
