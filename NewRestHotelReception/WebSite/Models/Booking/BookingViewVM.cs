using WebSite.Models.Shared;

namespace WebSite.Models.Booking
{
	public class BookingViewVM
	{
        public List<BookingVM> List { get; set; }
        public PagerVM Pager { get; set; }

    }
}
