namespace TaskManagement.Domain.Common.Settings.Hash;

public static class HashSettings
{
    public const int keySize = 64;
    public const int iterations = 350000;
    public static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
}
