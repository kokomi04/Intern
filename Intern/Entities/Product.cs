using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public int CategoryTypeId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public int ProductStatusId { get; set; }
        public int ProducerId { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ShellPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}

        public List<ProductImg> ProductImgs { get; set; }
        public List<BillDetail> BillDetails { get; set; }
        public List<VoteStar> VoteStars { get; set; }
        public List<AccountBag> AccountBags { get; set; }

        public Size Size { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        public CategoryType CategoryType { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public Producer Producer { get; set; }
    }
}
