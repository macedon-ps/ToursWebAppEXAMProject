namespace ToursWebAppEXAMProject.Utils
{
    public class Calculations
    {
        public static int CalculateAge(DateTime birthday)
        {
            if (birthday == DateTime.MinValue) return 0;
            else
            {
                var now = DateTime.Now;
                var age = now.Year - birthday.Year;

                if (now.AddYears(-age) < birthday)
                {
                    age--;
                }
                return age;
            }
        }
    }
}
