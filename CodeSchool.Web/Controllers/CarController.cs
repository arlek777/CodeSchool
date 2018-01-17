using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic;
using CodeSchool.DataAccess;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class CarController: Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public CarController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _crudService.GetAll<Car>();
            return Ok(cars);
        }

        [HttpGet]
        [Route("[action]/{query}")]
        public async Task<IActionResult> SearchCars(string query)
        {
            var cars = await _crudService.Where<Car>(c => c.Mark == query || c.Model == query);
            return Ok(cars);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            var alreadyAddedCar = await _crudService.Where<Car>(c => c.Mark == car.Mark && c.Model == car.Model);
            if (alreadyAddedCar.FirstOrDefault() != null)
            {
                return BadRequest("The car is already created.");
            }

            await _crudService.CreateOrUpdate(car, (dbCar, carModel) =>
            {
                dbCar.Mark = carModel.Mark;
                dbCar.Model = carModel.Model;
                dbCar.Year = carModel.Year;
                dbCar.Price = carModel.Price;
            });
            return Ok(car);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RemoveCar([FromBody] int id)
        {
            await _crudService.RemoveById<Car>(id);
            return Ok();
        }
    }
}
