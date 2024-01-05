using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProInvestAPI.Domain;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class SmsController : ControllerBase
{
    private readonly TwilioSettings _twilioSettings;

    public SmsController(IOptions<TwilioSettings> twilioSettings)
    {
        _twilioSettings = twilioSettings.Value;
        _twilioSettings.VerifyServiceSid = "VA7428342b2e81f67d08902a74565a7161";
        _twilioSettings.AccountSid ="ACaccd8374d63c8d55fc1512647a67a636";
        _twilioSettings.AuthToken ="f9f39cdd32bebd130afc4e457d292d97";
        TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
    }

    [HttpPost("sendVerificationCode")]
    public async Task<IActionResult> SendVerificationCode([FromBody] SmsRequest smsRequest)
    {
        try
        {
            var verification = await VerificationResource.CreateAsync(
            to: $"+52{smsRequest.ToPhoneNumber}",                
            channel: "sms",
                pathServiceSid: _twilioSettings.VerifyServiceSid
            );

            if (verification.Status == "pending")
                return Ok(new { Message = "Verification code sent successfully." });
            else
                return BadRequest(new { Message = "Failed to send verification code." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el código de verificación: {ex.Message}") ;
            return StatusCode(500, new { Message = "Internal Server Error" + _twilioSettings.AccountSid});
        }
    }

    [HttpPost("checkVerificationCode")]
    public async Task<IActionResult> CheckVerificationCode([FromBody] VerificationCheckRequest verificationCheckRequest)
    {
        try
        {
            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: verificationCheckRequest.ToPhoneNumber,
                code: verificationCheckRequest.Code,
                pathServiceSid: _twilioSettings.VerifyServiceSid
            );

            if (verificationCheck.Status == "approved")
                return Ok(new { Message = "Verification code is valid." });
            else
                return BadRequest(new { Message = "Invalid verification code." });
        }
        catch (Exception ex)
        {
            // Maneja las excepciones según tus necesidades
            Console.WriteLine($"Error al verificar el código: {ex.Message}");
            return StatusCode(500, new { Message = "Internal Server Error" });
        }
    }
}

public class VerificationCheckRequest
{
    public string ToPhoneNumber { get; set; }
    public string Code { get; set; }
}

public class SmsRequest
{
    public string ToPhoneNumber { get; set; }
    public string Message { get; set; }
}

