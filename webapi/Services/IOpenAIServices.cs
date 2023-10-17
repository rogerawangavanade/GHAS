namespace OpenAIApp.Services
{
    public interface IOpenAIServices
    {
        Task<string> GetChatGPTResponse(string prompt);
    }
}
