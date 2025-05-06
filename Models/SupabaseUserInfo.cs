using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Web;

namespace TrackNWork.Models
{
    public class SupabaseUserInfo
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string BauhofId { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }

        public static SupabaseUserInfo FromClaimsPrincipal(ClaimsPrincipal user)
        {
            var info = new SupabaseUserInfo
            {
                Email = user.FindFirst("email")?.Value
                         ?? user.FindFirst(ClaimTypes.Email)?.Value
                         ?? user.FindFirst("sub")?.Value,
                UserId = user.FindFirst("sub")?.Value
            };

            var metadata = user.FindFirst("user_metadata")?.Value;

            if (!string.IsNullOrEmpty(metadata))
            {
                try
                {
                    var json = JsonDocument.Parse(metadata).RootElement;

                    if (json.TryGetProperty("display_name", out var dn))
                        info.DisplayName = dn.GetString();

                    if (json.TryGetProperty("bauhof_id", out var bid))
                        info.BauhofId = bid.GetString();

                    if (json.TryGetProperty("role", out var r))
                        info.Role = r.GetString();
                }
                catch
                {
                    // Ignoriere Fehler in der JSON-Struktur
                }
            }

            return info;
        }
    }
}