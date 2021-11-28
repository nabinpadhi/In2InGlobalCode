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
    /// <summary>
    /// CompanyMasterBL
    /// </summary>
    public class CompanyMasterBL
    {
        /// <summary>
        /// get Company Details
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyDetails()
        {
            DataSet dsobjcompanydetails = new DataSet();
            try
            {
                CompanyMasterDL objcompanydetails = new CompanyMasterDL();
                dsobjcompanydetails = objcompanydetails.getCompanyDetails();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsobjcompanydetails;
        }

        /// <summary>
        /// get Company Name
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyName()
        {
            DataSet dsobjcompanyname = new DataSet();
            try
            {
                CompanyMasterDL objcompanyname = new CompanyMasterDL();
                dsobjcompanyname = objcompanyname.getCompanyName();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsobjcompanyname;
        }

        /// <summary>
        /// Save Company Master
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        public CompanyDto SaveCompanyMaster(CompanyEntity companyEntity)
        {
            CompanyDto response = null;
            CompanyMasterDL companyMasterDL = new CompanyMasterDL();

            if (companyEntity == null) return response;

            var varCompanyId = companyMasterDL.SaveCompanyMaster(companyEntity);

            response = new CompanyDto
            {
                CompanyId = varCompanyId
            };           

            return response;         
        }

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        public CompanyDto UpdateCompany(CompanyEntity companyEntity) 
        {
            CompanyDto response = null;
            CompanyMasterDL companyMasterDL = new CompanyMasterDL();

            if (companyEntity == null) return response;

            var varCompanyId = companyMasterDL.UpdateCompany(companyEntity);

            response = new CompanyDto
            {
                CompanyId = varCompanyId
            };

            return response;
        }

        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        public CompanyDto DeleteCompany(CompanyEntity companyEntity)
        {
            CompanyDto response = null;
            CompanyMasterDL companyMasterDL = new CompanyMasterDL();

            if (companyEntity == null) return response;

            var varCompanyId = companyMasterDL.DeleteCompany(companyEntity);

            response = new CompanyDto
            {
                CompanyId = varCompanyId
            };

            return response;
        }


    }
}
