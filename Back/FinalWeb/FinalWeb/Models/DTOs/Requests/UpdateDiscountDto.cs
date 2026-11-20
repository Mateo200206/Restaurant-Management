using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record UpdateDiscountDto
    {
        [Range(0,100)]
        public int DiscountPercentage { get; set; }
    }
}
