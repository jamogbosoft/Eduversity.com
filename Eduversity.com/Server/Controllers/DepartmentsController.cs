using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("admin-fId/{facultyId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DepartmentsResponse>>> GetAdminDepartments(int facultyId)
        {
            var result = await _departmentService.GetAdminDepartments(facultyId);
            return Ok(result);
        }

        [HttpGet("user-fId/{facultyId}")]
        public async Task<ActionResult<ServiceResponse<List<DepartmentReadDto>>>> GetDepartments(int facultyId)
        {
            var result = await _departmentService.GetDepartments(facultyId);
            return Ok(result);
        }

        [HttpGet("admin-dId/{departmentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DepartmentResponse>>> GetAdminDepartment(int departmentId)
        {
            var result = await _departmentService.GetAdminDepartment(departmentId);
            return Ok(result);
        }

        [HttpGet("user-dId/{departmentId}")]
        public async Task<ActionResult<ServiceResponse<DepartmentReadDto>>> GetDepartment(int departmentId)
        {
            var result = await _departmentService.GetDepartment(departmentId);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Department>>> CreateDepartment(Department department)
        {
            var result = await _departmentService.CreateDepartment(department);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Department>>> UpdateDepartment(Department department)
        {
            var result = await _departmentService.UpdateDepartment(department);
            return Ok(result);
        }

        [HttpDelete("{departmentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteDepartment(int departmentId)
        {
            var result = await _departmentService.DeleteDepartment(departmentId);
            return Ok(result);
        }

    }
}
