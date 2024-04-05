using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[] ? Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string? RoomId { get; set; }

    [JsonPropertyName("roomName")]
    public string ?RoomName { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
}

public class Reservation
{
    public DateTime time { get; set; }
    public DateTime date { get; set; }
    public string ?reserverName { get; set; }
    public Room? room { get; set; }
}

public class ReservationHandler
{
    private List<Reservation> reservations = new List<Reservation>();

    public void addReservation(Reservation reservation)
    {
        

        if (reservations.Any(r => r.room?.RoomId == reservation.room?.RoomId && r.date == reservation.date && r.time == reservation.time))
        {
            Console.WriteLine("The room is already reserved for the specified date and time. Please choose another date or time.");
        }
        else
        {
            reservations.Add(reservation);
            Console.WriteLine("Reservation created successfully.");
        }
        
    }

    public void deleteReservation(Reservation reservation)
    {   
        

        if (!reservations.Contains(reservation))
        {
            Console.WriteLine("Reservation not found.");
            return;
        }

        reservations.Remove(reservation);
        Console.WriteLine("Reservation deleted successfully.");
        
    }

    public void displayWeeklySchedule()
    {
        

       var weeklySchedule = reservations.OrderBy(r => r.date).GroupBy(r => r.date.DayOfWeek);

        foreach (var group in weeklySchedule)
        {
            Console.WriteLine(group.Key);

            foreach (var reservation in group)
            {
                Console.WriteLine($"Time: {reservation.time.ToString("HH:mm")}, Date: {reservation.date.ToShortDateString()}, Name: {reservation.reserverName}, Room: {reservation.room?.RoomName}");
            }

            Console.WriteLine();
        }
    }
    
}

class Program
{
    static void Main(string[] args)
    {
        
        string filePath = "Data.json";

        
        string jsonString = File.ReadAllText(filePath);
        var roomData = JsonSerializer.Deserialize<RoomData>( jsonString, new JsonSerializerOptions()
        {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | 
                JsonNumberHandling.WriteAsString
        });

        
        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                Console.WriteLine($"Room ID: {room.RoomId}, Room Name: {room.RoomName}, Capacity: {room.Capacity}");
            }
        }

             ReservationHandler reservationHandler = new ReservationHandler();
          
       
        Room[] selectedRooms = new Room[]
        { 

            roomData.Rooms[0], 
            roomData.Rooms[1],
             roomData.Rooms[2], 
            roomData.Rooms[6]  ,
             roomData.Rooms[3], 
            roomData.Rooms[5]  ,
             roomData.Rooms[8], 
            roomData.Rooms[4],   
             roomData.Rooms[7], 
            roomData.Rooms[6]   
        
        };

        DateTime[] selectedDates = new DateTime[]
        {
            DateTime.Now.AddDays(1), 
            DateTime.Now.AddDays(4),
             DateTime.Now.AddDays(6), 
            DateTime.Now.AddDays(3) ,
             DateTime.Now.AddDays(1), 
            DateTime.Now.AddDays(7)  ,
             DateTime.Now.AddDays(2), 
            DateTime.Now.AddDays(3)  , 
            DateTime.Now.AddDays(3), 
            DateTime.Now.AddDays(5)  
        };
        DateTime[] selectedTimes = new DateTime[]
        {
            DateTime.Today.AddHours(10), 
            DateTime.Today.AddHours(7),
             DateTime.Today.AddHours(1), 
            DateTime.Today.AddHours(4),
             DateTime.Today.AddHours(5), 
            DateTime.Today.AddHours(3) ,
             DateTime.Today.AddHours(2), 
            DateTime.Today.AddHours(13),
             DateTime.Today.AddHours(17), 
            DateTime.Today.AddHours(6)   
        };
        string[] reserverNames = new string[]
        {
            "Mauro Icardi",
            "Arda Mert Ay",
            "Fernando Muslera",
            "Dries Mertens",
            "Wilfried Zaha" ,
            "Kerem Akturkoglu",
            "Lucas Torreira",
            "Baris Alper Yilmaz",
            "Tete",
            "Victor Nelsson"
        };

    
       Reservation[] reservations = new Reservation[10];
        for (int i = 0; i < selectedRooms.Length; i++)
        {
           reservations[i]=new Reservation
            {
                time = selectedTimes[i],
                date = selectedDates[i],
                reserverName = reserverNames[i],
                room = selectedRooms[i]
            };
            reservationHandler.addReservation(reservations[i]);
        }
         Console.WriteLine("<-----------Reservation List------------->");
         reservationHandler.displayWeeklySchedule();

        reservationHandler.deleteReservation(reservations[3]);
        reservationHandler.deleteReservation(reservations[6]);
        reservationHandler.deleteReservation(reservations[8]);
        reservationHandler.deleteReservation(reservations[5]);

        Console.WriteLine("<-----------Updated Reservation List------------->");

         reservationHandler.displayWeeklySchedule();

    }
}
