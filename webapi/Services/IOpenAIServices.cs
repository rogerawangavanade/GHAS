namespace OpenAIApp.Services
{
    public interface IOpenAIServices
    {
        Task<string> GetChatGPTResponse(string query);
        Task<string> GetChatGPTContextResponse(string query);
        Task<string> GetChatGPTPizzaConversation(string query);

    }
}
