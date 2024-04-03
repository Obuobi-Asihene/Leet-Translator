using Leet_Translator.Models;
using Leet_Translator.Services;
using Leet_Translator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Leet_Translator.Controllers
{
    public class TranslationController : Controller
    {
        private readonly ITranslationService _translationService;
        public TranslationController(ITranslationService translationService)
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
            try
            {
                string translatedText = await _translationService.TranslateToLeetSpeak(request.InputText);

                //Log API call and result
                Log.Information("API call: TranslateToLeetSpeak, InputText: {InputText}, TranslatedText: {TranslatedText}, Timestamp: {Timestamp}",
                    request.InputText, translatedText, DateTime.UtcNow);

                return Json(new { translatedText });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while translating text.");

                return Json(new { ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetTranslationRecords()
        {
            try
            {
                var records = _translationService.GetTranslationRecords();

                return Json(records);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving translation records.");

                return StatusCode(500, "An error occured while retrieving translation records.");
            }
        }
    }
}
