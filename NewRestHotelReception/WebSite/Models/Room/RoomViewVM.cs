using WebSite.Models.Shared;

namespace WebSite.Models.Room
{
    public class RoomViewVM
    {
        public List<RoomVM> List { get; set; }
        public string Orderby { get; set; }
        public FilterVM Filter { get; set; }
        
    }
}
