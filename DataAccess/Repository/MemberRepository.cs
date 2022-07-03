using BusinessObject;
using DataAccess.Repository;
using System.Collections.Generic;


namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void AddNewMember(MemberObject addMem) => MemberDAO.Instance.AddNewMember(addMem);

        public IEnumerable<MemberObject> GetMembers() => MemberDAO.Instance.GetMemberObjectsList();

        public MemberObject GetMemberByMemberID(string memberID) => MemberDAO.Instance.GetMemberByID(memberID);

        public void RemoveAMember(string memberID) => MemberDAO.Instance.RemoveAMember(memberID);

        public void UpdateAMember(MemberObject updateMem) => MemberDAO.Instance.UpdateAMember(updateMem);

        public MemberObject GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);

        public IEnumerable<MemberObject> SearchMemberByName(string memberName) => MemberDAO.Instance.SearchMemberByName(memberName);
        public IEnumerable<MemberObject> SearchMemberByID(string memberID) => MemberDAO.Instance.SearchMemberByID(memberID);

        public IEnumerable<MemberObject> FilterMemberByCity(string city) => MemberDAO.Instance.FilterMemberByCity(city);

        public IEnumerable<MemberObject> FilterMemberByCountry(string country) => MemberDAO.Instance.FilterMemberByCountry(country);

        public bool CheckUpdateEmailDuplicated(string userID, string email) => MemberDAO.Instance.CheckUpdateEmailDuplicated(userID, email);
    }
}
