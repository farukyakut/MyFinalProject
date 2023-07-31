using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    // static kullandığımızda clas'ı kullanmak için new'lemeyiz
    public static class Messages
    {
        public static string ProductAded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductListed = "Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "1 kategoride en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExist = "Bu isimde başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori sayısı 15'i aştığı için yeni ürün eklenemedi ";
    }
}
