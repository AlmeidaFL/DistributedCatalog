using System;
using System.Security.Cryptography;
using System.Text;

// Exemplo de uso
string plainPassword = "MinhaSenhaSegura123";
        
// Criptografar
string encryptedPassword = EncryptionUtil.Encrypt(plainPassword);
Console.WriteLine("Senha criptografada: " + encryptedPassword);

// Descriptografar
string decryptedPassword = EncryptionUtil.Decrypt(encryptedPassword);
Console.WriteLine("Senha descriptografada: " + decryptedPassword);