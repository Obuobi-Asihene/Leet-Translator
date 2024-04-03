using Leet_Translator.Models;
using Leet_Translator.Services;
using Leet_Translator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Leet_Translator.Controllers
{
    public class TranslationController : Controller
    {
        private readonly ITranslationService _translationService;
        public TranslationController(TranslationService translationService)
        {
            _translationService = translationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Translate(TranslationRequest request)
        {
            string translatedText = await _translationService.TranslateToLeetSpeak(request.InputText);
            
            return Json(new { translatedText });
        }

        [HttpGet]
        public ActionResult GetTranslationRecords()
        {
            var records = _translationService.GetTranslationRecords();

            return Json(records);
        }
    }
}
