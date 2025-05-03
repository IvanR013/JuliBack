using System.ComponentModel.DataAnnotations;

namespace JuliBack.Models;
    public class Images
    {
        
        public int Id { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string PublicUrl { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public int Userid { get; set; }
    }

