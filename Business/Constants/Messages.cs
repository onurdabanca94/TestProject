using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages //static verdiğimiz için tekrar tekrar newleme yapmamak adına
    {
        public static string ProductAdded = "Ürün eklendi!"; //public'ler pascal case olarak büyük harfle yazılır değişkeni.
        public static string ProductUpdated = "Ürün güncellendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string MaintenanceTime = "Sistem bakıma girmiştir.";
        public static string ProductsListed = "Ürünler listelendi!";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 99 ürün olabilir!";
        public static string ProductNameAlreadyExist = "Bu isimde ürün zaten mevcut.";
        public static string CategoryLimitExceded = "Kategori sayısı sınır limite ulaşıldığı için yeni ürün eklenemez.";
        public static string AuthorizationDenied = "Yetkiniz bulunmamaktadır.";
        public static string UserRegistered = "Kullanıcı kayıt oldu!";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre hatalı..";
        public static string SuccessfulLogin = "Giriş Başarılı!";
        public static string UserAlreadyExists = "Kullanıcı mevcut.";
        public static string AccessTokenCreated = "Giriş Anahtarı Oluşturuldu!";
    }
}
