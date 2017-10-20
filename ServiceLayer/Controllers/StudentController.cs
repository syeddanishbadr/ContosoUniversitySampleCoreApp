using Framework.Core;
using Framework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ServiceLayer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        ILogger _logger;
        UnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public StudentController(UnitOfWork unitOfWork, ILogger<StudentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Returns the collection of students  
        /// </summary>
        /// <returns>Students collection</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _unitOfWork.StudentRepository.GetAllAsnyc(noTracking: true);
            return Ok(items);
        }

        /// <summary>
        /// Return student details based on the Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /Student
        ///     {
        ///         "id" : 3
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Student</returns>
        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _unitOfWork.StudentRepository.GetByIdAsnyc(id, noTracking: true);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Saves student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            _unitOfWork.StudentRepository.Insert(student);

            if (await _unitOfWork.Save())
                return CreatedAtRoute("GetStudent", new { id = student.ID }, student);
            else
                return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] Student student)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _unitOfWork.StudentRepository.GetByIdAsnyc(id.Value);

            if (studentToUpdate.ID != id)
            {
                return BadRequest();
            }

            try
            {
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.FirstMidName = student.FirstMidName;
                studentToUpdate.EnrollmentDate = student.EnrollmentDate;

                _unitOfWork.StudentRepository.Update(studentToUpdate);

                if (await _unitOfWork.Save())
                    return new NoContentResult();
                else
                    return BadRequest();
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsnyc(id);

            if (student == null)
            {
                return NotFound();
            }

            _unitOfWork.StudentRepository.Delete(student);
            if (await _unitOfWork.Save())
                return new NoContentResult();
            else
                return BadRequest();
        }
    }
}