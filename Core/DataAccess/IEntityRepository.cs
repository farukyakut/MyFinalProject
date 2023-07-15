
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//Core Katmanları diğer katmanları referans almaz
namespace Core.DataAccess
{
    //generic constraint
    //class : referans Tip
    //IEntity : IEntity olabilir veya IEntity implement eden bir sınıf olabilir.
    // new() : new'lenebilir olmalı (class), interface new'lenemez
    public interface IEntityRepository<T> where T : class,IEntity, new()
    {
        //Generic Repository

        //Filtreleme yapabilmek için " Expression<Func<T,bool>> filter=null " ifadesi kullanılır.
        //filter = null , filtre vermek zorunda değiliz demektir.
        List<T> GetAll(Expression<Func<T,bool>> filter=null);

        // Detay işlemlerinde kullanılcaktır
        // filtre kullanılmak zorundadır. Çünkü filter = null değil.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        
    }
}
