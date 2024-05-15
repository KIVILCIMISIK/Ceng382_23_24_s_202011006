using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Room
    {
        [Key]
        public string  roomId { get; set; }

       public string RoomName { get; set; }
    public int Capacity { get; set; }
    public List<Reservation> Reservations { get; set; }
}
public class Reservation
{
   [Key]
    public int ReservationId { get; set; }
    public DateTime Time { get; set; }
    public DateTime Date { get; set; }
    public string ReserverName { get; set; }
      public int RoomId { get; set; }
     public Room Room { get; set; }
}

    }

