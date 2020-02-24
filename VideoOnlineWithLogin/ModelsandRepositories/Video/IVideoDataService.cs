using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnlineWithLogin.Shared;

namespace VideoOnlineWithLogin.Server.Services
{
    public interface IVideoDataService
    {
        Task<IEnumerable<Video>> GetAllVideos();
        Task<Video> GetVideoDetails(int videoId);
        Task<Video> AddVideo(Video video);
        Task UpdateVideo(Video video);
        Task DeleteVideo(int videoId);
    }
}
