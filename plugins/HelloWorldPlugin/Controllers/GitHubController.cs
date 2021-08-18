using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using HelloWorldPlugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Octokit;

namespace PluginCore.IPlugins.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubClient _client;

        private readonly SettingsModel.GitHubOAuthModel _oAuth;

        public ISession Session
        {
            get
            {
                var session = this._httpContextAccessor.HttpContext.Session;

                return session;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GitHubController(IHttpContextAccessor httpContextAccessor)
        {
            SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("HelloWorldPlugin");
            this._oAuth = settingsModel.GitHubOAuth;
            this._client = new GitHubClient(new ProductHeaderValue(settingsModel.GitHubOAuth.AppName));

            this._httpContextAccessor = httpContextAccessor;
        }

        // repositories which include public and private repositories.
        public async Task<ActionResult> Index()
        {
            var accessToken = Session.GetString("OAuthToken");
            IReadOnlyList<Repository> repositories;
            if (accessToken != null)
            {
                _client.Credentials = new Credentials(accessToken);

                repositories = await _client.Repository.GetAllForCurrent();
            }
            else
            {
                return RedirectToAction("Login");
            }

            /* TODO: all the rest of the webapp */
            return Ok(repositories);
        }

        public async Task<ActionResult> Login()
        {
            // NOTE: this is not required, but highly recommended!
            // ask the ASP.NET Membership provider to generate a random value 
            // and store it in the current user's session
            string csrf = Guid.NewGuid().ToString();
            Session.SetString("CSRF:State", csrf);

            var request = new OauthLoginRequest(this._oAuth.ClientId)
            {
                Scopes = { },
                State = csrf,
                // other parameters
                RedirectUri = new Uri(this._oAuth.RedirectUri)
            };
            foreach (var scope in this._oAuth.Scopes)
            {
                request.Scopes.Add(scope);
            }


            // NOTE: user must be navigated to this URL
            var oauthLoginUrl = _client.Oauth.GetGitHubLoginUrl(request);

            return Redirect(oauthLoginUrl.AbsoluteUri);
        }

        public async Task<ActionResult> Callback(string code, string state)
        {
            if (String.IsNullOrEmpty(code))
                return RedirectToAction("Index");

            var expectedState = Session.GetString("CSRF:State");
            if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
            Session.Remove("CSRF:State");

            var request = new OauthTokenRequest(this._oAuth.ClientId, this._oAuth.ClientSecret, code);
            var token = await _client.Oauth.CreateAccessToken(request);
            Session.SetString("OAuthToken", token.AccessToken);

            return RedirectToAction("Index");
        }

    }
}
