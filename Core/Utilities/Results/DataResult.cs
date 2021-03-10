using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T> //DataResult için T = çalışırken bizim tarafımızdan tanımlanacak, Result inherit ediyor ve IDataResult implementasyonu
    {
        //Result sadece void'ler içindi fakat DataResult, Result içerisindekileri kapsayıcı rolündedir.
        public DataResult(T data, bool success, string message):base(success,message) //base class'a Result'a iki değişkeni geçir dedik.
        {
            Data = data;
        }
        public DataResult(T data, bool success):base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
