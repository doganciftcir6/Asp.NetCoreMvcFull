
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.UI.Extensions;

namespace Udemy.TodoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        //İlgili Service'mi kullanmak için Dependency Injection
        private readonly IWorkService _workService;

        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _workService.GetAll();
            return View(response.Data);
        }
        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            var response = await _workService.Create(dto);
            return this.ResponseRedirectToAction(response, "Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            //var dto = await _workService.GetById(id);
            //return View(new WorkUpdateDto
            //{
            //    Id = dto.Id,
            //    Definition = dto.Definition,
            //    IsCompleted = dto.IsCompleted
            //});

            //AUTOMAPPERDAN SONRA
            //return View(_mapper.Map<WorkUpdateDto>(dto));

            var response = await _workService.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
            var response = await _workService.Update(dto);
            return this.ResponseRedirectToAction(response, "Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _workService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
        }
        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
