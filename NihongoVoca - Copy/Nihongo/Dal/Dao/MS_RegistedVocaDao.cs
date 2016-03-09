using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_RegistedVocaDao : BaseDao
    {
        internal int SelectDataByModel(MS_RegistedVocaModels model, out MS_RegistedVocaModels result)
        {
            int returnCode = 0;
            result = new MS_RegistedVocaModels();
            try
            {
                var query = this.ms_registedvocasets.AsQueryable();
                if (model.VocaSetID > 0)
                {
                    query = query.Where(ss => ss.VocaSetID == model.VocaSetID);
                }
                if (!CommonMethod.IsNullOrEmpty(model.UserName))
                {
                    query = query.Where(ss => ss.UserName == model.UserName);
                }

                var vocaSet = this.ms_vocasets.Where(ss => ss.ID == model.VocaSetID);
                result = (from re in query
                         join se in vocaSet on re.VocaSetID equals se.ID
                         select new MS_RegistedVocaModels
                             {
                                 VocaSetID = se.ID,
                                 VocaSetName1 = se.Name1,
                                 VocaSetUrlDisplay = se.UrlDisplay,
                                 Fee = se.Fee,
                                 StartDate = re.StartDate,
                                 EndDate = re.EndDate,
                                 UsefulLife = se.UsefulLife,
                                 UserName = model.UserName,
                                 Status = re.Status,
                             })
                    .FirstOrDefault();

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }
    }
}