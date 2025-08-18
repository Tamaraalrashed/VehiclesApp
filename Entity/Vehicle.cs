namespace Entity;

public class Vehicle
{
    public VehicleMake  Make { get; set; }
    public VehicleModel  Model { get; set; }
}

public class VehicleMake
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class VehicleType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class VehicleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}