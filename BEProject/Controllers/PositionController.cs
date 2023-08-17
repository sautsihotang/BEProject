using BEProject.Data;
using BEProject.Model;
using BEProject.Model.Dto;
using BEProject.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly BEDbContext dbContext;

        public PositionController(BEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //getAllPosition
        [HttpGet]
        public async Task<IActionResult> getPosition()
        {
            var positions = await dbContext.Positions.Include(x => x.Title).OrderByDescending(s => s.PositionId).ToListAsync();
            List<PositionDto> ResultList= new List<PositionDto>();

            foreach (var position in positions)
            {
                PositionDto result = new PositionDto();
                result.id = position.PositionId;
                result.code = position.PositionCode;
                result.name = position.PositionName;
                result.titleId = position.TitleId;
                result.titleCode = position.Title.TitleCode;
                result.titleName = position.Title.TitleName;


                ResultList.Add(result);
            }

            return Ok(ResultList);
        }

        //GetByID
        [HttpGet("{id}")]
        public async Task<IActionResult> getPositionById(int id)
        {
            var position = await dbContext.Positions.Include(x => x.Title).FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null)
            {
                return NotFound();
            }

            PositionDto result = new PositionDto();
            result.id = position.PositionId;
            result.code = position.PositionCode;
            result.name = position.PositionName;
            result.titleId = position.TitleId;
            result.titleCode = position.Title.TitleCode;
            result.titleName = position.Title.TitleName;
            return Ok(result);
        }

        //PostPosition
        [HttpPost]
        public async Task<IActionResult> Position([FromBody] PositionReqDto positionReqDto)
        {
            Position newPosition = new Position
            {
                PositionCode = positionReqDto.code,
                PositionName = positionReqDto.name,
                TitleId = positionReqDto.titleId,
            };

            var checkPosCode = await dbContext.Positions.Where(s => s.PositionCode == newPosition.PositionCode).ToListAsync();
            if (checkPosCode.Any())
                return BadRequest("code position must unique");

            var checkPosName = await dbContext.Positions.Where(s => s.PositionName == newPosition.PositionName).ToListAsync();
            if (checkPosName.Any())
                return BadRequest("code position must unique");

            await dbContext.AddAsync(newPosition);
            await dbContext.SaveChangesAsync();

            return StatusCode(201, newPosition);
        }

        //editPosition
        [HttpPut("{id}")]
        public async Task<IActionResult> Position([FromRoute] int id, PositionReqDto positionReqDto)
        {
            var positionPut = await dbContext.Positions.FirstOrDefaultAsync(m => m.PositionId == id);
            if (positionPut == null)
            {
                return NotFound();
            }

            positionPut.PositionCode = positionReqDto.code;
            positionPut.PositionName = positionReqDto.name;
            positionPut.TitleId = positionReqDto.titleId;
            await dbContext.SaveChangesAsync(); 
            return Ok(positionPut);
        }


        //deleteposition
        [HttpDelete("{id}")]
        public async Task<IActionResult> Position([FromRoute] int id)
        {
            var positionDelete = await dbContext.Positions.FirstOrDefaultAsync(m => m.PositionId == id);
            if (positionDelete == null)
            {
                return NotFound();
            }

            var employee = await dbContext.Employees.Where(p => p.PositionId == id).ToListAsync();
            if (employee.Any())
            {
                return BadRequest("Position can't delete because already in use");
            }

            dbContext.Remove(positionDelete);
            await dbContext.SaveChangesAsync();
            return Ok(positionDelete);
        }


    }
}
