// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

Console.WriteLine("Hello, World!");

//Solid
//O(Open Closed Principle) == yaptığın yazılıma yeni bir özellik ekliyorsa mevcuttaki hiçbir koda dokunmamlısın

ProductManager productManager = new ProductManager(new EfProductDal());

//foreach (var product in productManager.GetAll())
//{
//    Console.WriteLine(product.ProductName);
//}

foreach (var product in productManager.GetByUnitPrice(50,100))
{
    Console.WriteLine(product.ProductName);
}

foreach (var product in productManager.GetAllByCategoryId(5))
{
    Console.WriteLine(product.ProductName);
}