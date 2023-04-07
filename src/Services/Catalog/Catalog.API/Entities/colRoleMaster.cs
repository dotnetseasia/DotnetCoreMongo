using CompanyAdmin.API.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities
{
    public class colRoleMaster : BaseEntity
    {
        public List<ModuleMaster> listModuleMaster { get; set; }
    }

    public class ModuleMaster
    {
        public string ModuleName { get; set; }
        public string ModuleId { get; set; }
        public int DisplayOrder { get; set; }
        public List<PageMaster> listPageMaster { get; set; }
    }

    public class PageMaster
    {
        public string PageName { get; set; }
        public string PageId { get; set; }
        public int DisplayOrder { get; set; }
        public List<TabMaster> listTabMaster { get; set; }
    }


    public class TabMaster
    {
        public string TabName { get; set; }
        public string TabId { get; set; }
        public int DisplayOrder { get; set; }
        public List<ActionMaster> listActionMaster { get; set; }
    }

    public class ActionMaster
    {
        public string ActionName { get; set; }
        public string ActionId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
