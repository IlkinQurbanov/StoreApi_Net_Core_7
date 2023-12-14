using Microsoft.AspNetCore.Mvc.Filters;

namespace StoreApi.Filters
{
    public class StatsFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            DateTime reference = new DateTime(2020, 1, 1);
            TimeSpan time = DateTime.Now - reference;
            Console.WriteLine("[StatsFilter] OnActionExecuting - Time=" + time.TotalMicroseconds + "ms");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            DateTime reference = new DateTime(2020, 1, 1);
            TimeSpan time = DateTime.Now - reference;
            Console.WriteLine("[StatsFilter] OnActionExecuted - Time=" + time.TotalMicroseconds + "ms");
        }

     
    }
}
