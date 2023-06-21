using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace HKInc.Utils.Encrypt
{
    public class AESEncrypt256
    {
        public static string Encrypt(string password)
        {
            return Encrypt(HKInc.Utils.Common.GlobalVariable.EncryptionKey, password);
        }

        public static string Encrypt(string textToEncrypt, string encryptionKey) //  encryptionKey = user password로 encrypt한다
        {            
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            // 입력받은 문자열을 바이트 배열로 변환  
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(textToEncrypt);

            // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서   
            // Salt를 사용한다.  
            byte[] Salt = Encoding.ASCII.GetBytes(encryptionKey.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(encryptionKey, Salt);

            // Create a encryptor from the existing SecretKey bytes.  
            // encryptor 객체를 SecretKey로부터 만든다.  
            // Secret Key에는 32바이트  
            // Initialization Vector로 16바이트를 사용  
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();

            // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언  
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;        
        }

        public static string Decrypt(string textToDescypt, string  decryptionKey) // decryptionKey is user password.
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            //decryptionKey = "HKIncEfframeWORKwithDevEXPress825488verSION";
            //decryptionKey = "YmSOFTEfframeWORKwithDevEXPress825488verSION";


            byte[] EncryptedData = Convert.FromBase64String(textToDescypt);
            byte[] Salt = Encoding.ASCII.GetBytes(decryptionKey.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(decryptionKey, Salt);

            // Decryptor 객체를 만든다.  
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            // 데이터 읽기 용도의 cryptoStream객체  
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            // 복호화된 데이터를 담을 바이트 배열을 선언한다.  
            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }
    }
}
