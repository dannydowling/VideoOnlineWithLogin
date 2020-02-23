using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VideoOnlineWithLogin.Shared
{
    public class Video
    {
        [Key]
        public Guid videoId { get; set; } = new Guid();

        public string videoName { get; set; }
        public Uri videoLink { get; set; }

    }
}
