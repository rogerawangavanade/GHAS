﻿using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Services;
using System.Data;

namespace ChatBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAIController : Controller
    {

        private readonly ILogger<OpenAIController> _logger;
        private readonly IOpenAIServices _openAIServices;
        public OpenAIController(ILogger<OpenAIController> logger, IOpenAIServices openAIService)
        {
            _logger = logger;
            _openAIServices = openAIService;
        }

        [HttpGet]
        [Route("GetChatGPTResponse")]
        public async Task<IActionResult> ChatGPTResponse(string query)
        {
            string result = await _openAIServices.GetChatGPTResponse(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetChatGPTContextResponse")]
        public async Task<IActionResult> GetChatGPTContextResponse(string query)
        {
            string result = await _openAIServices.GetChatGPTContextResponse(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetChatGPTPizzaConversation")]
        public async Task<IActionResult> GetChatGPTPizzaConversation(string query)
        {
            string result = await _openAIServices.GetChatGPTPizzaConversation(query);

            return Ok(result);
        }
    }
}
