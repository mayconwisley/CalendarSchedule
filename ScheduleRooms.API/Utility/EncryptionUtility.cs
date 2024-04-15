using ScheduleRooms.API.Utility.Interface;
using System.Security.Cryptography;
using System.Text;

namespace ScheduleRooms.API.Utility;

public class EncryptionUtility : IEncryptionUtility
{
    private readonly IConfiguration _configuration;

    public EncryptionUtility()
    {
        _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();
    }

    public string Dado(string? dado)
    {
        if (dado is null)
        {
            return string.Empty;
        }

        string? strKey = _configuration.GetSection("Encryption")["Key"];
        string? strIv = _configuration.GetSection("Encryption")["Iv"];

        byte[] key = Encoding.UTF8.GetBytes(strKey!);
        byte[] iv = Encoding.UTF8.GetBytes(strIv!);

        using (TripleDES triple = TripleDES.Create())
        {
            triple.Key = key;
            triple.IV = iv;

            byte[] dadoByte = Encoding.UTF8.GetBytes(dado);
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, triple.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dadoByte, 0, dadoByte.Length);
                cs.FlushFinalBlock();
            }
            byte[] encryption = ms.ToArray();

            dado = Convert.ToBase64String(encryption);
            encryption = Convert.FromBase64String(dado);
        }
        return dado;
    }
}
