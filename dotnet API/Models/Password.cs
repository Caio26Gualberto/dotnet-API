using System;
using System.Security.Cryptography;
using System.Text;

public class Password
{
    //Todo aprender nova forma de importar senha e chave por arquivos externos para não ficar expostos arquivos sensiveis ao código
    public static byte[] EncryptedPassword()
    {
        string senha = "anewlevel098123";
        string chave = "098123";

        // Criptografar a senha
        byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
        byte[] chaveBytes = Encoding.UTF8.GetBytes(chave);
        byte[] criptografado = null;

        using (Aes aes = Aes.Create())
        {
            aes.Key = chaveBytes;
            aes.IV = new byte[16];
            aes.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var ms = new System.IO.MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(senhaBytes, 0, senhaBytes.Length);
                cs.FlushFinalBlock();
                criptografado = ms.ToArray();
            }
        }

        // Descriptografar a senha
        using (Aes aes = Aes.Create())
        {
            aes.Key = chaveBytes;
            aes.IV = new byte[16];
            aes.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var ms = new System.IO.MemoryStream())
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                cs.Write(criptografado, 0, criptografado.Length);
                cs.FlushFinalBlock();
                byte[] descriptografado = ms.ToArray();
                string senhaDescriptografada = Encoding.UTF8.GetString(descriptografado);

                return descriptografado;
            }
        }
    }
}
