using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System.Collections.Generic;
using System.Data;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    IProductDal _productDal;
    ICategoryService _categoryService;
    public ProductManager(IProductDal productDal, ICategoryService categoryService)
    {
        _productDal = productDal;
        _categoryService = categoryService;
    }

    //validate doğrulama attribute'ü. Attribute'lara tipler "typeof" ile atanır.
    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {

        //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
        //{
        //    //Aynı isimde ürün eklenemez
        //    if (CheckIfProductNameExist(product.ProductName).Success)
        //    {
        //        _productDal.Add(product);
        //        return new SuccessResult(Messages.ProductAded);
        //    }

        //}
        //return new ErrorResult();


        IResult result =  BusinessRules.Run(CheckIfProductNameExist(product.ProductName),CheckIfProductCountOfCategoryCorrect(product.CategoryId),
            CheckIfCategorytLimitExist());

        if(result != null)
        {
            return result;
        }
        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAded);


        ////Bir kategoride en fazla 10 ürün olabilir
        //var result = _productDal.GetAll(p=> p.CategoryId == product.CategoryId).Count;
        //if(result >= 10) 
        //{
        //    return new ErrorResult(Messages.ProductCountOfCategoryError);
        //}

       
     
        //validate
        //ValidationRules
        //business codes

       
    }

    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour == 22)
        {
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }
        //iş kodları
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
    }

    public IDataResult<List<Product>> GetByCategoryId(int id)
    {
        
        return new SuccessDataResult<List<Product >>(_productDal.GetAll(p=>p.CategoryId ==id));
    }

    public IDataResult<Product> GetById(int productid)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productid));
    }

    public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
    }


    [ValidationAspect(typeof(ProductValidator))]
    public IResult Update(Product product)
    {
        ////Bir kategoride en fazla 10 ürün olabilir
        //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
        //if (result >= 10)
        //{
        //    return new ErrorResult(Messages.ProductCountOfCategoryError);
        //}

        throw new NotImplementedException();
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        //Bir kategoride en fazla 10 ürün olabilir
        var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
        if (result >= 10)
        {
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }
        return new SuccessResult();
    }

    private IResult CheckIfProductNameExist(string productName)
    {
        //Aynı isimde ürün eklenemez
        var result = _productDal.GetAll(p => p.ProductName == productName);
        if (result != null)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExist);
        }
        return new SuccessResult();
    }

    private IResult CheckIfCategorytLimitExist()
    {
        //Aynı isimde ürün eklenemez
        var result = _categoryService.GetAll();
        if (result.Data.Count>15)
        {
            return new ErrorResult(Messages.CategoryLimitExceded);
        }
        return new SuccessResult();
    }
}
