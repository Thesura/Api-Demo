using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_API.Entities;
using Test_API.Models;
using Test_API.Services;

namespace Test_API.Controllers
{
    [Route("api/members")]
    public class MembersController : Controller {
        private IMemberData _memberData;

        public MembersController(IMemberData memberData) 
        {
            _memberData = memberData;
        }



        [HttpGet("{id}", Name = "GetMember")]
        public IActionResult getMember(int id)
        {
            if (!_memberData.MemberExists(id))
            {
                return NotFound();
            }

            var memberFromRepo = _memberData.GetMember(id);

            var memberToReturn = Mapper.Map<MemberDto>(memberFromRepo);

            return Ok(memberToReturn);



        }

        [HttpGet()]
        public IActionResult GetAllMembers()
        {
            var memberFromRepo = _memberData.GetMembers();

            var members = Mapper.Map<IEnumerable<MemberDto>>(memberFromRepo);

            return new JsonResult(members);

        }


        [HttpPost]
       public IActionResult addMember([FromBody] MemberForCreationDto member)
        {
            if(member == null)
            {
                return BadRequest();
            }

            var memberForCreation = Mapper.Map<Member>(member);

            _memberData.AddMember(memberForCreation);

            if (!_memberData.Save())
            {
                throw new Exception("save failed");
            }

            var memberToReturn = Mapper.Map<MemberDto>(memberForCreation);

            return CreatedAtRoute("GetMember", new { id = memberToReturn.Id }, memberToReturn);
        }

        [HttpDelete("{id}")]

        public IActionResult deleteMember(int id)
        {
            var memberFromRepo = _memberData.GetMember(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }
            _memberData.DeleteMember(memberFromRepo);

            if (!_memberData.Save())
            {
                throw new Exception($"Delete a member {id} failed");
            }

            return NoContent();
        }

    }
}
