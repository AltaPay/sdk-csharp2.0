using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
    public class CheckoutSessionResult : ApiResult
    {
        public string SessionId { get; set; }
        public string SessionStatus { get; set; }

        public CheckoutSessionResult(APIResponse apiResponse)
        {
            if (apiResponse.Header.ErrorCode == 0)
            {
                if (!String.IsNullOrEmpty(apiResponse.Body.Result))
                    Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);
                else
                    Result = Result.Success; // Fallback for when Result node is missing

                if (apiResponse.Body.Session != null)
                {
                    SessionId = apiResponse.Body.Session.Id;
                    SessionStatus = apiResponse.Body.Session.Status;
                }
            }
            else
            {
                Result = Result.SystemError;
                ResultMerchantMessage = apiResponse.Header.ErrorMessage;
                ResultMessage = "An error occurred";
            }
        }
    }
}
