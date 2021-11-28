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
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getProjectDetails() 
        {
            DataSet dsProject = new DataSet();
            try
            {
                ProjectMasterDL objProject = new ProjectMasterDL();
                dsProject = objProject.getProjectDetails();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsProject;
        }
        
    }
}
