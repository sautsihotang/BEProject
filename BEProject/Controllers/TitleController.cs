using BEProject.Data;
using BEProject.Model;
using BEProject.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly BEDbContext dbContext;

        public TitleController(BEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GetAllTitle
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var titles = await dbContext.Titles.OrderByDescending(s => s.TitleId).ToListAsync();
            
            List<TitleDto> ResultList = new List<TitleDto>();

            foreach (var title in titles)
            {
                TitleDto result = new TitleDto();
                result.id = title.TitleId;
                result.code = title.TitleCode;
                result.name = title.TitleName;
                
                ResultList.Add(result);
            }

            return Ok(ResultList);
        }

        //GetbyID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var title = await dbContext.Titles.FirstOrDefaultAsync(m => m.TitleId == id);
            if (title == null)
            {
                return NotFound("Your ID not Found");
            }

            TitleDto result = new TitleDto();
            result.id = title.TitleId;
            result.code = title.TitleCode;
            result.name = title.TitleName;

            return Ok(result);
        }

        //PostTitle
        [HttpPost]
        public async Task<IActionResult> PostTitle([FromBody] TitleReqDto titleReqDto)
        {
            Title request = new Title();
            request.TitleCode = titleReqDto.code;
            request.TitleName = titleReqDto.name;

            var checkTitleName = await dbContext.Titles.Where(t => t.TitleName == titleReqDto.name).ToListAsync();
            if (checkTitleName.Any())
            {
                return BadRequest("name title must unique");
            }
            var checkTitlecode = await dbContext.Titles.Where(t => t.TitleCode == titleReqDto.code).ToListAsync();
            if (checkTitlecode.Any())
            {
                return BadRequest("code title must unique");
            }



            await dbContext.Titles.AddAsync(request);
            await dbContext.SaveChangesAsync();

            return StatusCode(201, request);

        }

        //editTitle
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTitle([FromRoute] int id, TitleReqDto titleReqDto)
        {
            var titleUpdate = await dbContext.Titles.FirstOrDefaultAsync(x => x.TitleId == id);
            if (titleUpdate == null)
            {
                return NotFound();
            }

            titleUpdate.TitleCode = titleReqDto.code;
            titleUpdate.TitleName = titleReqDto.name;

            await dbContext.SaveChangesAsync();

            return Ok(titleUpdate);
        }

        //Delete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var titleDelete = await dbContext.Titles.FirstOrDefaultAsync(x => x.TitleId == id);
            if (titleDelete == null)
            {
                return NotFound();
            }

            var jobPosition = await dbContext.Positions.Where(x => x.TitleId == id).ToListAsync();
            if (jobPosition.Any())
            {
                return BadRequest("Title can't delete because already in use");
            }
            dbContext.Remove(titleDelete);
            await dbContext.SaveChangesAsync();
            return Ok(titleDelete);
        }

    }
}
