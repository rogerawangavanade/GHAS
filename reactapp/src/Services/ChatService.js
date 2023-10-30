export class ChatService {
    constructor() {
        this.baseUrl = 'https://openaichatbotapiservice.azurewebsites.net';
        //LOCAL
        //  this.baseUrl = "https://localhost:7038"
    }

    async getChatGPTResponse(prompt) {
        try {
            // Include the 'query' parameter in the URL
            const url = `${this.baseUrl}/api/OpenAI/GetChatGPTResponse?prompt=${encodeURIComponent(prompt)}`;
            const request = await fetch(url);
            const data = await request.text();
            return data;
        } catch (error) {
            throw new Error(`Error fetching chat conversation: ${error.message}`);
        }
    }
}