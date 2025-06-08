using SDKTemplate.Models;
using SDKTemplate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.ViewModels
{
    class PasswordViewModel
    {
        public string res { get; set; }
        public async Task ExecuteAddItemCommand(string AdminName, string AdminPassword)
        {
            await PasswordService.OnAdd(AdminName, AdminPassword);
        }
        public async Task ExecuteUpdatePresenceCommand(Password password)
        {
            await PasswordService.UpdatePasswordAsync(password);
        }

        public async Task ExecuteCheckPasswordCommand(Password password)
        {
            res = await PasswordService.CheckPassword(password);
        }
    }
}
