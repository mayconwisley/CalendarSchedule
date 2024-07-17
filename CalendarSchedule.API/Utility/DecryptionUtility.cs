using CalendarSchedule.API.Utility.Interface;
using System.Security.Cryptography;
using System.Text;

namespace CalendarSchedule.API.Utility;

public class DecryptionUtility : IDecryptionUtility
{
    private readonly IConfiguration _configuration;

    public DecryptionUtility()
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
            byte[] dadoEncryption = Convert.FromBase64String(dado);
            byte[] dadoDecryption = triple.CreateDecryptor().TransformFinalBlock(dadoEncryption, 0, dadoEncryption.Length);
            dado = Encoding.UTF8.GetString(dadoDecryption);
        }
        return dado;
    }
}
