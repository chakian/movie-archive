using Authentication.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Authentication.API.Entities
{
    public class AspNetClient
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(250)]
        public string AllowedOrigin { get; set; }
    }
}
