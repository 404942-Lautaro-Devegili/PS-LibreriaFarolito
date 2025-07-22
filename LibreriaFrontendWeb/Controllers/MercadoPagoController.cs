using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercadoPagoController : ControllerBase
    {
        private readonly string _accessToken = "APP_USR-715881156935809-053108-42a4944033153d303573ccf99823d919-2463172721"; // Esta es del vendedor

        [HttpPost("CreatePreference")]
        public async Task<ActionResult> CreatePreference([FromBody] CreatePreferenceRequest request)
        {
            try
            {
                Console.WriteLine($"Access Token being used: {_accessToken}");

                var preference = new
                {
                    items = request.Items.Select(item => new
                    {
                        title = item.Title,
                        quantity = item.Quantity,
                        currency_id = item.CurrencyId,
                        unit_price = item.UnitPrice
                    }).ToArray(),
                    payer = new
                    {
                        email = request.Payer.Email
                    },
                    back_urls = new
                    {
                        success = request.BackUrls.Success,
                        failure = request.BackUrls.Failure,
                        pending = request.BackUrls.Pending
                    },
                    auto_return = request.AutoReturn,
                    external_reference = request.ExternalReference
                };

                Console.WriteLine($"Preference being sent: {JsonSerializer.Serialize(preference, new JsonSerializerOptions { WriteIndented = true })}");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

                    var json = JsonSerializer.Serialize(preference);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://api.mercadopago.com/checkout/preferences", content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"MercadoPago Response: Status={response.StatusCode}");
                    Console.WriteLine($"MercadoPago Response Content: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        var preferenceId = result.GetProperty("id").GetString();

                        return Ok(new { preferenceId = preferenceId });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            message = "Error creando preferencia de pago",
                            details = responseContent,
                            statusCode = (int)response.StatusCode
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreatePreference: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { message = "Error interno", error = ex.Message });
            }
        }
    }

    // DTOs para las requests
    public class CreatePreferenceRequest
    {
        public List<PreferenceItem> Items { get; set; } = new();
        public PayerInfo Payer { get; set; } = new();
        public BackUrlsInfo BackUrls { get; set; } = new();
        public string AutoReturn { get; set; } = string.Empty;
        public string ExternalReference { get; set; } = string.Empty;
    }

    public class PreferenceItem
    {
        public string Title { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string CurrencyId { get; set; } = "ARS";
        public decimal UnitPrice { get; set; }
    }

    public class PayerInfo
    {
        public string Email { get; set; } = string.Empty;
    }

    public class BackUrlsInfo
    {
        public string Success { get; set; } = string.Empty;
        public string Failure { get; set; } = string.Empty;
        public string Pending { get; set; } = string.Empty;
    }
}