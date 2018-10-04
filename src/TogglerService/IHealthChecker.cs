namespace TogglerService
{
    using System.Threading.Tasks;

    public interface IHealthChecker
    {
        Task CheckHealth();
    }
}
