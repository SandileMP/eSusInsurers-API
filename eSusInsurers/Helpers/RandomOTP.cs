namespace eSusInsurers.Helpers
{
    public class RandomOTP
    {
        public static string CreateRandomOTP()
        {
            string _allowedChars = "0123456789";
            Random randomNum = new Random();
            char[] chars = new char[4];
            for (int i = 0; i < 4; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randomNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
