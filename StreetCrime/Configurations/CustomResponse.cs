using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace StreetCrime.Configurations
{
  public class CustomResponse
  {
    public object SendSuccessResponse(object data)
    {
      var response = new
      {
        Message = "Operation performed successfully",
        Code = 1,
        Data = data
      };

      return response;
    }

    public object SendErrorResponse(string message)
    {
      return new { Message = "Operation failed to perform", Code = 0, Data = new { Message = message } };
    }

    public object SendErrorResponse(ModelStateDictionary modelStates)
    {
      var errorMessage = modelStates.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage;

      return new { Message = "Operation failed to perform", Code = 0, Data = new { Message = errorMessage } };
    }
  }
}
