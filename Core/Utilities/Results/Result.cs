using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //constructor - get readonly'dir fakat constructor içerisinde set edilebilir! - Constructor dışında set etmeyeceğiz.
        // Aşağıdaki :this eklentisi classın kendisini temsil ediyor yani Result class'ı
        public Result(bool success, string message) : this(success) //Result'ın constructor'ına tek parametreli olanına success'i yolla
        {
            Message = message;
            //Success = success; - Bu işlemi diğer constructor aşağıda yapsın diyoruz.
        }

        //Constructor overloading - sadece bir sonuç döndürmek için.
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
