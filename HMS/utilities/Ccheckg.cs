﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using HMS.Models;
using System.Data.SqlClient;
using System.Data;

namespace anchor1.utilities
{

    ///////////////////////////////////////////////////////////////////////////////
// SAMPLE: Symmetric key encryption and decryption using Rijndael algorithm.
// 
// To run this sample, create a new Visual C# project using the Console
// Application template and replace the contents of the Class1.cs file with
// the code below.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// 
// Copyright (C) 2002-2013 Obviex(TM). All rights reserved.
// 
/// <summary>
/// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
/// decrypt data. As long as encryption and decryption routines use the same
/// parameters to generate the keys, the keys are guaranteed to be the same.
/// The class uses static functions with duplicate code to make it easier to
/// demonstrate encryption and decryption logic. In a real-life application, 
/// this may not be the most efficient way of handling encryption, so - as
/// soon as you feel comfortable with it - you may want to redesign this class.
/// </summary>

    public static class Ccheckg
    {

    /// <summary>
    /// Encrypts specified plaintext using Rijndael symmetric key algorithm
    /// and returns a base64-encoded result.
    /// </summary>
    /// <param name="plainText">
    /// Plaintext value to be encrypted.
    /// </param>
    /// <param name="passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name="saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name="hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name="passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name="initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged class IV must be 
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name="keySize">
    /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    /// Longer keys are more secure than shorter keys.
    /// </param>
    /// <returns>
    /// Encrypted value formatted as a base64-encoded string.
    /// </returns>
        private static string Encrypt
        (
            string  plainText,
            string  passPhrase,
            string  saltValue,
            string  hashAlgorithm,
            int     passwordIterations,
            string  initVector,
            int     keySize
        )
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                encryptor,
                CryptoStreamMode.Write
            );

            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

    /// <summary>
    /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    /// </summary>
    /// <param name="cipherText">
    /// Base64-formatted ciphertext value.
    /// </param>
    /// <param name="passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name="saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name="hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name="passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name="initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged class IV must be
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name="keySize">
    /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    /// Longer keys are more secure than shorter keys.
    /// </param>
    /// <returns>
    /// Decrypted string value.
    /// </returns>
    /// <remarks>
    /// Most of the logic in this function is similar to the Encrypt
    /// logic. In order for decryption to work, all parameters of this function
    /// - except cipherText value - must match the corresponding parameters of
    /// the Encrypt function which was called to generate the
    /// ciphertext.
    /// </remarks>
        private static string Decrypt
        (
            string  cipherText,
            string  passPhrase,
            string  saltValue,
            string  hashAlgorithm,
            int     passwordIterations,
            string  initVector,
            int     keySize
        )
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes;
            // Convert our ciphertext into a byte array.
            try
            {
                cipherTextBytes = Convert.FromBase64String(cipherText);
            }
            catch
            {
                return "";
            }


            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor
            (
                keyBytes,
                initVectorBytes
            );

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                decryptor,
                CryptoStreamMode.Read
            );

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read
            (
                plainTextBytes,
                0,
                plainTextBytes.Length
            );

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString
            (
                plainTextBytes,
                0,
                decryptedByteCount
            );

            // Return decrypted string.   
            return plainText;
        }


        public static string convert_pass1(string pass1, int flag=0)
        {
            //string plainText = "Hello, World!";    // original plaintext
            // password only....
            string passPhrase = "Hex14fem5@";        // can be any string
            string saltValue = "L1m1ted#";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 3;                // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128

            if (flag==0)
                return Encrypt(pass1, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            else
                return Decrypt(pass1, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

        }

        public static string convert_pass2(string pass1, int flag = 0)
        {
            //string plainText = "Hello, World!";    // original plaintext

            string passPhrase = "Hex14fem5@";        // can be any string
            string saltValue = "L1m1ted#";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 3;                // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128
            worksess worksess = (worksess)HttpContext.Current.Session["worksess"];
            if (worksess == null)
                saltValue = "10";
            else
                saltValue = worksess.xyr.ToString();

            if (flag == 0)
            {
                string s = Encrypt(pass1, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
                //s = s.Split('=')[0]; // Remove any trailing '='s
                s = s.Replace('+', '-'); // 62nd char of encoding
                s = s.Replace('/', '_'); // 63rd char of encoding
                return s;
            }
            else
            {
                string s = pass1;
                s = s.Replace('-', '+'); // 62nd char of encoding
                s = s.Replace('_', '/'); // 63rd char of encoding
                return Decrypt(s, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            }


        }

        /// <summary>
        /// Returns a MD5 hash as a string
        /// </summary>
        /// <param name="TextToHash">String to be hashed.</param>
        /// <returns>Hash as string.</returns>
        public static String GetMD5Hash(String TextToHash)
        {
            //Check wether data was passed
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return String.Empty;
            }

            //Calculate MD5 hash. This requires that the string is splitted into a byte[].
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            //Convert result back to string.
            string sresult = System.BitConverter.ToString(result);
            sresult = sresult.Replace("-", "");
            return sresult;
        }


    }

}