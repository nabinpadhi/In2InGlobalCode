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
    public class ProjectMasterBL
    {
        /// <summary>
        /// This function is used to insert user information
        /// </summary>
        public ProjectDto SaveProjectMaster(ProjectEntity projectEntity)
        {
            ProjectDto response = null;
            ProjectMasterDL projectMasterDL = new ProjectMasterDL();

            if (projectEntity == null) return response;

            var varProjectId = projectMasterDL.SaveProjectMaster(projectEntity);

            response = new ProjectDto
            {
                ProjectId = varProjectId
            };
            return response;
        }


        /// <summary>
        /// This function is used to insert user information
        /// </summary>
        public ProjectDto UpdateProjectMaster(ProjectEntity projectEntity)
        {
            ProjectDto response = null;
            ProjectMasterDL projectMasterDL = new ProjectMasterDL();

            if (projectEntity == null) return response;

            var varProjectId = projectMasterDL.UpdateProjectMaster(projectEntity);

            response = new ProjectDto
            {
                ProjectId = varProjectId
            };
            return response;
        }

        /// <summary>
        /// This function is used to insert user information
        /// </summary>
        public ProjectDto DeleteProjectMaster(ProjectEntity projectEntity)
        {
            ProjectDto response = null;
            ProjectMasterDL projectMasterDL = new ProjectMasterDL();

            if (projectEntity == null) return response;

            var varProjectId = projectMasterDL.DeleteProjectMaster(projectEntity);

            response = new ProjectDto
            {
                ProjectId = varProjectId
            };
            return response;
        }






        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getProjectGridDetails(string userRole, string userEmail) 
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getProjectGridDetails(userRole, userEmail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }

        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getAssignedProject(string userRoll,string userEmail)
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getAssignedProject(userRoll,userEmail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }

        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getAssignedProjects(string userEmail)
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getAssignedProjects(userEmail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }




        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getProjectNameForDashboard(string userRole, string userEmail)
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getProjectNameForDashboard(userRole, userEmail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }



        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getProjectId()
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getProjectId();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }


        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getEmailforAdminAndUser(string roleId, string useEMail)
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getEmailforAdminAndUser(roleId, useEMail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }

        public DataSet getTemplateInfoForProjectId(ProjectEntity projectEntity)
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getTemplateInfoForProjectId(projectEntity);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }
    }
}
