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
    /// Assigned Template BAL
    /// </summary>
    public class AssignedTemplateBL
    {
        /// <summary>
        /// Populate Project Name For Template
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateProjectNameForTemplate(string userrole, string useremail)
        {
            DataSet dsTemplategrid = new DataSet();
            try
            {
                AssignedTemplateDL objTemplategrid = new AssignedTemplateDL();
                dsTemplategrid = objTemplategrid.PopulateProjectNameForTemplate(userrole, useremail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsTemplategrid;
        }

        /// <summary>
        /// Populate All User Email For Assigned Project
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public DataSet PopulateAllUserEmailForAssignedProject(long projectid)
        {
            DataSet dsTemplateProject = new DataSet();
            try
            {
                AssignedTemplateDL objTemplateProject = new AssignedTemplateDL();
                dsTemplateProject = objTemplateProject.PopulateAllUserEmailForAssignedProject(projectid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsTemplateProject;
        }


        /// <summary>
        /// Populate TemplateName For Assigned Project  And User
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateTemplateNameForAssignedProjectAndUser(long projectid, long userid)
        {
            DataSet dsTemplategrid = new DataSet();
            try
            {
                AssignedTemplateDL objTemplategrid = new AssignedTemplateDL();
                dsTemplategrid = objTemplategrid.PopulateTemplateNameForAssignedProjectAndUser(projectid, userid);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsTemplategrid;
        }

        /// <summary>
        /// Save Assigned Template
        /// </summary>
        /// <param name="templateMasterEntity"></param>
        /// <returns></returns>
        public AssignedTemplateDto SaveAssignedTemplate(AssignedTemplateEntity templateMasterEntity)
        {
            AssignedTemplateDto response = null;
            AssignedTemplateDL templateAssignedDL = new AssignedTemplateDL();

            if (templateMasterEntity == null) return response;

            var varTemplateId = templateAssignedDL.SaveAssignedTemplate(templateMasterEntity);

            response = new AssignedTemplateDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }

        /// <summary>
        /// Populate All Assigned Template Grid
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateAllAssignedTemplateGrid()
        {
            DataSet dsAssignedTemplategrid = new DataSet();
            try
            {
                AssignedTemplateDL objAssignedTemplategrid = new AssignedTemplateDL();
                dsAssignedTemplategrid = objAssignedTemplategrid.PopulateAllAssignedTemplateGrid();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsAssignedTemplategrid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateMasterEntity"></param>
        /// <returns></returns>
        public AssignedTemplateDto DeleteAssignedTemplate(AssignedTemplateEntity templateMasterEntity)
        {
            AssignedTemplateDto response = null;
            AssignedTemplateDL templateAssignedDL = new AssignedTemplateDL();

            if (templateMasterEntity == null) return response;

            var varTemplateId = templateAssignedDL.DeleteAssignedTemplate(templateMasterEntity);

            response = new AssignedTemplateDto
            {
                TemplateId = varTemplateId
            };
            return response;
        }

        /// <summary>
        /// Create Project
        /// </summary>
        /// <param name="projectEntity"></param>
        /// <returns></returns>
        public ProjectDto CreateProject(ProjectEntity projectEntity) 
        {
            ProjectDto response = null;
            AssignedTemplateDL templateAssignedDL = new AssignedTemplateDL();

            if (projectEntity == null) return response;

            var varProjectId = templateAssignedDL.CreateProject(projectEntity); 

            response = new ProjectDto
            {
                ProjectId = varProjectId
            };
            return response;
        }

        /// <summary>
        /// Populate Project Grid
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateCreateProjectGrid() 
        {
            DataSet dsPopulateProjectGrid = new DataSet(); 
            try
            {
                AssignedTemplateDL objAssignedTemplategrid = new AssignedTemplateDL();
                dsPopulateProjectGrid = objAssignedTemplategrid.PopulateCreateProjectGrid();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsPopulateProjectGrid; 
        }
    }
}
