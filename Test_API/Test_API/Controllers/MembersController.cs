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
        private object patchDoc;
        private int memberId;

        public MembersController(IMemberData memberData) 
        {
            _memberData = memberData;
        }



        [HttpGet("{id}", Name = "GetMember")]
        public IActionResult getMember(int id)
        {
            var memberFromRepo = _memberData.GetMember(id);

            var memberToReturn = Mapper.Map<MemberDto>(memberFromRepo);

            return Ok(memberToReturn);

        }


        [HttpPost]
        public IActionResult addMember([FromBody] MemberForCreationDto member)
        {
            if (member == null)
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

            [HttpPatch("{id}")]
            public IActionResult PartiallyUpdateMembersController(int id, [FromBody] JsonPatchExtensions<MemberDto>patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }
            if (!_memberData.MemberExists(memberId))
            {
                return NotFound();
            }
            
            var MemberFormRepo = _memberData.GetMember(memberId);

            if(MemberFormRepo == null)
            {
                return NotFound();
            }
            var memberToPatch = Mapper.Map<MemberDto>(MemberFormRepo);

            patchDoc.ApplyTo(memberToPatch);
             
            Mapper.Map(memberToPatch, MemberFormRepo);

            _memberData.UpdateMembersController(MemberFormRepo);

            if (!_memberData.Save())
            {
                throw new Exception($"Patching book {id} for Member {memberId} failed on save");
            }

            return NoContent();

        }
    }
}
