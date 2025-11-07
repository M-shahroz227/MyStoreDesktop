using System;
using System.Security.Cryptography;
using System.Text;

namespace MyStoreDesktop.Services
{
    public static class CredentialManager
    {
        // Simple encryption key (in production, use a more secure approach)
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("MyStore2025SecureKey1234567890AB"); // 32 bytes for AES-256

        /// <summary>
        /// Save credentials to local storage
        /// </summary>
        public static void SaveCredentials(string username, string password)
        {
            try
            {
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.SavedPassword = Encrypt(password);
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error saving credentials: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Load saved credentials
        /// </summary>
        public static (string username, string password, bool rememberMe) LoadCredentials()
        {
            try
            {
                bool rememberMe = Properties.Settings.Default.RememberMe;
                if (!rememberMe)
                {
                    return (string.Empty, string.Empty, false);
                }

                string username = Properties.Settings.Default.SavedUsername ?? string.Empty;
                string encryptedPassword = Properties.Settings.Default.SavedPassword ?? string.Empty;
                string password = string.IsNullOrEmpty(encryptedPassword) ? string.Empty : Decrypt(encryptedPassword);

                return (username, password, rememberMe);
            }
            catch (Exception)
            {
                return (string.Empty, string.Empty, false);
            }
        }

        /// <summary>
        /// Clear saved credentials
        /// </summary>
        public static void ClearCredentials()
        {
            try
            {
                Properties.Settings.Default.SavedUsername = string.Empty;
                Properties.Settings.Default.SavedPassword = string.Empty;
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error clearing credentials: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Encrypt a string using AES
        /// </summary>
        private static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.GenerateIV();

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // Prepend IV to encrypted data
                byte[] result = new byte[aes.IV.Length + encryptedBytes.Length];
                Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

                return Convert.ToBase64String(result);
            }
        }

        /// <summary>
        /// Decrypt a string using AES
        /// </summary>
        private static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;

                // Extract IV from the beginning
                byte[] iv = new byte[aes.IV.Length];
                byte[] cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
