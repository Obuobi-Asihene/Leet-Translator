using Leet_Translator.Areas.Identity.Data;
using Leet_Translator.Models;
using Leet_Translator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Leet_Translator.Controllers
{
    [Authorize]
    public class TranslationController : Controller
    {
        private readonly ITranslationService _translationService;
        private readonly UserManager<AppUser> _userManager;
        public TranslationController(ITranslationService translationService, UserManager<AppUser> userManager)
        {
            _translationService = translationService;
            _userManager = userManager;
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
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Json(new { error = "User not found" });
                }

                string translatedText = await _translationService.TranslateToLeetSpeak(request.InputText, currentUser.Id);

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
        public async Task<IActionResult> GetTranslationRecords()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound();
                }

                var records = await _translationService.GetTranslationRecords(currentUser.Id);

                return View("TranslationRecords", records);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving translation records.");

                return StatusCode(500, "An error occured while retrieving translation records.");
            }
        }
    }
}
