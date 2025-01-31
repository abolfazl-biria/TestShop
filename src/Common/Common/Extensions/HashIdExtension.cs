using HashidsNet;

namespace Common.Extensions;

public static class HashIdExtension
{
    private const string HashSalt =
        "agret-lJa2faL25HFJsfjag812fFkGh123aFR9tG67gDha3r4#vgwsrKfjHdHbKhg6J2KWe";

    private const int HashLength = 12;
    private const string HashAlphabets = "abcdefghklmnoprstuvw123456789";

    private static readonly Hashids Hasher = new(HashSalt, HashLength, HashAlphabets);

    public static string Encode<T>(this T id) where T : struct =>
        id switch
        {
            int value => value.Encode(),
            _ => ""
        };

    public static string Encode(this int id) => Hasher.Encode(id);

    public static int Decode(this string eid)
    {
        try
        {
            return Hasher.Decode(eid)[0];
        }
        catch
        {
            return -1;
        }
    }

    public static List<int> Decode(this List<string> eid)
    {
        try
        {
            return eid.Select(item => Hasher.Decode(item)[0]).ToList();
        }
        catch
        {
            return [-1];
        }
    }
}