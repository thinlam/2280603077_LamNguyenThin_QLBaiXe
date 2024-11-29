using System;
using System.Collections.Generic;
using System.Text;

interface IVehicle
{
    void DisplayInfo(); 
    decimal CalculateFee(DateTime checkOutTime); 
}

abstract class Vehicle : IVehicle
{
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Type { get; set; }
    public DateTime CheckInTime { get; set; }
    public const decimal HourlyRate = 20000; 

    public abstract void DisplayInfo();

    public decimal CalculateFee(DateTime checkOutTime)
    {
        TimeSpan duration = checkOutTime - CheckInTime;
        return (decimal)Math.Ceiling(duration.TotalHours) * HourlyRate;
    }
}

class Car : Vehicle
{
    public override void DisplayInfo()
    {
        Console.WriteLine($"Biển số: {LicensePlate}, Hãng: {Brand}, Loại: {Type}, Giờ vào: {CheckInTime}");
    }
}

class ParkingLot
{
    private List<Vehicle> ListVehicle = new List<Vehicle>();
    private Dictionary<string, Vehicle> Vehicles = new Dictionary<string, Vehicle>();
    private List<Vehicle> ArrayList = new List<Vehicle>();

    public void AddVehicle(Vehicle vehicle)
    {
        ListVehicle.Add(vehicle);
        Vehicles[vehicle.LicensePlate] = vehicle;
        Console.WriteLine("Xe đã được thêm thành công!");
    }

    public void DisplayVehicles()
    {
        Console.WriteLine("Danh sách xe trong bãi:");
        foreach (var vehicle in ListVehicle)
        {
            vehicle.DisplayInfo();
        }
    }

    public void SearchVehicle(string licensePlate)
    {
        if (Vehicles.TryGetValue(licensePlate, out var vehicle))
        {
            Console.WriteLine("Thông tin xe:");
            vehicle.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Không tìm thấy xe với biển số này.");
        }
    }

    public void RemoveVehicle(string licensePlate, DateTime checkOutTime)
    {
        if (Vehicles.TryGetValue(licensePlate, out var vehicle))
        {
            decimal fee = vehicle.CalculateFee(checkOutTime);
            Console.WriteLine($"Phí gửi xe: {fee} VND");
            ListVehicle.Remove(vehicle);
            Vehicles.Remove(licensePlate);
            Console.WriteLine("Xe đã được xóa khỏi bãi.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy xe với biển số này.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        ParkingLot parkingLot = new ParkingLot();

        while (true)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\n1. Thêm xe mới");
            Console.WriteLine("2. Hiển thị danh sách xe trong bãi");
            Console.WriteLine("3. Tìm kiếm xe");
            Console.WriteLine("4. Xóa xe khỏi bãi");
            Console.WriteLine("5. Thoát chương trình");
            Console.Write("Chọn chức năng: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Nhập biển số xe: ");
                    string licensePlate = Console.ReadLine();
                    Console.Write("Nhập hãng xe: ");
                    string brand = Console.ReadLine();
                    Console.Write("Nhập loại xe: ");
                    string type = Console.ReadLine();
                    DateTime checkInTime = DateTime.Now;

                    Vehicle car = new Car
                    {
                        LicensePlate = licensePlate,
                        Brand = brand,
                        Type = type,
                        CheckInTime = checkInTime
                    };

                    parkingLot.AddVehicle(car);
                    break;

                case 2:
                    parkingLot.DisplayVehicles();
                    break;

                case 3:
                    Console.Write("Nhập biển số xe cần tìm: ");
                    string searchLicensePlate = Console.ReadLine();
                    parkingLot.SearchVehicle(searchLicensePlate);
                    break;

                case 4:
                    Console.Write("Nhập biển số xe cần lấy: ");
                    string removeLicensePlate = Console.ReadLine();
                    Console.Write("Nhập giờ ra (hh:mm): ");
                    string[] timeParts = Console.ReadLine().Split(':');
                    DateTime checkOutTime = DateTime.Now.Date.AddHours(int.Parse(timeParts[0]))
                        .AddMinutes(int.Parse(timeParts[1]));
                    parkingLot.RemoveVehicle(removeLicensePlate, checkOutTime);
                    break;

                case 5:
                    return;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }
}
