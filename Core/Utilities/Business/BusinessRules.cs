using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        // Araç olduğu için static yazdık.
        public static IResult Run(params IResult[] logics) //params yazdığımız zaman belirtilen IResult ile ilgili istediğimiz kadar kural yazabiliriz. - IResult[] yapınca array olarak sonsuz tane yazılır.
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; // Başarılı değilse error gönder (yazdığımız kural)
                }
            }
            return null; // Başarılı ise bir şey döndürme
        }
    }
}
