using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Terminal.Backend.Unit.Identity.Common;

public static class CodeEncoder
{
    public static string Decode(string code) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
    public static string Encode(string code) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
}
