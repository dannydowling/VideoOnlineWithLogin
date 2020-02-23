using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnlineWithLogin.Shared;

namespace VideoOnlineWithLogin.Api.Models
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetAllVideos();
        Video GetVideoById(int videoId);
        Video AddVideo(Video video);
        Video UpdateVideo(Video video);
        void DeleteVideo(int videoId);
    }
}
