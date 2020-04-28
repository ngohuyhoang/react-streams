using System;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Stream
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
