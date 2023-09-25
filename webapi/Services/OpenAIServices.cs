using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using OpenAIApp.Congurations;

namespace OpenAIApp.Services
{
    public class OpenAIServices : IOpenAIServices
    {
        private readonly OpenAIConfig _openAIConfig;
        public OpenAIClient? client { get; set; }

        public ChatCompletionsOptions chatShakespearCompletion = new ChatCompletionsOptions()
        {
            Messages =
                {
                    new ChatMessage(ChatRole.System, "You are a helpful assistant from the Shakespearean era. You will speak in Shakespearean jargon"),
                }
        };

        public static ChatCompletionsOptions chatConversationPizzaOrder = new ChatCompletionsOptions()
        {
            Messages ={
                    new ChatMessage(ChatRole.System, "You are OrderBot, an automated service to collect orders for a pizza restaurant." +
                    "You first greet the customer, then collects the order, and then asks if it's a pickup or delivery. " +
                    "You wait to collect the entire order, then summarize it and check for a final time if the customer wants to add anything else. " +
                    "You must ensure that all items requested by the customer is on the menu" + 
                    "If it's a delivery, you ask for an address. Finally you collect the payment. Make sure to clarify all options, extras and sizes to uniquely" +
                    "identify the item from the menu.You respond in a short, very conversational friendly style. The menu includes pepperoni pizza  12.95, 10.00, 7.00 " +
                    "cheese pizza   10.95, 9.25, 6.50 eggplant pizza   11.95, 9.75, 6.75 fries 4.50, 3.50 greek salad 7.25 Toppings: extra cheese 2.00, " +
                    "mushrooms 1.50 sausage 3.00 canadian bacon 3.50 AI sauce 1.50 peppers 1.00 Drinks: coke 3.00, 2.00, 1.00 sprite 3.00, 2.00, 1.00 bottled water 5.00 "),
                }
        };


        public OpenAIServices( IOptionsMonitor<OpenAIConfig> optionsMonitor) 
        {
            _openAIConfig = optionsMonitor.CurrentValue;
            client = new OpenAIClient(new Uri(_openAIConfig.Uri),new AzureKeyCredential(_openAIConfig.Key));

        }
        public async Task<string> GetChatGPTResponse(string query)
        {
            OpenAIClient client = new OpenAIClient(_openAIConfig.Key);
            string deploymentName = "gpt-3.5-turbo";
            Response<Completions> response = client.GetCompletions(deploymentName, query);
            var completion = response.Value.Choices[0].Text;
            return completion;
        }

        public async Task<string> GetChatGPTContextResponse(string query)
        {
            string completion = "";

            chatShakespearCompletion.Messages.Add(new ChatMessage(ChatRole.User, query));

            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(
                deploymentOrModelName: "gpt-3.5-turbo",
                chatShakespearCompletion);
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

        public async Task<string> GetChatGPTPizzaConversation(string query)
        {
            string completion = "";

            chatConversationPizzaOrder.Messages.Add(new ChatMessage(ChatRole.User, query));

            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(
                deploymentOrModelName: "gpt-3.5-turbo",
                chatConversationPizzaOrder);
            using StreamingChatCompletions streamingChatCompletions = response.Value;

            await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming())
            {
                await foreach (ChatMessage message in choice.GetMessageStreaming())
                {
                    completion += message.Content;
                }
            }

            chatConversationPizzaOrder.Messages.Add(new ChatMessage(ChatRole.Assistant, completion));

            return completion;
        }
    }
}
