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
    public class TemplateMasterBl
    {
        /// <summary>
        /// Save Project Master
        /// </summary>
        /// <param name="templateMasterEntity"></param>
        /// <returns></returns>
        public TemplateMasterDto SaveProjectMaster(TemplateMasterEntity templateMasterEntity)
        {
            TemplateMasterDto response = null;
            TemplateMasterDL templateMasterDL = new TemplateMasterDL(); 

            if (templateMasterEntity == null) return response;

            var varTemplateId = templateMasterDL.SaveTemplateMaster(templateMasterEntity);

            response = new TemplateMasterDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }

        /// <summary>
        /// Save Project Master
        /// </summary>
        /// <param name="templateMasterEntity"></param>
        /// <returns></returns>
        public TemplateMasterDto SaveTemplateMaster(TemplateMasterEntity templateMasterEntity)  
        {
            TemplateMasterDto response = null;
            TemplateMasterDL templateMasterDL = new TemplateMasterDL();

            if (templateMasterEntity == null) return response;

            var varTemplateId = templateMasterDL.SaveTemplateMaster(templateMasterEntity);

            response = new TemplateMasterDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }



        /// <summary>
        /// Populate Template Name
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateTemplateName()  
        {
            DataSet dsTemplate = new DataSet();
            try
            { 
                TemplateMasterDL objTemplate = new TemplateMasterDL();
                dsTemplate = objTemplate.PopulateTemplateName();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsTemplate;
        }

        /// <summary>
        /// Populate Template Grid
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateTemplateGrid()
        {
            DataSet dsTemplategrid = new DataSet();
            try
            {
                TemplateMasterDL objTemplategrid = new TemplateMasterDL();
                dsTemplategrid = objTemplategrid.PopulateTemplateGrid();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsTemplategrid;
        }
    }
}
