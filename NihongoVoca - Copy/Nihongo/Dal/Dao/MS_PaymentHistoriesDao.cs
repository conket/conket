using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_PaymentHistoriesDao : BaseDao
    {
        public int Process(MS_PaymentHistoriesModels model)
        {
            int returnCode = 0;

            try
            {
                this.BeginTransaction();

                #region save payment
                Nihongo.Dal.Mapping.ms_paymenthistories payment = new Mapping.ms_paymenthistories()
                {
                    UserName = model.UserName,
                    VocaSetID = model.VocaSetID,
                    VoucherCode = model.VoucherCode,
                    Fee = CommonMethod.ParseDecimal(model.RemainFee),
                    PaymentDate = DateTime.Now,
                    PaymentMethod = model.PaymentMethod,
                    ReceivedDate = DateTime.Now,

                    CardCode = model.CardCode,
                    CardName = model.CardName,
                    CardSeri = model.CardSeri,

                    FullName = model.FullName,
                    Email = model.Email,
                    Address = model.Address,
                    BankName = model.BankName,
                    Phone = model.Phone,

                    Description = model.Description,
                    Status = CommonData.Status.Enable,
                };
                ms_paymenthistories.AddObject(payment);
                #endregion

                #region update registed 

                var vocaSet = this.ms_vocasets.FirstOrDefault(ss => ss.ID == model.VocaSetID);
                var registed = this.ms_registedvocasets.FirstOrDefault(ss => ss.VocaSetID == model.VocaSetID && ss.UserName == model.UserName);
                if (registed == null)
                {
                    registed = new Mapping.ms_registedvocasets()
                    {
                        UserName = model.UserName,
                        VocaSetID = model.VocaSetID,
                        StartDate = DateTime.Now.Date,
                        EndDate = DateTime.Now.Date.AddMonths(CommonMethod.ParseInt(vocaSet.UsefulLife)),
                        NumOfDays = (DateTime.Now.Date.AddMonths(CommonMethod.ParseInt(vocaSet.UsefulLife)) - DateTime.Now.Date).Days + 1,
                        NumOfMonths = CommonMethod.ParseInt(vocaSet.UsefulLife),
                        Status = CommonData.Status.Enable,
                        Description = CommonData.StringEmpty,
                    };
                    ms_registedvocasets.AddObject(registed);
                }
                else
                {
                    //registed.StartDate = DateTime.Now.Date;
                    var startDate = CommonMethod.ParseDate(registed.StartDate);
                    var endDate = registed.EndDate >= DateTime.Now 
                        ? registed.EndDate.Value.AddMonths(CommonMethod.ParseInt(vocaSet.UsefulLife))
                        : DateTime.Now.AddMonths(CommonMethod.ParseInt(vocaSet.UsefulLife));

                    registed.StartDate = startDate;
                    registed.EndDate = endDate;
                    registed.NumOfDays = (endDate - startDate).Days + 1;
                    registed.NumOfMonths = ((endDate - startDate).Days + 1) / 30;
                    registed.Status = CommonData.Status.Enable;
                }

                #region update vocaset

                vocaSet.NumOfRegistedPerson = CommonMethod.ParseInt(vocaSet.NumOfRegistedPerson) + 1;

                #endregion

                #endregion

                #region Update user vocas



                #endregion

                returnCode = this.Saves();

                if (returnCode == CommonData.DbReturnCode.Succeed)
                {
                    returnCode = this.Commit();
                }
                else
                {
                    this.Rollback();
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                returnCode = this.ProcessDbException(ex);
            }

            return returnCode;
        }
    }
}