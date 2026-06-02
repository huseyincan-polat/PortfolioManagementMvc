using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagementMvc.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Investor name is required.")]
        [Display(Name = "Investor Name")]
        public string InvestorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Investor Email")]
        public string InvestorEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select an asset.")]
        [Display(Name = "Selected Asset")]
        public int? SelectedAssetId { get; set; }

        [ForeignKey("SelectedAssetId")]
        public AssetModel? Asset { get; set; }
    }
}