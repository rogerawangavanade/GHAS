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

        public static ChatCompletionsOptions chatCompletion = new ChatCompletionsOptions()
        {
            Messages ={new ChatMessage(ChatRole.System, @"You are an Xbox customer support agent whose primary goal is to help users with issues they are experiencing with their Xbox devices. You are friendly and concise. You only provide factual answers to queries, and do not provide answers that are not related to Xbox."),
            },
            Temperature = (float)0.1,
            MaxTokens = 800,
            NucleusSamplingFactor = (float)0.95,
            FrequencyPenalty = 0,
            PresencePenalty = 0,
        };
        //{
        //    Messages =
        //        {
        //            new ChatMessage(ChatRole.System, @""),
        //        },
        //    Temperature = (float)0.7,
        //    MaxTokens = 800,
        //    NucleusSamplingFactor = (float)0.95,
        //    FrequencyPenalty = 0,
        //    PresencePenalty = 0,
        //};

        public OpenAIServices( IOptionsMonitor<OpenAIConfig> optionsMonitor) 
        {
            _openAIConfig = optionsMonitor.CurrentValue;
            client = new OpenAIClient(new Uri(_openAIConfig.Uri),new AzureKeyCredential(_openAIConfig.Key));

        }

        public async Task<string> GetChatGPTResponse(string prompt)
        {
            string completion = "";

            chatCompletion.Messages.Add(new ChatMessage(ChatRole.User, prompt));

            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(deploymentOrModelName: "gpt35turbochatbot", chatCompletion
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


            //Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync("openaichatbotexamplemodel",
            //new ChatCompletionsOptions()
            //{
            //    Messages =
            //    {
            //        new ChatMessage(ChatRole.System, @""),
            //    },
            //    Temperature = (float)0.7,
            //    MaxTokens = 800,
            //    NucleusSamplingFactor = (float)0.95,
            //    FrequencyPenalty = 0,
            //    PresencePenalty = 0,
            //});

            //ChatCompletions completions = responseWithoutStream.Value;

        }

        //public async Task<string> GetChatGPTResponse(string query)
        //{
        //    OpenAIClient client = new OpenAIClient(_openAIConfig.Key);
        //    string deploymentName = "gpt-3.5-turbo";
        //    Response<Completions> response = client.GetCompletions(deploymentName, query);
        //    var completion = response.Value.Choices[0].Text;
        //    return completion;
        //}

        //public async Task<string> GetChatGPTContextResponse(string query)
        //{
        //    string completion = "";

        //    chatShakespearCompletion.Messages.Add(new ChatMessage(ChatRole.User, query));

        //    Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(
        //        deploymentOrModelName: "gpt-3.5-turbo",
        //        chatShakespearCompletion);
        //    using StreamingChatCompletions streamingChatCompletions = response.Value;

        //    await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming())
        //    {
        //        await foreach (ChatMessage message in choice.GetMessageStreaming())
        //        {
        //            completion += message.Content;
        //        }
        //    }
        //    return completion;
        //}

        //public async Task<string> GetChatGPTPizzaConversation(string query)
        //{
        //    string completion = "";

        //    chatConversationPizzaOrder.Messages.Add(new ChatMessage(ChatRole.User, query));

        //    Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(
        //        deploymentOrModelName: "gpt-3.5-turbo",
        //        chatConversationPizzaOrder);
        //    using StreamingChatCompletions streamingChatCompletions = response.Value;

        //    await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming())
        //    {
        //        await foreach (ChatMessage message in choice.GetMessageStreaming())
        //        {
        //            completion += message.Content;
        //        }
        //    }

        //    chatConversationPizzaOrder.Messages.Add(new ChatMessage(ChatRole.Assistant, completion));

        //    return completion;
        //}
    }
}
