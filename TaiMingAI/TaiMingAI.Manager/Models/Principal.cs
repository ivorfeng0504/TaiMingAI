using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.Models
{
    public class Principal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public AdministratorDto Account { get; set; }

        public Principal(FormsAuthenticationTicket ticket, AdministratorDto account)
        {
            Identity = new FormsIdentity(ticket);
            Account = account;
        }
        public bool IsInRole(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles)) return true;

            if (Account == null || string.IsNullOrEmpty(Account.Role)) return false;
            var accountRoles = Account.Role.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (accountRoles.Count() == 0) return false;
            if (accountRoles.Contains(RoleEnum.Admin.ToString())) return true;

            var rolesArr = roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return accountRoles.Intersect(rolesArr).Count() > 0;
        }
    }
}