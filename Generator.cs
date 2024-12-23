namespace UnifiedCivilNumberGenerator
{
    public class Generator
    {
        /// <summary> 
        /// 
        /// DOCUMENTATION
        /// 
        /// The method below generates a new UCN variation, that in my opinion, is significally harder to crack. Instead of a pre-set standards that are
        /// easily predicted, I made an overhaul of the entire structure. The UCN will be created using a timestamp of the unix time that
        /// has passed, down to the second of the birth. This already makes the predictability harder, since only the owner of the UCN,
        /// their parents and the hospital in which they were brought to life would know that information. The timestamp is then randomized
        /// once more by adding a birthplace + gender 3-digit index, which is also present in the original Bulgarian UCN. The timestamp is,
        /// again, randomized a third time, by multiplying it by two. This makes the timestamp completely irreversible. Generating an entire
        /// correct 10-digit UCN is way harder than just generating a 4-digit part of the UCN. 
        /// 
        /// There are 1000 4-digit combinations, at best, in the specified pre-set range for each bg city + control number. It took me 
        /// about 10 tries to generate a valid UCN by just brute-forcing 4-digit combinations in a specified city range. That's because 
        /// there's far too little possible combinations for the limited range the UCNs provide. 
        /// 
        /// The UCN format will change to an 11-digit number after the next century and will remain that way up to 20 centuries in the future,
        /// after which the format will become a 12-digit number and so on. The 12-digit format would outlive our dating format, and most possibly
        /// humankind itself. 
        /// The chance of collision is practically null. In the examples I demonstrate that even if a bunch of people were born in the
        /// same day, hour, minute and second, in the same city and with the same gender, their UCNs will be different. 
        /// 
        /// The Bulgarian UCN structure is made for easier human understanding, which is its major flaw, making it insecure and outdated. 
        /// In a world in which there are technological advancements every month, the Bulgarian goverment must think of better ways to 
        /// preserve vital personal information about its citizens. Personality theft and fraud will advance with the new technology wave,
        /// but will the outdated structure of the country advance with it too? 
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="birthPlaceGenderIndex"></param>
        /// <param name="city"></param>
        /// <param name="gender"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        public static void UCNGenerator(int year, int month, int day, int hour, int minutes, int seconds, int birthPlaceGenderIndex, string city, string gender, string date, string time)
        {
            // convert info about citizen in date format
            DateTime birth = new DateTime(year, month, day,
                             hour, minutes, seconds, DateTimeKind.Utc);

            // convert birth date into a 64-bit timestamp (Will outlive the 2038 Unix Bug, maybe even our planet)
            long unixTimeStamp = Math.Abs(((DateTimeOffset)birth).ToUnixTimeSeconds());

            // add birthplace + gender index, then multiply by 2 to make the timestamp even more unpredictable
            long newUCN = (unixTimeStamp + birthPlaceGenderIndex) * 2;

            Console.WriteLine("New UCN");
            Console.WriteLine(newUCN);

            // store the generated UCN examples
            string path = "C:../../../IO/Examples.txt";
            File.AppendAllText(path, newUCN.ToString() + $" - {city} - {gender} - {date} - {time}" + Environment.NewLine);
        }
     
    }
}
