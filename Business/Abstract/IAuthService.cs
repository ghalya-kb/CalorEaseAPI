using Core.Utilities.Result;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDto dto);
        Task<IDataResult<string>> LoginAsync(LoginDto dto);
    }
}
