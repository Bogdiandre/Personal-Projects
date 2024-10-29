using DALLibrary;
using DomainLibrary.Domains;
using LogicLibrary.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DTO;
using System.Reflection.Metadata.Ecma335;
using DomainLibrary.Exceptions;

namespace WebsiteApplication.Pages
{
    [Authorize]
    public class MyNormalRequestsModel : PageModel
    {
        [BindProperty]
        public List<NormalRequest> normalRequestList { get; set; }
        public readonly NormalRequestManager normalRequestManager;
        public void OnGet()
        {    try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                normalRequestList = normalRequestManager.GetNormalRequestByUserID(userId);
            }
            catch (NormalRequestException x) { TempData["Message"] = x.Message; }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!"; }
        }
        public MyNormalRequestsModel()
        {
            normalRequestManager = new NormalRequestManager(new NormalRequestDAL());
        }
    }
}
