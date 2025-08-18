using System.Text.Json.Serialization;

namespace VehiclesApp.DTOs;

public class VeicleMakeThirdParty
{ 
 [JsonPropertyName("Make_ID")] 
 public int Id { get; set; }
    
 [JsonPropertyName("Make_Name")]
 public string Name { get; set; }
 
}

public class VeicleTypeThirdParty
{

   [JsonPropertyName("VehicleTypeId")] 
   public int Id { get; set; }
    
   [JsonPropertyName("VehicleTypeName")]
   public string Name { get; set; }

}

public class VeicleModelMakeThirdParty
{

   [JsonPropertyName("Make_ID")] 
   public int MakeId { get; set; }
    
   [JsonPropertyName("Make_Name")]
   public string Name { get; set; } 
   
   [JsonPropertyName("Model_ID")] 
   public int ModelId { get; set; }
    
   [JsonPropertyName("Model_Name")]
   public string ModelName { get; set; }

}