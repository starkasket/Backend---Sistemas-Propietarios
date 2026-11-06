using System;

namespace Backend.Services;

public class EmailService : IEmailService
{
    public async Task<string> SendAsync(string to, string subject, string body)
    {
        await Task.Delay(500);

        return $"Se envía un mensaje a {to} con asunto: {subject}";
    }
}