using GifApiProject.Models;

namespace GifApiProject.Interfaces
{
    public interface IGifsAPIService
    {
        Task<List<GifModel>> GetGifs();
        Task<bool> CreateGifAsync(GifModel gif);
    }
}
