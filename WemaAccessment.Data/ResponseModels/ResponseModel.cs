using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Data.ResponseModels
{
  public class ResponseModel
  {
    public string Message { get;set; }
    public bool HasError { get;set; }
  }
  public class ResponseModel<T> : ResponseModel
  {
    public T Data { get; set; }
  }
}
