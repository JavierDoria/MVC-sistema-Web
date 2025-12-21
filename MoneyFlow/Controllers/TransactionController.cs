using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;

namespace MoneyFlow.Controllers
{
    public class TransactionController(ServiceManager _serviceManager,
        TransactionManager _transactionManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    
        [HttpGet]
        public IActionResult ServicesByType(string typeService)
        {
            //EL userId esta por defecto en 1
            var userId = 1;
            var services = _serviceManager.GetByType(userId, typeService);
            return Ok(services);
        }
    }
}
