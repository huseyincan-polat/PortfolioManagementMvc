using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagementMvc.Models
{
    public class AssetModel
    {
        [Key]
        public int AssetId { get; set; }

        [Required]
        public string AssetSymbol { get; set; } = string.Empty;

        [Required]
        public string AssetName { get; set; } = string.Empty;

        public int TargetYear { get; set; }

        public ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
    }
}