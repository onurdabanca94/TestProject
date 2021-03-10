using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    //static bir sınıfın metodları da static olmalıdır.(C# için - Java hariç)
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            //Generic olarak belirtilen Model nesnesi için validate işlemi yapıldı.
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
