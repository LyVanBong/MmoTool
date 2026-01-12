using System.Threading.Tasks;

namespace src.FacebookTool.Services;

public interface IFacebookService
{
    Task RunAutomationAsync();
    Task<string> GetCookiesAsync();
}
