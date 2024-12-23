using System.Diagnostics;

namespace UnifiedCivilNumberGenerator
{
    internal class UCNCracker
    {
        /// <summary>
        /// Target:
        /// Male
        /// Born on 01/01/2000 in Sofia
        /// Change pre-set conditions for customized experience
        /// </summary>

        public static bool male = true;
        public static string year = "2000";
        public static string month = "01";
        public static string day = "01";
        public static string city = "Sofia-city";

        public static Dictionary<string, string> birthCityKeyValuePair = new Dictionary<string, string>()
        {
            {"Blagoevgrad", "000-043"},{"Burgas","044-093"},{"Varna","094-139"},{"Veliko Tarnovo","140-169"},{"Vidin","170-183"},
            {"Vratsa","184-217"},{"Gabrovo","218-233"},{"Kurdjali","234-281"},{"Kiustendil","282-301"},{"Lovetch","302-319"},
            {"Montana","320-341"},{"Pazardjik","342-377"},{"Pernik","378-395"},{"Pleven","396-435"},{"Plovdiv","436-501"},
            {"Razgrad","502-527"},{"Ruse","528-555"},{"Silistra","556-575"},{"Sliven","576-601"},{"Smolqn","602-623"},
            {"Sofia-city","624-721"},{"Sofia-county","722-751"},{"Stara Zagora","752-789"},{"Dobrich","790-821"},
            {"Targovishte","822-843"},{"Haskovo","844-871"},{"Shumen","872-903"},{"Qmbol","904-925"},{"Other","926-999"}
        };

        /// <summary>
        /// This method generates all the possible UCN combinations based on the pre-set target conditions. 
        /// It's shockingly easy and downright worrysome how effective this simple UCN generator is. 
        /// I won't disclose how I check the validity of the UCNs, but trust me, it's easy.
        /// </summary>
        public static void UCNCombinationsGenerator()
        {
            // year
            string n = year.Substring(2);

            // month
            string x = "";
            if(int.Parse(year) <= 1900)
            {
                x = (int.Parse(month) + 20).ToString();
            }
            else if(int.Parse(year) >= 2000)
            {
                x = (int.Parse(month) + 40).ToString();
            }

            string UCN = n + x + day;

            string cityRange = "";
            foreach (var pair in birthCityKeyValuePair)
            {
                if (pair.Key == city)
                {
                    cityRange = pair.Value;
                }
            }

            string[] splitted = cityRange.Split('-').ToArray();
            string startIndex = splitted[0] + "0";
            string endIndex = splitted[1] + "9";

            // Sofia range - 624-721 
            int startLastFour = int.Parse(startIndex);
            int endLastFour = int.Parse(endIndex);

            List<string> UCNs = new List<string>();

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int lastFour = startLastFour; lastFour <= endLastFour; lastFour++)
            {
                // even for men
                if (male)
                {
                    if (lastFour.ToString()[2] % 2 == 0)
                    {
                        UCNs.Add(UCN + lastFour);
                    }
                }
                else // odd for women
                {
                    if (lastFour.ToString()[2] % 2 == 1)
                    {
                        UCNs.Add(UCN + lastFour);
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine(string.Join(Environment.NewLine, UCNs));
            Console.WriteLine($"Generated {UCNs.Count} UCNs in {stopwatch.ElapsedMilliseconds} ms");
            
        }
    }
}
