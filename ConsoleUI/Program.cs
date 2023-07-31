// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

Console.WriteLine("Hello, World!");

//Solid
//O(Open Closed Principle) == yaptığın yazılıma yeni bir özellik ekliyorsa mevcuttaki hiçbir koda dokunmamlısın
//IoC
ProductTest();
// DTO = Data Transformation Object
//CategoryTest();

static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

    //foreach (var product in productManager.GetAll())
    //{
    //    Console.WriteLine(product.ProductName);
    //}

    //foreach (var product in productManager.GetByUnitPrice(50, 100).Data)
    //{
    //    Console.WriteLine(product.ProductName);
    //}

    //foreach (var product in productManager.GetByCategoryId(5).Data)
    //{
    //    Console.WriteLine(product.ProductName);
    //}

    var result = productManager.GetProductDetails();

    if (result.Success == true)
    {
        foreach (var product in result.Data)
        {
            Console.WriteLine(product.ProductName + "/" + product.CategoryName);
        }
    }
    else
    {
        Console.WriteLine(result.Message);
    }
    
}

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (var category in categoryManager.GetAll().Data)
    {
        Console.WriteLine(category.CategoryName);
    }
}