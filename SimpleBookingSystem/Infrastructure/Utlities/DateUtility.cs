namespace SimpleBookingSystem.Infrastructure.Utlities
{
    public class DateUtility
    {
        public static bool DatesOverlap(DateTime dtStartA, DateTime dtEndA, DateTime dtStartB, DateTime dtEndB)
        {
            return dtStartA < dtEndB && dtStartB < dtEndA;
        }
    }
}
