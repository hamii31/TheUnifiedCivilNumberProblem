namespace UnifiedCivilNumberGenerator
{
    public class NewUCN
    {
        /// <summary>
        /// The idea behind this project is to demonstrate how outdated the Bulgarian UCN structure is, how easy it is to brute-force it
        /// and my solution for the problem.
        /// </summary>
        public static Dictionary<string, string> birthCityKeyValuePair = new Dictionary<string, string>()
        {
            {"Blagoevgrad", "000-043"},{"Burgas","044-093"},{"Varna","094-139"},{"Veliko Tarnovo","140-169"},{"Vidin","170-183"},
            {"Vratsa","184-217"},{"Gabrovo","218-233"},{"Kurdjali","234-281"},{"Kiustendil","282-301"},{"Lovetch","302-319"},
            {"Montana","320-341"},{"Pazardjik","342-377"},{"Pernik","378-395"},{"Pleven","396-435"},{"Plovdiv","436-501"},
            {"Razgrad","502-527"},{"Ruse","528-555"},{"Silistra","556-575"},{"Sliven","576-601"},{"Smolqn","602-623"},
            {"Sofia-city","624-721"},{"Sofia-county","722-751"},{"Stara Zagora","752-789"},{"Dobrich","790-821"},
            {"Targovishte","822-843"},{"Haskovo","844-871"},{"Shumen","872-903"},{"Qmbol","904-925"},{"Other","926-999"}
        };
        static void Main(string[] args)
        {
            // Type crack or new in order to choose what you want to do. 
            Console.WriteLine("Choose mode: (crack/new)");
            Console.Write("> ");
            string input = Console.ReadLine();
            if(input.ToLower() == "crack")
            {
                UCNCracker.UCNCombinationsGenerator(); // Generate random UCNs 
            }
            else if(input.ToLower() == "new")
            {
                UCNBuilder(); // Generate a new UCN varation
            }
            else
            {
                Console.WriteLine("Unrecognized command");
            }
        }

        public static void UCNBuilder()
        {
            Console.WriteLine("Enter birth date: (dd/mm/yyyy)");
            Console.Write("> ");
            string birthDate = Console.ReadLine()!;
            string[] splitted = birthDate.Split('/').ToArray();
            string day =splitted[0];
            string month = splitted[1];
            string year = splitted[2];

            Console.WriteLine("Enter birth time: (hh:mm:ss)");
            Console.Write("> ");
            string birthTime = Console.ReadLine()!;
            splitted = birthTime.Split(':').ToArray();
            int hour = int.Parse(splitted[0]);
            int minutes = int.Parse(splitted[1]);
            int seconds = int.Parse(splitted[2]);

            Console.WriteLine("Enter birth city:");
            Console.Write("> ");
            string city = Console.ReadLine()!;
            string cityRange = "";

            Console.WriteLine("Enter gender:(M/F)");
            Console.Write("> ");
            string gender = Console.ReadLine()!;

            foreach (var pair in birthCityKeyValuePair)
            {
                if (pair.Key == city)
                {
                    cityRange = pair.Value;
                }
            }

            splitted = cityRange.Split('-').ToArray();
            int startIndex = int.Parse(splitted[0]);
            int endIndex = int.Parse(splitted[1]);
            int birthPlaceGenderIndex = 0;

            // Generate a random number based on birth city and gender 
            Random rng = new Random();
            switch (gender.ToLower())
            {
                case "m":
                    birthPlaceGenderIndex = (2 * rng.Next(startIndex / 2, endIndex / 2));
                    break;
                case "f":
                    birthPlaceGenderIndex = (2 * rng.Next(startIndex / 2, endIndex / 2) + 1);
                    break;
                default:
                    break;
            }

            // The typical Bulgarian UCN will place a 3-digit number, based on where the person is born and at what order they were born,
            // as well as the gender of the person (male = even last digit, female = odd last digit)
            // After that, the 10th number will be the remainer of the sum of the 9 numbers with the added static weights for each 
            // number, divided by 11. 

            // Utilizing the cracker I built, I was able to find a valid UCN in less than a minute, it was the 8th UCN in the list of 490. 
            // I am fairly certain you will always find valid UCNs from the start of the list. To combat this vulnerability
            // I have come up with a solution - restructure the entire UCN structure.

            // Generate new UCN variation
            Generator.UCNGenerator(int.Parse(year), int.Parse(month), int.Parse(day), hour, minutes, seconds, birthPlaceGenderIndex, city, gender, birthDate, birthTime);
        }
    }
}
