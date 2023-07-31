using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect , metodun sonunda veya başında hata verdiğinde çalışacak yapı
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        //metodun başında attibürütleri doğrular
        protected override void OnBefore(IInvocation invocation)
        {
            //çalışma anında Validator(örneğin Productvalidator) instance'si oluşturur ve Ivalidator'e soyutlar
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //_validatortype = Validator(Örneğin ProductValidator). Validatordan(örneğin ProductValidatordan) Class(örneğin Product) sınıfını çağırır
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //(invocation = method)'un argümanlarını gez, aynı class (örneğin Product) tipindekileri vallidate et
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
