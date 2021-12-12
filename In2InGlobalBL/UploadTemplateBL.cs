using In2InGlobal.datalink;
using In2InGlobalBusinessEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobalBL
{
    /// <summary>
    /// Upload Template BL
    /// </summary>
    public class UploadTemplateBL
    {
        /// <summary>
        /// Save Assigned Template
        /// </summary>
        /// <param name="tempEntity"></param>
        /// <param name="uploadTemplate"></param>
        /// <returns></returns>
        public UploadTemplateDto SaveAssignedTemplate(UploadTemplateEntity tempEntity)
        {
            UploadTemplateDto response = null;
            UploadTemplateDL tempuploadDL = new UploadTemplateDL();

            if (tempEntity == null) return response;

            var varTemplateId = tempuploadDL.SaveUploadTemplate(tempEntity);

            response = new UploadTemplateDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }
        public UploadTemplateDto SaveUploadTemplate(DataTable dtUploadTemplate, UploadTemplateEntity uploadTemplateEntity)
        {
            UploadTemplateDto response = null;
            UploadTemplateDL tempuploadDL = new UploadTemplateDL();

            if (uploadTemplateEntity == null) return response;

            var varTemplateId = tempuploadDL.SaveUploadTemplate(dtUploadTemplate, uploadTemplateEntity);

            response = new UploadTemplateDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }
        public UploadTemplateDto UpdateAssignedTemplate(UploadTemplateEntity tempEntity)
        {
            UploadTemplateDto response = null;
            UploadTemplateDL tempuploadDL = new UploadTemplateDL();

            if (tempEntity == null) return response;

            var varTemplateId = tempuploadDL.UpdateUploadTemplate(tempEntity);

            response = new UploadTemplateDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }



        /// <summary>
        /// Load Project Name For Template
        /// </summary>
        /// <returns></returns>
        public DataSet LoadProjectNameForTemplate()
        {
            DataSet dsUplaodProject = new DataSet();
            try
            {
                UploadTemplateDL objUploadProject = new UploadTemplateDL();
                dsUplaodProject = objUploadProject.LoadProjectNameForTemplate();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsUplaodProject;
        }


        /// <summary>
        /// Load Project Name For Template
        /// </summary>
        /// <returns></returns>
        public DataSet CreateTableForMasterTemplate(string queryTable)
        {
            DataSet dsUplaodProject = new DataSet();
            try
            {
                UploadTemplateDL objUploadProject = new UploadTemplateDL();
                dsUplaodProject = objUploadProject.CreateTableForMasterTemplate(queryTable);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsUplaodProject;
        }


        public DataSet LoadUploadFileTemplateGrid(string userRole, string userEmail, int pid)
        {
            DataSet dsUplaodProject = new DataSet();
            try
            {
                UploadTemplateDL objUploadProject = new UploadTemplateDL();
                dsUplaodProject = objUploadProject.LoadUploadFileTemplateGrid(userRole, userEmail, pid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsUplaodProject;
        }

        public DataSet LoadSearchTemplateGrid(string userRole, string userEmail, int pid)
        {
            DataSet dsUplaodProject = new DataSet();
            try
            {
                UploadTemplateDL objUploadProject = new UploadTemplateDL();
                dsUplaodProject = objUploadProject.LoadSearchTemplateGrid(userRole, userEmail, pid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsUplaodProject;
        }





        /// <summary>
        /// Load All User Email For Assigned Project
        /// </summary>
        /// <param name="projectid"></param> 
        /// <returns></returns>
        public DataSet LoadAllUserEmailForAssignedProject(long projectid)
        {
            DataSet dsUplaodUser = new DataSet();
            try
            {
                UploadTemplateDL objUploadUser = new UploadTemplateDL();
                dsUplaodUser = objUploadUser.LoadAllUserEmailForNotAssignedProject(projectid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsUplaodUser;
        }


        /// <summary>
        /// Populate TemplateName For Assigned Project  And User
        /// </summary>
        /// <returns></returns>
        public DataSet LoadTemplateForNotAssignedProjectAndUser(long projectid, string userid)
        {
            DataSet dsUplaodTemplate = new DataSet();
            try
            {
                UploadTemplateDL objUploadTemplate = new UploadTemplateDL();
                dsUplaodTemplate = objUploadTemplate.LoadTemplateForNotAssignedProjectAndUser(projectid, userid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsUplaodTemplate;
        }

    }
}
