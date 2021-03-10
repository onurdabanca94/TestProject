using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result //IResult değil, Inheritance olduğu için
    {
        public SuccessResult(string message) : base(true, message) //base'e gönderilecek elemanları tanımladık base = Result sınıfı gönderilenler de success ve message bilgisidir.
        {

        }
        public SuccessResult():base(true) //message vermedik direkt true yolluyor. - tek parametre
        {

        }
    }
}
