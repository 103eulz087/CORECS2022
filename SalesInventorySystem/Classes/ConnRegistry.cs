using Microsoft.Win32;
using SalesInventorySystem;

public static class ConnRegistry
{
    private const string MainKeyPath = @"AAITCRE\ConnSettingsMain";
    private const string ServerKeyPath = @"AAITCRE\ConnSettingsServer";

    // 🔁 Generic key opener (for any path)
    private static RegistryKey OpenKey(string keyPath, bool writable)
    {
        return Registry.CurrentUser.CreateSubKey(keyPath, writable);
    }

    // ✅ Existing behavior (keeps compatibility)
    public static string Get(string name)
    {
        using (var key = OpenKey(MainKeyPath, false))
        {
            return key == null ? null : key.GetValue(name)?.ToString();
        }
    }

    public static void Set(string name, string value)
    {
        using (var key = OpenKey(MainKeyPath, true))
        {
            if (key == null) return;
            key.SetValue(name, value);
        }
    }

    // ✅ NEW: write to specific key (Main or Server)
    private static void SetTo(string keyPath, string name, string value)
    {
        using (var key = OpenKey(keyPath, true))
        {
            if (key == null) return;
            key.SetValue(name, value);
        }
    }

    // ✅ Existing method (now writes to BOTH)
    public static void SetTarget(string serverNameWithPort, string dbName, string userId, string password, int timeoutSeconds = 3600)
    {
        var dbconn =
            "Data Source=" + serverNameWithPort + ";" +
            "Initial Catalog=" + dbName + ";" +
            "User ID =" + userId + ";" +
            "Password=" + password + ";" +
            "Connection Timeout = " + timeoutSeconds + ";" +
            "Persist Security Info = True;";

        // 🔵 Write to MAIN (existing behavior)
        SetTo(MainKeyPath, "dbconn", dbconn);
        SetTo(MainKeyPath, "servername", serverNameWithPort);
        SetTo(MainKeyPath, "dbname", dbName);
        SetTo(MainKeyPath, "serverid", userId);
        SetTo(MainKeyPath, "serverpassword", password);

    }
    public static void SetTargetConnSettingsServer(string serverNameWithPort, string dbName, string userId, string password, int timeoutSeconds = 3600)
    {
        var dbconn =
            "Data Source=" + serverNameWithPort + ";" +
            "Initial Catalog=" + dbName + ";" +
            "User ID =" + userId + ";" +
            "Password=" + password + ";" +
            "Connection Timeout = " + timeoutSeconds + ";" +
            "Persist Security Info = True;";
        // 🟢 ALSO write to SERVER (your new requirement)
        //if (GlobalConfig.Token == "MTQ2NzgwNjAz" || GlobalConfig.Token == "ODM1NTI0ODYz"
        //            || GlobalConfig.Token == "NjQwOTg4MzU1" || GlobalConfig.Token == "ODU2NDE4OTA3")
        //{
        SetTo(ServerKeyPath, "dbconn", dbconn);
        SetTo(ServerKeyPath, "servername", serverNameWithPort);
        SetTo(ServerKeyPath, "dbname", dbName);
        SetTo(ServerKeyPath, "serverid", userId);
        SetTo(ServerKeyPath, "serverpassword", password);
        //}
    }
}