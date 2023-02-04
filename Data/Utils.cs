using System.Security.Cryptography;


namespace AD_CW1_20048632_Amrit_Adhikari_C2.Data    // this is the namespace
{
    internal class Utils
    {
        private const char _segmentDelimiter = ':';

        public static string HashSecret(string input)   
        {
            var saltSize = 16;
            var iterations = 100_000;
            var keySize = 32;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);   // this is the method

            return string.Join(
                _segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        public static bool VerifyHash(string input, string hashString)  //verifyhash
        {
            string[] segments = hashString.Split(_segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        public static string GetAppDirectoryPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "AD_cw1_Amrit_Adhikari"     // this is the name of the folder where json file will be saved
            );
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");   // this is the name of the json file with users data
        }

        public static string GetInventoryFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "_inventory.json");  // this is the name of the json file with inventory data
        }
        public static string GetTakeoutsFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "_takeouts.json");   // this is the name of the json file with takeouts data
        }
    }
}
