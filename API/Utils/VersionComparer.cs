using System.Text.RegularExpressions;

namespace API.Utils;

public static class VersionComparer
{
    private static readonly Regex VersionRegex;

    static VersionComparer()
    {
        VersionRegex = new Regex(@"^\d+(\.\d+)*$");
    }
    public static int CompareVersions(string version1, string version2)
    {
        if (!IsValidVersion(version1) || !IsValidVersion(version2))
        {
            return 0;
        }
        
        var v1Parts = version1.Split('.');
        var v2Parts = version2.Split('.');
        
        int minLength = Math.Min(v1Parts.Length, v2Parts.Length);

        for (int i = 0; i < minLength; i++)
        {
            int v1Part = int.Parse(v1Parts[i]);
            int v2Part = int.Parse(v2Parts[i]);

            if (v1Part < v2Part)
                return -1;
            if (v1Part > v2Part)
                return 1;
        }

        // Handle the case when one version string is a prefix of the other
        if (v1Parts.Length < v2Parts.Length)
        {
            for (int i = v1Parts.Length; i < v2Parts.Length; i++)
            {
                if (int.Parse(v2Parts[i]) != 0)
                    return -1;
            }
        }

        if (v1Parts.Length > v2Parts.Length)
        {
            for (int i = v2Parts.Length; i < v1Parts.Length; i++)
            {
                if (int.Parse(v1Parts[i]) != 0)
                    return 1;
            }
        }

        return 0; // Versions are equal
    }
    
    private static bool IsValidVersion(string version)
    {
        return !string.IsNullOrEmpty(version) && VersionRegex.IsMatch(version);
    }
}