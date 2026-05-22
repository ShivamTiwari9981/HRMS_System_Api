using HRMS.Shared.Helpers;
using System.ComponentModel.DataAnnotations;


namespace HRMS.Infrastructure.Persistence.Seeders.Constants
{
    public static class MenuConstants
    {
        public static readonly List<MenuSeedModel> Menus = new()
        {
            //---------------------------------------------------------
            // DASHBOARD
            //---------------------------------------------------------

            new MenuSeedModel
            {
                MenuName = "Dashboard",
                MenuIcon = "dashboard",
                RouterLink = "/dashboard",
                MenuType = "MainMenu",
                DisplayOrder = 1
            },


           //---------------------------------------------------------
            // COMPANY MANAGEMENT
            //---------------------------------------------------------

            new MenuSeedModel
            {
                MenuName = "Company Management",
                MenuIcon = "business",
                MenuType = "MainMenu",
                DisplayOrder = 1,

                Children = new List<MenuSeedModel>
                {
                    new()
                    {
                        MenuName = "Company Profile",
                        MenuIcon = "domain",
                        RouterLink = "/company/profile",
                        MenuType = "SubMenu",
                        DisplayOrder = 1
                    },

                    new()
                    {
                        MenuName = "Branch",
                        MenuIcon = "apartment",
                        RouterLink = "/branch/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 2
                    }
                }
            },

            //---------------------------------------------------------
            // MASTER MANAGEMENT
            //---------------------------------------------------------

            new MenuSeedModel
            {
                MenuName = "Master Management",
                MenuIcon = "settings_applications",
                MenuType = "MainMenu",
                DisplayOrder = 2,

                Children = new List<MenuSeedModel>
                {
                    new()
                    {
                        MenuName = "Department",
                        MenuIcon = "account_tree",
                        RouterLink = "/department/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 1
                    },

                    new()
                    {
                        MenuName = "Designation",
                        MenuIcon = "badge",
                        RouterLink = "/designation/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 2
                    },

                    new()
                    {
                        MenuName = "Shift",
                        MenuIcon = "schedule",
                        RouterLink = "/shift/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 3
                    },

                    new()
                    {
                        MenuName = "Leave Type",
                        MenuIcon = "event_busy",
                        RouterLink = "/leave-type/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 4
                    }
                }
            },

            //---------------------------------------------------------
            // EMPLOYEE MANAGEMENT
            //---------------------------------------------------------

            new MenuSeedModel
            {
                MenuName = "Employee Management",
                MenuIcon = "groups",
                MenuType = "MainMenu",
                DisplayOrder = 3,

                Children = new List<MenuSeedModel>
                {
                    new()
                    {
                        MenuName = "Employees",
                        MenuIcon = "badge",
                        RouterLink = "/employee/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 1
                    },

                    new()
                    {
                        MenuName = "Shift Management",
                        MenuIcon = "manage_accounts",
                        RouterLink = "/employee-shift/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 2
                    }
                }
            },

            //---------------------------------------------------------
            // ACCESS CONTROL
            //---------------------------------------------------------

            new MenuSeedModel
            {
                MenuName = "Access Control",
                MenuIcon = "admin_panel_settings",
                MenuType = "MainMenu",
                DisplayOrder = 4,

                Children = new List<MenuSeedModel>
                {
                    new()
                    {
                        MenuName = "Users",
                        MenuIcon = "group",
                        RouterLink = "/user/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 1
                    },

                    new()
                    {
                        MenuName = "Roles",
                        MenuIcon = "security",
                        RouterLink = "/role/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 2
                    },

                    new()
                    {
                        MenuName = "Permissions",
                        MenuIcon = "verified_user",
                        RouterLink = "/permission/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 3
                    },

                    new()
                    {
                        MenuName = "Menu Permissions",
                        MenuIcon = "menu_open",
                        RouterLink = "/menu-permission/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 4
                    },

                    new()
                    {
                        MenuName = "Role Menu Mapping",
                        MenuIcon = "account_tree",
                        RouterLink = "/role-menu-mapping/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 5
                    },

                    new()
                    {
                        MenuName = "User Role Mapping",
                        MenuIcon = "supervised_user_circle",
                        RouterLink = "/user-role-mapping/list",
                        MenuType = "SubMenu",
                        DisplayOrder = 6
                    }
                }
            } ,
            //---------------------------------------------------------
            // ATTENDANCE
            //---------------------------------------------------------

            //new MenuSeedModel
            //{
            //    MenuName = "Attendance",
            //    MenuIcon = "calendar_month",
            //    MenuType = "MainMenu",
            //    DisplayOrder = 4,

            //    Children = new List<MenuSeedModel>
            //    {
            //        new()
            //        {
            //            MenuName = "Daily Attendance",
            //            MenuIcon = "fact_check",
            //            RouterLink = "/attendance/list",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 1
            //        },

            //        new()
            //        {
            //            MenuName = "Leave Request",
            //            MenuIcon = "event_busy",
            //            RouterLink = "/leave/list",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 2
            //        },

            //        new()
            //        {
            //            MenuName = "Holiday Calendar",
            //            MenuIcon = "event",
            //            RouterLink = "/holiday/list",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 3
            //        },

            //        new()
            //        {
            //            MenuName = "Attendance Report",
            //            MenuIcon = "analytics",
            //            RouterLink = "/attendance/report",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 4
            //        }
            //    }
            //},

            ////---------------------------------------------------------
            //// PAYROLL
            ////---------------------------------------------------------

            //new MenuSeedModel
            //{
            //    MenuName = "Payroll",
            //    MenuIcon = "payments",
            //    MenuType = "MainMenu",
            //    DisplayOrder = 5,

            //    Children = new List<MenuSeedModel>
            //    {
            //        new()
            //        {
            //            MenuName = "Salary Structure",
            //            MenuIcon = "currency_rupee",
            //            RouterLink = "/salary/structure",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 1
            //        },

            //        new()
            //        {
            //            MenuName = "Generate Payroll",
            //            MenuIcon = "account_balance_wallet",
            //            RouterLink = "/payroll/generate",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 2
            //        },

            //        new()
            //        {
            //            MenuName = "Payslip",
            //            MenuIcon = "receipt_long",
            //            RouterLink = "/payroll/payslip",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 3
            //        }
            //    }
            //},

            ////---------------------------------------------------------
            //// RECRUITMENT
            ////---------------------------------------------------------

            //new MenuSeedModel
            //{
            //    MenuName = "Recruitment",
            //    MenuIcon = "work",
            //    MenuType = "MainMenu",
            //    DisplayOrder = 6,

            //    Children = new List<MenuSeedModel>
            //    {
            //        new()
            //        {
            //            MenuName = "Job Opening",
            //            MenuIcon = "business_center",
            //            RouterLink = "/recruitment/jobs",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 1
            //        },

            //        new()
            //        {
            //            MenuName = "Candidates",
            //            MenuIcon = "group",
            //            RouterLink = "/recruitment/candidates",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 2
            //        },

            //        new()
            //        {
            //            MenuName = "Interview Schedule",
            //            MenuIcon = "event_note",
            //            RouterLink = "/recruitment/interviews",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 3
            //        }
            //    }
            //},

           

            ////---------------------------------------------------------
            //// REPORTS
            ////---------------------------------------------------------

            //new MenuSeedModel
            //{
            //    MenuName = "Reports",
            //    MenuIcon = "bar_chart",
            //    MenuType = "MainMenu",
            //    DisplayOrder = 8,

            //    Children = new List<MenuSeedModel>
            //    {
            //        new()
            //        {
            //            MenuName = "Employee Report",
            //            MenuIcon = "summarize",
            //            RouterLink = "/reports/employee",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 1
            //        },

            //        new()
            //        {
            //            MenuName = "Attendance Report",
            //            MenuIcon = "analytics",
            //            RouterLink = "/reports/attendance",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 2
            //        },

            //        new()
            //        {
            //            MenuName = "Payroll Report",
            //            MenuIcon = "insert_chart",
            //            RouterLink = "/reports/payroll",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 3
            //        }
            //    }
            //},

            ////---------------------------------------------------------
            //// SETTINGS
            ////---------------------------------------------------------

            //new MenuSeedModel
            //{
            //    MenuName = "Settings",
            //    MenuIcon = "settings",
            //    MenuType = "MainMenu",
            //    DisplayOrder = 9,

            //    Children = new List<MenuSeedModel>
            //    {
            //        new()
            //        {
            //            MenuName = "General Settings",
            //            MenuIcon = "tune",
            //            RouterLink = "/settings/general",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 1
            //        },

            //        new()
            //        {
            //            MenuName = "Email Settings",
            //            MenuIcon = "email",
            //            RouterLink = "/settings/email",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 2
            //        },

            //        new()
            //        {
            //            MenuName = "Notification Settings",
            //            MenuIcon = "notifications",
            //            RouterLink = "/settings/notification",
            //            MenuType = "SubMenu",
            //            DisplayOrder = 3
            //        }
            //    }
            //}
        };
    }

    public class MenuSeedModel
    {
        public string MenuName { get; set; }

        public string? MenuIcon { get; set; }

        public string? RouterLink { get; set; }

        public string MenuType { get; set; }

        public int DisplayOrder { get; set; }

        public bool? IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedBy { get; set; } = SystemUser.DefaultSystemUser;


        public List<MenuSeedModel> Children { get; set; } = new();
    }
}
