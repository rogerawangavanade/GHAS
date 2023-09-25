export class ChatService {
    constructor() {
        this.baseUrl = 'https://openaichatbotapiexample1.azurewebsites.net';
        // this.baseUrl = "https://localhost:7126"
    }

    async getChatGPTPizzaConversation(query) {
        try {
            // Include the 'query' parameter in the URL
            const url = `${this.baseUrl}/api/OpenAI/GetChatGPTPizzaConversation?query=${encodeURIComponent(query)}`;
            const request = await fetch(url);
            const data = await request.text();
            return data;
        } catch (error) {
            throw new Error(`Error fetching chat conversation: ${error.message}`);
        }
    }
}