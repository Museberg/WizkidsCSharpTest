using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordPredictionBackend.Clients;
using WordPredictionBackend.Data;
using System;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace WordPredictionBackend.Controllers
{
   
    [ApiController]
    public class TextPredictionsController : ControllerBase
    {
        private readonly WebServiceClient _webServiceClient;
        private readonly DictionaryContext _dictionaryContext;

        public TextPredictionsController(WebServiceClient webServiceClient, DictionaryContext dictionaryContext)
        {
            _webServiceClient = webServiceClient;
            _dictionaryContext = dictionaryContext;
        }

        [HttpGet("web")]
        public async Task<IActionResult> WebServicePredictions(string text)
        {
            var predictions = await _webServiceClient.GetPredictionsFromText(text);
            return predictions is not null ? Ok(JsonSerializer.Serialize(predictions)) : BadRequest();
        }

        [HttpGet("local")]
        public async Task<IActionResult> DatabasePredictions(string text)
        {
            // Extracting all words from the database
            var allWords = await _dictionaryContext.Words.Select(w => w.Value).ToListAsync();
            // Getting all words that starts with the given text
            var wordSuggestions = allWords.Where(w => w.StartsWith(text)).ToList();

            return Ok(JsonSerializer.Serialize(wordSuggestions));
        }
    }
}
