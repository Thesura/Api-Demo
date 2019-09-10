using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_API.Entities;

namespace Test_API.Services
{
    public interface IMemberData
    {
        IEnumerable<Member> GetMembers();

        void AddMember(Member member);

        Member GetMember(int id);

        bool Save();
    }
}
