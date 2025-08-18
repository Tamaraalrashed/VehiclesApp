using System.Text.Json;
using Entity;
using Microsoft.AspNetCore.Mvc;
using VehiclesApp.DTOs;

namespace VehiclesApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController(HttpClient http) : ControllerBase

{
  
    [HttpGet("GetAllMakes")]
    public async Task<ActionResult<List<VehicleMake>>> GetAllMakes(int page = 1, int pageSize = 10)
    {
        try
        {
            var url = "https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json";
            var resultsJson = await http.GetAsync(url);
            resultsJson.EnsureSuccessStatusCode();

            var content = await resultsJson.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            VeicleMakeWrapper results = JsonSerializer.Deserialize<VeicleMakeWrapper>(content, options);
            List<VehicleMake> makes = results.Results
                .Select(p => new VehicleMake()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToList();

            return Ok(makes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    
    }
    
    [HttpGet("GetAllTypes")]
    public async Task<ActionResult<List<VehicleType>>> GetAllTypes(int makeId)
    {
        try
        {
            var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json";
            var resultsJson = await http.GetAsync(url);
            resultsJson.EnsureSuccessStatusCode();

            var content = await resultsJson.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            VeicleTypeWrapper results = JsonSerializer.Deserialize<VeicleTypeWrapper>(content, options);
            List<VehicleType> makes = results.Results
                .Select(p => new VehicleType()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToList();

            return Ok(makes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    
    }   
    
    [HttpGet("GetModelsForMake")]
    public async Task<ActionResult<List<Vehicle>>> GetModelsForMake(int makeId,int modelYear)
    {
        try
        {
            var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{modelYear}?format=json";
            //var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json";
            var resultsJson = await http.GetAsync(url);
            resultsJson.EnsureSuccessStatusCode();

            var content = await resultsJson.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            VeicleModelMakeWrapper results = JsonSerializer.Deserialize<VeicleModelMakeWrapper>(content, options);
            List<Vehicle> models = results.Results
                .Select(p => new Vehicle()
                {
                    
                   Make = new VehicleMake()
                   {
                       Id = p.MakeId,
                       Name = p.Name,
                   },
                   Model = new VehicleModel()
                   {
                       Id = p.ModelId,
                       Name = p.ModelName
                   }
                   
                })
                .ToList();

            return Ok(models);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    
    }
}