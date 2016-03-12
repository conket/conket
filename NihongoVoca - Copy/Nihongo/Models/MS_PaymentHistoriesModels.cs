using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nihongo.Models
{
    public class MS_PaymentHistoriesModels
    {
        public int VocaSetID { get; set; }
        public int UserID { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public decimal Fee { get; set; }

        public bool HasVoucher { get; set; }
        public int? VoucherID { get; set; }
        public string VoucherCode { get; set; }
        public string PaymentMethod { get; set; }
        public string CaptchaInputText { get; set; }
        public string CardCode { get; set; }
        public string CardSeri { get; set; }
        public string CardName { get; set; }
        public string Status { get; set; }
        public string BankName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal? DecreasePercent { get; set; }
        public decimal? DecreaseFee { get; set; }
        public decimal? RemainFee { get; set; }

        [Required(ErrorMessage="Họ tên bắt buộc nhập!")]
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "SĐT bắt buộc nhập!")]
        public string Phone { get; set; }
    }
}