using opensis.core.helper;
using opensis.core.School.Interfaces;
using opensis.data.Interface;
using opensis.data.ViewModels.Membership;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.School.Services
{
    public class MembershipService : IMembershipService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";
        public IMembershipRepository membershipRepository;
        public MembershipService(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }
        public GetAllMembersList GetAllMembersForNotice(GetAllMembersList allMembersList)
        {
            GetAllMembersList getAllMembers = new GetAllMembersList();

            if (TokenManager.CheckToken(allMembersList._tenantName, allMembersList._token))
            {
                getAllMembers = this.membershipRepository.GetAllMemberList(allMembersList);
                return getAllMembers;
            }
            else
            {
                getAllMembers._failure = true;
                getAllMembers._message = TOKENINVALID;
                return getAllMembers;
            }
        }
    }
}
