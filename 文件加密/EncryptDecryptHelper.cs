using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;
using System.IO;
//using System.Web.Security;

namespace 文件加密
{
    /// <summary>
    /// EncryptDecryptHelper 加密解密帮助类 SHA1,MD5,RSA,DES,3DES-1,RC2,3DES-2,AES,DSA
    /// </summary>
    public class EncryptDecryptHelper
    {
        #region 默认密钥向量
        //默认密钥向量 
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public const string strKeys = "QssdE4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk12s";
        #endregion


        #region 利用SHA1，MD5对字符串进行加密（不可解密）
        ///   <summary> 
        ///   利用MD5对字符串进行加密 
        ///   </summary> 
        ///   <param   name= "encryptString "> 待加密的字符串 </param> 
        ///   <returns> 返回加密后的字符串 </returns> 
        public static string EncryptMD5(string encryptString)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            UTF8Encoding Encode = new UTF8Encoding();
            byte[] HashedBytes = md5Hasher.ComputeHash(Encode.GetBytes(encryptString));
            return Encode.GetString(HashedBytes);
        }
        ///   <summary> 
        ///   利用SHA1对字符串进行加密 （不可解密）
        ///   </summary> 
        ///   <param   name= "encryptString "> 待加密的字符串 </param> 
        ///   <returns> 返回加密后的字符串 </returns> 
        public static string EncryptSHA1(string encryptString)
        {
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            UTF8Encoding Encode = new UTF8Encoding();
            byte[] HashedBytes = sha1Hasher.ComputeHash(Encode.GetBytes(encryptString));
            return Encode.GetString(HashedBytes);
        }
        #endregion
        #region 利用SHA1/MD5对字符串进行加密（不可解密）
        /// <summary> 
        /// SHA1加密字符串 （不可解密）
        /// </summary> 
        /// <param name="source">源字符串</param> 
        /// <returns>加密后的字符串</returns> 
        //public string SHA1(string source)
        //{
        //    return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        //}
        /// <summary> 
        /// MD5加密字符串 （不可解密）
        /// </summary> 
        /// <param name="source">源字符串</param> 
        /// <returns>加密后的字符串</returns> 
        //public string MD5(string source)
        //{
        //    return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5"); ;
        //}
        #endregion

        #region RSA


        #region rsa解密
        /// <summary>
        /// rsa解密
        /// </summary>
        /// <param name="s">加密后字符串字符串</param>
        /// <param name="key">加密key</param>
        /// <returns></returns>
        public static string RSADecrypt(string s, string key = strKeys)
        {
            string result = null;
            if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            CspParameters cspp = new CspParameters();
            cspp.KeyContainerName = key;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
            string[] decryptArray = s.Split(new string[] { "-" }, StringSplitOptions.None);
            byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (a => Convert.ToByte(byte.Parse(a, System.Globalization.NumberStyles.HexNumber))));
            byte[] bytes = rsa.Decrypt(decryptByteArray, true);
            result = System.Text.UTF8Encoding.UTF8.GetString(bytes);
            return result;
        }
        #endregion


        #region rsa加密
        /// <summary>
        /// rsa加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="key">加密key</param>
        /// <returns></returns>
        public static string RSAEncrypt(string s, string key = strKeys)
        {
            if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            CspParameters cspp = new CspParameters();
            cspp.KeyContainerName = key;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
            byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(s), true);
            return BitConverter.ToString(bytes);
        }
        #endregion


        #endregion


        #region DES


        #region DES加密字符串
        ///   <summary> 
        ///   DES加密字符串 
        ///   </summary> 
        ///   <param   name= "encryptString "> 待加密的字符串 </param> 
        ///   <param   name= "encryptKey "> 加密密钥,要求为8位 </param> 
        ///   <returns> 加密成功返回加密后的字符串，失败返回源串 </returns> 
        public static string EncryptDES(string encryptString, string encryptKey= strKeys)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        ///   <summary> 
        ///   DES加密字符串 
        ///   </summary> 
        ///   <param   name= "encryptString "> 待加密的字符串 </param> 
        ///   <param   name= "encryptKey "> 加密密钥,要求为8位 </param> 
        ///   <returns> 加密成功返回加密后的字符串，失败返回源串 </returns> 
        public static string EncryptDES(byte[] inputByteArray, string encryptKey= strKeys)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                //byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
        #endregion


        #region DES解密字符串
        ///   <summary> 
        ///   DES解密字符串 
        ///   </summary> 
        ///   <param   name= "decryptString "> 待解密的字符串 </param> 
        ///   <param   name= "decryptKey "> 解密密钥,要求为8位,和加密密钥相同 </param> 
        ///   <returns> 解密成功返回解密后的字符串，失败返源串 </returns> 
        public static string DecryptDES(string decryptString, string decryptKey = strKeys)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion


        #endregion


        #region 3DES-1


        #region 3DES加密
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="strString">需要加密的字符串</param>
        /// <param name="strKey">加密key</param>
        /// <returns></returns>
        public static string DES3Encrypt(string strString, string strKey = strKeys)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();


            DES.Key = hashMD5.ComputeHash(Encoding.Default.GetBytes(strKey));
            DES.Mode = CipherMode.ECB;


            ICryptoTransform DESEncrypt = DES.CreateEncryptor();


            byte[] Buffer = Encoding.Default.GetBytes(strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        #endregion


        #region 3DES解密
        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="strString">解密字符串</param>
        /// <param name="strKey">解密key</param>
        /// <returns></returns>
        public static string DES3Decrypt(string strString, string strKey = strKeys)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();


            DES.Key = hashMD5.ComputeHash(Encoding.Default.GetBytes(strKey)); DES.Mode = CipherMode.ECB;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(strString);
                result = Encoding.Default.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (System.Exception e)
            {
                throw (new System.Exception("null", e));
            }
            return result;
        }
        #endregion


        #endregion


        #region RC2


        #region RC2加密
        /// <summary>
        /// RC2加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static string RC2Encrypt(string encryptString, string encryptKey = strKeys)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                RC2CryptoServiceProvider rC2 = new RC2CryptoServiceProvider();
                byte[] byteEncryptString = Encoding.Default.GetBytes(encryptString);
                MemoryStream memorystream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memorystream, rC2.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncryptString, 0, byteEncryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Convert.ToBase64String(memorystream.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;


        }
        #endregion


        #region RC2解密
        /// <summary>
        /// RC2解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static string RC2Decrypt(string decryptString, string decryptKey = strKeys)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                RC2CryptoServiceProvider rC2 = new RC2CryptoServiceProvider();
                byte[] byteDecrytString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rC2.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecrytString, 0, byteDecrytString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Encoding.Default.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        #endregion


        #endregion


        #region 3DES-2


        #region 3DES 加密
        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="encryptString">待加密密文</param>
        /// <param name="encryptKey1">密匙1(长度必须为8位)</param>
        /// <param name="encryptKey2">密匙2(长度必须为8位)</param>
        /// <param name="encryptKey3">密匙3(长度必须为8位)</param>
        /// <returns></returns>
        public static string DES3Encrypt(string encryptString, string encryptKey1, string encryptKey2, string encryptKey3)
        {


            string returnValue;
            try
            {
                returnValue = EncryptDES(encryptString, encryptKey3);
                returnValue = EncryptDES(returnValue, encryptKey2);
                returnValue = EncryptDES(returnValue, encryptKey1);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;


        }
        #endregion


        #region 3DES 解密
        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey1">密匙1(长度必须为8位)</param>
        /// <param name="decryptKey2">密匙2(长度必须为8位)</param>
        /// <param name="decryptKey3">密匙3(长度必须为8位)</param>
        /// <returns></returns>
        public static string DES3Decrypt(string decryptString, string decryptKey1, string decryptKey2, string decryptKey3)
        {


            string returnValue;
            try
            {
                returnValue = DecryptDES(decryptString, decryptKey1);
                returnValue = DecryptDES(returnValue, decryptKey2);
                returnValue = DecryptDES(returnValue, decryptKey3);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        #endregion


        #endregion


        #region AES


        #region AES加密
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">加密密匙</param>
        /// <returns></returns>
        public static string AESEncrypt(string encryptString, string encryptKey = strKeys)
        {
            string returnValue;
            byte[] temp = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael AESProvider = Rijndael.Create();
            try
            {
                byte[] byteEncryptString = Encoding.Default.GetBytes(encryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, AESProvider.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncryptString, 0, byteEncryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Convert.ToBase64String(memoryStream.ToArray());
            }


            catch (Exception ex)
            {
                throw ex;
            }


            return returnValue;


        }
        #endregion


        #region AES解密
        /// <summary>
        ///AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(string decryptString, string decryptKey = strKeys)
        {
            string returnValue = "";
            byte[] temp = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael AESProvider = Rijndael.Create();
            try
            {
                byte[] byteDecryptString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, AESProvider.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Encoding.Default.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        #endregion


        #endregion


        #region DSA加密 暂没有实现
        /// <summary>
        ///DSA 加密
        /// </summary>
        /// <param name="decryptString">待加密密文</param>
        /// <param name="decryptKey">加密密钥</param>
        /// <returns></returns>
        public static string EncryptDSA(string encryptString, string key = strKeys)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(encryptString);
            //选择签名方式，有RSA和DSA 
            DSACryptoServiceProvider dsac = new DSACryptoServiceProvider();
            //byte[] byteEncryptString = Encoding.Default.GetBytes(encryptString);
            MemoryStream memorystream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memorystream, Aes.Create(key).CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
           
            //sign便是出来的签名结果。 
            return Convert.ToBase64String(memorystream.ToArray());
        }
        /// <summary>
        ///DSA 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string DecryptDSA(string decryptString, string key = strKeys)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(decryptString);
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            //byte[] byteDecryptString = Encoding.Default.GetBytes(key);
            MemoryStream memorystream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memorystream, Aes.Create(key).CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memorystream.ToArray()); 
        }
        /// <summary>
        /// 校验签名是否正确
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool VerifySignature(string decryptString, string key = strKeys)
        {
            bool ret = false;
            byte[] hashValue = { 22, 45, 78, 53, 1, 2, 205, 98, 75, 123, 45, 76, 143, 189, 205, 65, 12, 193, 211, 255 };
            DSACryptoServiceProvider signer = new DSACryptoServiceProvider();
            DSASignatureFormatter formatter = new DSASignatureFormatter(signer);
            formatter.SetHashAlgorithm("SHA1");
            byte[] signedHashValue = formatter.CreateSignature(hashValue);
            DSASignatureDeformatter deformatter = new DSASignatureDeformatter(signer);
            deformatter.SetHashAlgorithm("SHA1");

            ret = deformatter.VerifySignature(hashValue, signedHashValue);
            signer.Clear();
            return ret;
        }
        #endregion
    }
}
