using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{

    public class Result : IResult
    {
       
        //Manager clas'ında sadece sucess vermek yeterli olur işini yapabilmek için iki tan ctor oluşturuldu.
        public Result(bool success, string message) : this(success)
        {
            Message = message;
            
        }

        public Result(bool success)
        {
            
            Success = success;
        }



        public bool Success { get;  }

        public string Message { get; }
    }
}
