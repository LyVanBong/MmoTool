using System.Collections.Generic;
using System.Threading.Tasks;
using src.ZaloTool.Models;

namespace src.ZaloTool.Services;

public interface IZaloAccountService
{
    Task<IEnumerable<AccountZalo>> GetAllAccountsAsync();
    Task AddAccountAsync(AccountZalo account);
    Task UpdateAccountAsync(AccountZalo account);
    Task DeleteAccountAsync(int id);
}
