using System.Security.Cryptography;
using HoneyShop.Core.Contracts.Security;
using HoneyShop.Core.Excpetions;

namespace HoneyShop.BusinessLogic.Security;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16; // Размер соли (16 байт)
    private const int HashSize = 32; // Размер хеша (32 байта)
    private const int Iterations = 10000; // Количество итераций для PBKDF2

    // Хеширование пароля
    public string HashPassword(string password)
    {
        try
        {
            // Генерация соли
            var salt = GenerateSalt();

            // Хеширование пароля с солью
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                var hash = pbkdf2.GetBytes(HashSize);
                var hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                return Convert.ToBase64String(hashBytes);
            }
        }
        catch (ArgumentNullException e)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (Exception e)
        {
            throw new HoneyException("Ошибка при хешировании пароля!", 500);
        }
    }

    // Проверка пароля
    public bool VerifyPassword(string hashedPassword, string password)
    {
        try
        {
            // Декодируем хешированный пароль
            var hashBytes = Convert.FromBase64String(hashedPassword);

            // Извлекаем соль из хешированного пароля
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Хешируем введённый пароль с тем же количеством итераций и той же солью
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                var hash = pbkdf2.GetBytes(HashSize);
                // Сравниваем хеши
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        catch (ArgumentNullException e)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (FormatException e)
        {
            throw new HoneyException("Неверный формат хешированного пароля!", 400);
        }
        catch (Exception e)
        {
            throw new HoneyException("Ошибка при проверке пароля!", 500);
        }
    }

    // Генерация соли
    private static byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);
            return salt;
        }
    }
}
