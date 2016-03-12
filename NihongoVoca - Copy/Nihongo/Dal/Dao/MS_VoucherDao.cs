using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_VoucherDao : BaseDao
    {
        internal int SelectData(MS_VoucherModels model, out MS_VoucherModels result)
        {
            int returnCode = 0;
            result = new MS_VoucherModels();
            try
            {
                var query = this.ms_vouchers.AsQueryable();
                if (model.ID > 0)
                {
                    query = query.Where(ss => ss.ID == model.ID);
                }
                if (!CommonMethod.IsNullOrEmpty(model.Code))
                {
                    query = query.Where(ss => ss.Code == model.Code);
                }
                if (!CommonMethod.IsNullOrEmpty(model.VocaSetID))
                {
                    query = query.Where(ss => ss.VocaSetID == model.VocaSetID);
                }
                result = query
                    .Select(ss => new MS_VoucherModels
                    {
                        ID = ss.ID,
                        Code = ss.Code,
                        EffectiveEndDate = ss.EffectiveEndDate,
                        EffectiveStartDate = ss.EffectiveStartDate,
                        DecreaseFee = ss.DecreaseFee,
                        DecreasePercent = ss.DecreasePercent,
                        RemainFee = ss.RemainFee,
                        Status = ss.Status,
                        VocaSetID = ss.VocaSetID,
                        VocaSetFee = ss.VocaSetFee
                    })
                    .FirstOrDefault();

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int CheckValidVoucher(MS_VoucherModels model, out bool isOK, out bool isUsed, out MS_VoucherModels result)
        {
            int returnCode = 0;
            isOK = false;
            isUsed = false;
            result = new MS_VoucherModels();
            try
            {
                if (!this.ms_paymenthistories.Any(ss => ss.VoucherID == model.ID && ss.UserID == model.UserID))
                {
                    var query = this.ms_vouchers.AsQueryable();
                    if (model.ID > 0)
                    {
                        query = query.Where(ss => ss.ID == model.ID);
                    }
                    if (!CommonMethod.IsNullOrEmpty(model.Code))
                    {
                        query = query.Where(ss => ss.Code == model.Code);
                    }
                    if (!CommonMethod.IsNullOrEmpty(model.VocaSetID))
                    {
                        query = query.Where(ss => ss.VocaSetID == model.VocaSetID);
                    }
                    result = query
                        .Select(ss => new MS_VoucherModels
                        {
                            ID = ss.ID,
                            Code = ss.Code,
                            EffectiveEndDate = ss.EffectiveEndDate,
                            EffectiveStartDate = ss.EffectiveStartDate,
                            DecreaseFee = ss.DecreaseFee,
                            DecreasePercent = ss.DecreasePercent,
                            RemainFee = ss.RemainFee,
                            Status = ss.Status,
                            VocaSetID = ss.VocaSetID,
                            VocaSetFee = ss.VocaSetFee
                        })
                        .FirstOrDefault();

                    if (result != null && result.RemainDays > 0)
                    {
                        isOK = true;
                    }
                }
                else
                {
                    isUsed = true;
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }
    }
}