using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using opensis.core.School.Interfaces;
using opensis.data.ViewModels.Membership;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Membership")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private IMembershipService _membershipService;


        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }



        [HttpPost("getAllMembers")]
        public ActionResult<GetAllMembersList> GetAllMembers(GetAllMembersList allMembersList)
        {
            GetAllMembersList getAllMembers = new GetAllMembersList();
            try
            {
                getAllMembers = _membershipService.GetAllMembersForNotice(allMembersList);
            }
            catch (Exception es)
            {
                getAllMembers._failure = true;
                getAllMembers._message = es.Message;
            }
            return getAllMembers;
        }
    }
}
