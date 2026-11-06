using System;

namespace Backend.Services;

public interface IEmailService
{
    public Task<string> SendAsync(string to, string subject, string body);
}