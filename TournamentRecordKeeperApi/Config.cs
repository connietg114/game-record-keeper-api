using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentRecordKeeperApi
{
    public class RootConfiguration
    {
        public GameRecordKeeperApiConfiguration GameRecordKeeperApiConfiguration { get; set; } = new GameRecordKeeperApiConfiguration();
        public ModernMagicIdentityServerConfiguration ModernMagicIdentityServerConfiguration { get; set; } = new ModernMagicIdentityServerConfiguration();
    }
    public class GameRecordKeeperApiConfiguration
    {
        public string GameRecordKeeperApiBaseUrl { get; set; }
    }

    public class ModernMagicIdentityServerConfiguration
    {
        public string ModernMagicIdentityServerBaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ResponseType { get; set; }
        public bool? UsePkce { get; set; }
    }

    public class Config
    {
        public static IEnumerable<Client> GetClients(GameRecordKeeperApiConfiguration config)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "game-record-keeper-swagger-ui",
                    ClientName = "Game Record Keeper Swagger UI",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("game".Sha256()) },
                    RequirePkce = true,
                    RequireConsent = false,

                    FrontChannelLogoutSessionRequired = true,
                    BackChannelLogoutSessionRequired = true,
                    EnableLocalLogin = true,
                    RedirectUris = { $"{config.GameRecordKeeperApiBaseUrl}/oauth2-redirect.html" },
                    AllowedCorsOrigins = new string[] { config.GameRecordKeeperApiBaseUrl },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "GameRecordKeeperAPI"
                    }
                }
            };
        }
    }
}
