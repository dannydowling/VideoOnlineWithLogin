using System.Collections.Generic;
using System.Linq;
using VideoOnlineWithLogin.Shared;
using VideoOnlineWithLogin.Data;

namespace VideoOnlineWithLogin.Api.Models
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _appDbContext;

        public VideoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _appDbContext.Videos;
        }

        public Video GetVideoById(int videoId)
        {
            return _appDbContext.Videos.FirstOrDefault(c => c.videoId == videoId);
        }

        public Video AddVideo(Video video)
        {
            var addedEntity = _appDbContext.Videos.Add(video);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Video UpdateVideo(Video video)
        {
            var foundvideo = _appDbContext.Videos.FirstOrDefault(e => e.videoId == video.videoId);

            if (foundvideo != null)
            {
                foundvideo.videoName = video.videoName;
                foundvideo.videoLink = video.videoLink;

                _appDbContext.SaveChanges();

                return foundvideo;
            }

            return null;
        }

        public void DeleteVideo(int videoId)
        {
            var foundVideo = _appDbContext.Videos.FirstOrDefault(e => e.videoId == videoId);
            if (foundVideo == null) return;

            _appDbContext.Videos.Remove(foundVideo);
            _appDbContext.SaveChanges();
        }
    }
}
