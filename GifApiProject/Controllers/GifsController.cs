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
    }
}
