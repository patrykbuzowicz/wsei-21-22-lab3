using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsei.Lab3.Services;

namespace Wsei.Lab3.Controllers
{
    public class MetricsController : Controller
    {
        private readonly IMetricsCollector _collector;

        public MetricsController(IMetricsCollector collector)
        {
            _collector = collector;
        }

        public IActionResult Index()
        {
            var stats = _collector.GetEndpointStats();
            return View(stats);
        }
    }
}
