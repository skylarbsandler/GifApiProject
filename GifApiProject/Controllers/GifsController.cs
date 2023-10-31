using Microsoft.AspNetCore.Mvc;
using GifApiProject.Services;
using GifApiProject.Interfaces;
using GifApiProject.Models;

namespace GifApiProject.Controllers
{
    public class GifsController : Controller
    {
        private readonly IGifsAPIService _gifAPIService;

        public GifsController(IGifsAPIService gifsAPIService)
        {
            _gifAPIService = gifsAPIService;
        }

        public async Task<IActionResult> Index()
        {
            List<GifModel> gifs = new List<GifModel>();
            gifs = await _gifAPIService.GetGifs();
            return View(gifs);
        }

        public IActionResult CreateGif()
        {
            return View();
        }

        [HttpPost("CreateGif")]
        public async Task<IActionResult> CreateGifAsync(GifModel gif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Submission");
            }
            var result = await _gifAPIService.CreateGifAsync(gif);
            if (result) { return Ok("Your gif has been successfully created"); }
            else return BadRequest("An error occured while createing the gif");
        }
    }
}
