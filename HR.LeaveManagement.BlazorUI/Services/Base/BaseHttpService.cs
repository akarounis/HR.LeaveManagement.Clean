namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;

    public BaseHttpService(IClient client)
    {
        _client = client;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if(ex.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted.",
                ValidationErrors = ex.Response,
                Success = false
            };
        }

        if(ex.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "The record was not found.",
                ValidationErrors = ex.Response,
                Success = false
            };
        }

        return new Response<Guid>()
        {
            Message = "Something went wrong, please try again later.",
            Success = false
        };
    }
}
