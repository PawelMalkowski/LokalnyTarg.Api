using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;
using LokalnyTarg.Api.Mappers;
using LokalnyTarg.Api.ViewModel;
using LokalnyTarg.IServices.Category;
using LokalnyTarg.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LokalnyTarg.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Category")]
    [EnableCors()]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Route("", Name = "GetCategory")]
        [HttpGet]

        public async Task<IActionResult> GetCategory()
        {
            var categoryList = await _categoryService.GetCategories();
            return Ok(categoryList);
        }

        [Route("", Name = "AddCategory")]
        [HttpPost]
        [Authorize(Roles = "Administrator, Admin")]

        public async Task<IActionResult> AddCategory([FromBody] AddCategory addCategory)
        {
            try
            {
                await _categoryService.AddCategory(
                    AddCategoryToAddCategoryServiceMapper.AddCategoryToAddCategoryService(addCategory));
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Status = "Error",
                    ErrorDescription = exception.Message 
                };
                return BadRequest(error);
            }
            return Ok();

        }
    }
}
