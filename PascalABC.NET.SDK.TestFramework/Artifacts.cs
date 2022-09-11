using System.IO.Compression;
using System.Security.Cryptography;

namespace PascalABC.NET.SDK.TestFramework;

public static class Artifacts
{
    // TODO: Replace with NuGet.
    private static Uri Url = new("http://pascalabc.net/downloads/PABCNETC.zip");
    private const string Sha256 = "0EE974C1038F3AEBAED40688FC7CD287454FBCFFE71E49FB231E90F97E5120D4";

    public static async Task EnsurePascalAbcNetDownloaded()
    {
        if (File.Exists(PascalAbcNetBinaryPath)) return;
        var file = await DownloadFile(Url, DownloadsDirectory, Sha256);
        UnpackArchive(file, PascalAbcNetDirectoryPath);
    }

    private static string DownloadsDirectory => Path.Combine(Path.GetTempPath(), "PascalABC.NET.SDK.Tests.Downloads");
    private static string PascalAbcNetDirectoryPath => Path.Combine(Path.GetTempPath(), "PABCNETC");
    private static string PascalAbcNetBinaryPath => Path.Combine(PascalAbcNetDirectoryPath, "pabcnetc.exe");

    public static async Task<string> GetPascalAbcNetDirectory()
    {
        await EnsurePascalAbcNetDownloaded();
        return PascalAbcNetDirectoryPath;
    }

    private static async Task<string> DownloadFile(Uri uri, string directory, string expectedSha256)
    {
        using var client = new HttpClient();
        var data = await client.GetByteArrayAsync(uri);
        using var sha256 = SHA256.Create();
        var actualSha256 = BitConverter.ToString(sha256.ComputeHash(data)).Replace("-", "");
        if (!expectedSha256.Equals(actualSha256, StringComparison.OrdinalIgnoreCase))
            throw new Exception($"Hash mismatch for URI {uri}: expected {expectedSha256}, got {actualSha256}.");

        var fileName = Path.GetFileName(uri.LocalPath);
        var filePath = Path.Combine(directory, fileName);
        await File.WriteAllBytesAsync(filePath, data);
        return filePath;
    }

    private static void UnpackArchive(string archive, string destinationPath)
    {
        ZipFile.ExtractToDirectory(archive, destinationPath);
    }
}
