using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_API.Entities;

namespace Test_API.Services
{
    public class MemberData : IMemberData
    {
        private ApiContext _context;

        public MemberData(ApiContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
        }

        public Member GetMember(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Member> GetMembers()
        {
            return _context.Members.ToList();
        }

        public bool MemberExists(int id)
        {
            return _context.Members.Any(m => m.Id == id);
        }


        public void DeleteMember(Member member)
        {
            _context.Members.Remove(member);
        }  

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
