using AnalyticsService.Services.Interfaces;

namespace AnalyticsService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; 
using AnalyticsService.Services.Implementations; 
[Route("api/[controller]")]
[ApiController]
public class ModuleReportController : ControllerBase
{
    private readonly IReportPropertyService _reportPropertyService;

    public ModuleReportController(IReportPropertyService reportPropertyService)
        {
            _reportPropertyService = reportPropertyService;
        }

        // POST api/moduleReport/generate
        [HttpPost("generate/{ModuleId}")]
        public async Task<IActionResult> GenerateModuleReport(int ModuleId )
        {
            try
            {
                var pdfBytes = await _reportPropertyService.GenerateModuleReportPdfAsync(ModuleId); 
                // Si le PDF est vide ou null, on renvoie 404
                // if (pdfBytes == null || pdfBytes.Length == 0)
                //     return NotFound("Le PDF est vide ou introuvable.");
                return File(pdfBytes, "application/pdf", $"ModuleReport{ModuleId}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }  
        // POST api/rapportquestionnaire/generate
     /*   [HttpPost("generate/{ModuleId}/{QuestionnaireId}")]
        public async Task<IActionResult> GenerateRapportQuestionnaire(int ModuleId, int QuestionnaireId)
        {
            try
            {
                // 1) Utiliser le service pour générer le rapport et l'enregistrer dans la base de données
                var pdfBytes = await _reportPropertyService.GenerateMQReportPdfAsync(ModuleId, QuestionnaireId);

                // 2) Retourner le PDF généré
                return File(pdfBytes, "application/pdf", $"RapportQuestionnaire_{QuestionnaireId}_Module_{ModuleId}.pdf");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner un message d'erreur
                return StatusCode(500, new { message = ex.Message });
            }
        }*/
     [HttpGet("generateHtml/{ModuleId}")]
     public async Task<IActionResult> GeneratePerformanceReportHtml(int ModuleId)
     {
         try
         {
             // Appeler la méthode pour générer le rapport
             var Html= await _reportPropertyService.GenerateModuleReportPdfAsyncHtml(ModuleId);

             // Retourner le fichier généré (ici, on suppose que c'est un PDF)
             return Ok(Html);
         }
         catch (Exception ex)
         {
             // Gérer les erreurs (par exemple, si l'enseignant n'existe pas ou s'il y a un problème de génération)
             return BadRequest($"Une erreur est survenue lors de la génération du html : {ex.Message}");
         }
     }

    }

   
