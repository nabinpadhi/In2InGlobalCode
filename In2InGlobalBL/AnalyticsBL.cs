using In2InGlobal.datalink;
using In2InGlobalBusinessEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.businesslogic
{
    public class AnalyticsBL
    {
        /// <summary>
        /// get Company Name
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyName()
        {
            DataSet dsobjcompanyname = new DataSet();
            try
            {
                AnalyticsDL objcompanyname = new AnalyticsDL();
                dsobjcompanyname = objcompanyname.getCompanyName();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsobjcompanyname;
        }

        /// <summary>
        /// getUserEmailByCompany
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public DataSet getUserEmailByCompany(long companyid)
        {
            DataSet dsobjcompanyname = new DataSet();
            try
            {
                AnalyticsDL analyticsDL = new AnalyticsDL();
                dsobjcompanyname = analyticsDL.getUserEmailByCompany(companyid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsobjcompanyname;
        }

        /// <summary>
        /// getProjectNameByUserEmail
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="useremail"></param>
        /// <returns></returns>
        public DataSet getProjectNameByUserEmail(int companyid, string useremail)
        {
            DataSet dsobjcompanyname = new DataSet();
            try
            {
                AnalyticsDL analyticsDL = new AnalyticsDL();
                dsobjcompanyname = analyticsDL.getProjectNameByUserEmail(companyid, useremail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsobjcompanyname;
        }

        /// <summary>
        /// SaveAnalyticConfiguration
        /// </summary>
        /// <param name="analyticsEntity"></param>
        /// <returns></returns>
        public AnalyticsDto SaveAnalyticConfiguration(AnalyticsEntity analyticsEntity)
        {
            AnalyticsDto response = null;
            AnalyticsDL analyticsDL = new AnalyticsDL();

            if (analyticsEntity == null) return response;

            var varId = analyticsDL.SaveAnalyticConfiguration(analyticsEntity);

            response = new AnalyticsDto
            {
                Id = varId
            };

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet getAnalyticsGridDetails()
        {
            DataSet dsanalyticsgrid = new DataSet();
            try
            {
                AnalyticsDL objcompanyname = new AnalyticsDL();
                dsanalyticsgrid = objcompanyname.getAnalyticsGridDetails();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsanalyticsgrid;
        }

        /// <summary>
        /// UpdateAnalyticConfiguration
        /// </summary>
        /// <param name="analyticsEntity"></param>
        /// <returns></returns>
        public AnalyticsDto UpdateAnalyticConfiguration(AnalyticsEntity analyticsEntity)
        {
            AnalyticsDto response = null;
            AnalyticsDL companyMasterDL = new AnalyticsDL();

            if (analyticsEntity == null) return response;

            var varId = companyMasterDL.UpdateAnalyticConfiguration(analyticsEntity);

            response = new AnalyticsDto
            {
                Id = varId
            };

            return response;
        }

        /// <summary>
        /// DeleteAnalyticConfiguration
        /// </summary>
        /// <param name="analyticsEntity"></param>
        /// <returns></returns>
        public AnalyticsDto DeleteAnalyticConfiguration(AnalyticsEntity analyticsEntity)
        {
            AnalyticsDto response = null;
            AnalyticsDL analyticsDL = new AnalyticsDL();

            if (analyticsEntity == null) return response;

            var varId = analyticsDL.DeleteAnalyticConfiguration(analyticsEntity);

            response = new AnalyticsDto
            {
                Id = varId
            };

            return response;
        }
    }
}
