using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAIApp.Congurations;

namespace OpenAIApp.Services
{
    public class OpenAIServices : IOpenAIServices
    {
        private readonly OpenAIConfig _openAIConfig;
        public OpenAIClient? client { get; set; }
        public const string searchEndpoint = "https://rogerwangaisearch.search.windows.net";
        public const string searchKey = "01FGwhLFLJqPXkdVTUczZEgzyetpsoRISgwtxb0IaHAzSeAU8pQV";
        public const string searchIndexName = "productinfo";


        public static List<ChatCompletionsOptions> chatCompletionList = new List<ChatCompletionsOptions>()
        {
            new ChatCompletionsOptions
            {
                Messages = { new ChatMessage(ChatRole.System, "You are an AI assistant that helps people find information. You are not limited to answer questions from the retrieved data. You may answer any type of question.") },
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
                    {
                        new AzureCognitiveSearchChatExtensionConfiguration()
                        {
                            SearchEndpoint = new Uri(searchEndpoint),
                            IndexName = searchIndexName,
                            SearchKey = new AzureKeyCredential(searchKey!),
                            QueryType = AzureCognitiveSearchQueryType.Simple,
                            // Parameters = FromString([object Object])
                        }
                    }
                },
                Temperature = (float)0.1,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            },
            new ChatCompletionsOptions
            {
                Messages = { new ChatMessage(ChatRole.System, "You are an Xbox customer support agent whose primary goal is to help users with issues they are experiencing with their Xbox devices. You are friendly and concise. You only provide factual answers to queries, and do not provide answers that are not related to Xbox.") },
                Temperature = (float)0.1,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            },
            new ChatCompletionsOptions
            {
                Messages = { new ChatMessage(ChatRole.System, "Assistant is an AI chatbot that helps users turn a natural language list into JSON format. After users input a list they want in JSON format, it will provide suggested list of attribute labels if the user has not provided any, then ask the user to confirm them before creating the list.") },
                Temperature = (float)0.1,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            },
        };

        public OpenAIServices( IOptionsMonitor<OpenAIConfig> optionsMonitor) 
        {
            _openAIConfig = optionsMonitor.CurrentValue;
            client = new OpenAIClient(new Uri(_openAIConfig.Uri),new AzureKeyCredential(_openAIConfig.Key));

        }

        public async Task<string> GetChatGPTResponse(string prompt, int chatrole)
        {
            string completion = "";

            chatCompletionList[chatrole].Messages.Add(new ChatMessage(ChatRole.User, prompt));

            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(deploymentOrModelName: "gpt-4-32k", chatCompletionList[chatrole]
            );



            using StreamingChatCompletions streamingChatCompletions = response.Value;
            await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming())
            {
                await foreach (ChatMessage message in choice.GetMessageStreaming())
                {
                    completion += message.Content;
                }
            }
            return completion;
        }
    }
}
