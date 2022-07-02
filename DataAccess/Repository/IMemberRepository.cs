using BusinessObject;
using System.Collections.Generic;
using DataAccess.Repository;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByMemberID(string memberID);
        void AddNewMember(MemberObject addMem);
        void UpdateAMember(MemberObject updateMem);
        void RemoveAMember(string memberID);
        bool CheckUpdateEmailDuplicated(string userID, string email);

        MemberObject GetMemberByEmail(string email);

        IEnumerable<MemberObject> SearchMemberByName(string memberName);
        IEnumerable<MemberObject> SearchMemberByID(string memberID);

        IEnumerable<MemberObject> FilterMemberByCity(string city);
        IEnumerable<MemberObject> FilterMemberByCountry(string country);


    }
}
