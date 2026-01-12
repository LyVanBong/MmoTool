using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.ZaloTool.Database;
using src.ZaloTool.Models;

namespace MmoTool.ZaloTool.Services;

public class ZaloAccountService : IZaloAccountService
{
    private readonly IDbContextFactory<ZaloToolContext> _contextFactory;

    public ZaloAccountService(IDbContextFactory<ZaloToolContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<AccountZalo>> GetAllAccountsAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.AccountZalos.ToListAsync();
    }

    public async Task AddAccountAsync(AccountZalo account)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.AccountZalos.Add(account);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAccountAsync(AccountZalo account)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.AccountZalos.Update(account);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var account = await context.AccountZalos.FindAsync(id);
        if (account != null)
        {
            context.AccountZalos.Remove(account);
            await context.SaveChangesAsync();
        }
    }
}
