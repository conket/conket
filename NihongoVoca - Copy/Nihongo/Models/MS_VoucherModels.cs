using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ivs.Core.Common;

namespace Nihongo.Models
{
    public class MS_VoucherModels
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        public int RemainDays
        {
            get
            {
                if (EffectiveEndDate.HasValue)
                {
                    return (EffectiveEndDate.Value - DateTime.Now).Days + 1;
                }

                return 0;
            }
        }
        public decimal? DecreaseFee { get; set; }
        public string DecreaseFeeDisplay
        {
            get
            {
                if (DecreaseFee.HasValue)
                {
                    return CommonMethod.ParseDecimal(DecreaseFee).ToString("N0");
                }
                return string.Empty;
            }
        }

        public decimal? DecreasePercent { get; set; }
        public string DecreasePercentDisplay
        {
            get
            {
                if (DecreasePercent.HasValue)
                {
                    return CommonMethod.ParseDecimal(DecreasePercent).ToString("N0");
                }
                return string.Empty;
            }
        }

        public decimal? RemainFee { get; set; }
        public string RemainFeeDisplay
        {
            get
            {
                if (RemainFee.HasValue)
                {
                    return CommonMethod.ParseDecimal(RemainFee).ToString("N0");
                }
                return string.Empty;
            }
        }

        public string Status { get; set; }
        public string VocaSetCode { get; set; }
        public decimal? VocaSetFee { get; set; }
    }
}