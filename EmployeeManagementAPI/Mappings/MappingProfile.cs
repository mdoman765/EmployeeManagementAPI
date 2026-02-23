using AutoMapper;
using EmployeeManagementAPI.DTOs.Attendance;
using EmployeeManagementAPI.DTOs.Department;
using EmployeeManagementAPI.DTOs.Employee;
using EmployeeManagementAPI.DTOs.Salary;
using EmployeeManagementAPI.DTOs.User;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name));
        CreateMap<CreateEmployeeDto, Employee>();

        CreateMap<Department, DepartmentDto>();
        CreateMap<CreateDepartmentDto, Department>();

        CreateMap<Salary, SalaryDto>()
            .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.Name));
        CreateMap<CreateSalaryDto, Salary>();

        CreateMap<Attendance, AttendanceDto>()
            .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.Name));
        CreateMap<CreateAttendanceDto, Attendance>();
        CreateMap<User, UserDto>()
    .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.Name));
    }
}
