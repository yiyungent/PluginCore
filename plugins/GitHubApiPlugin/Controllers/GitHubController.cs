//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using GitHubApiPlugin.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Octokit;
using PluginCore;

namespace GitHubApiPlugin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubClient _client;

        private readonly SettingsModel _settings;

        public const string TokenSessionKey = "GitHub.AccessToken";

        public const string StateSessionKey = "GitHub.CSRF:State";

        public ISession Session
        {
            get
            {
                var session = this._httpContextAccessor.HttpContext.Session;

                return session;
            }
        }

        public string BaseUrl
        {
            get
            {
                // 末尾 没有 /
                // 如果有端口号，则带上
                string baseUrl =
                    $"{this._httpContextAccessor.HttpContext.Request.Scheme}://{this._httpContextAccessor.HttpContext.Request.Host.Value}";

                return baseUrl;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GitHubController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._settings = PluginSettingsModelFactory.Create<SettingsModel>("GitHubApiPlugin");

            if (!_settings.HomePageUrl.StartsWith("http"))
            {
                _settings.HomePageUrl = this.BaseUrl + (_settings.HomePageUrl.StartsWith("/") ? _settings.HomePageUrl : $"/{_settings.HomePageUrl}");
            }
            if (!_settings.GitHubOAuth.RedirectUrl.StartsWith("http"))
            {
                _settings.GitHubOAuth.RedirectUrl = this.BaseUrl + (_settings.GitHubOAuth.RedirectUrl.StartsWith("/") ? _settings.GitHubOAuth.RedirectUrl : $"/{_settings.GitHubOAuth.RedirectUrl}");
            }

            // 注意: 每次请求都会 new Controller, 每次请求 Client 都是新实例
            this._client = new GitHubClient(new ProductHeaderValue(_settings.GitHubOAuth.AppName));
            var accessToken = Session.GetString(TokenSessionKey);
            if (!string.IsNullOrEmpty(accessToken))
            {
                this._client.Credentials = new Credentials(accessToken);
            }

        }

        #region APIs
        // repositories which include public and private repositories.
        public async Task<ActionResult<CommonResponseModel>> Index()
        {
            CommonResponseModel responseModel = new CommonResponseModel();
            try
            {
                var accessToken = Session.GetString(TokenSessionKey);
                IReadOnlyList<Repository> repositories;
                if (accessToken != null)
                {
                    repositories = await _client.Repository.GetAllForCurrent();
                    responseModel.Data = repositories;
                }
                else
                {
                    return RedirectToAction("Login");
                }

                responseModel.Code = 1;
                responseModel.Message = "成功";
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = ex.Message;
            }

            /* TODO: all the rest of the webapp */
            return await Task.FromResult(responseModel);
        }

        public async Task<ActionResult<CommonResponseModel>> SearchRepos(string q = "")
        {
            CommonResponseModel responseModel = new CommonResponseModel();
            try
            {
                // you can also specify a search term here
                var request = new SearchRepositoriesRequest(q);

                var result = await _client.Search.SearchRepo(request);
                if (result != null)
                {
                    responseModel.Data = result;
                }

                responseModel.Code = 1;
                responseModel.Message = "成功";
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = ex.Message;
            }

            return await Task.FromResult(responseModel);
        }
        #endregion

        #region 登录
        public async Task<ActionResult> Login()
        {
            // NOTE: this is not required, but highly recommended!
            // ask the ASP.NET Membership provider to generate a random value 
            // and store it in the current user's session
            string csrf = Guid.NewGuid().ToString();
            Session.SetString(StateSessionKey, csrf);

            var request = new OauthLoginRequest(this._settings.GitHubOAuth.ClientId)
            {
                Scopes = { },
                State = csrf,
                // other parameters
                RedirectUri = new Uri(this._settings.GitHubOAuth.RedirectUrl)
            };
            foreach (var scope in this._settings.GitHubOAuth.Scopes)
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
            {
                //return RedirectToAction("Index");
                return RedirectToIndex();
            }

            var expectedState = Session.GetString(StateSessionKey);
            if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
            Session.Remove(StateSessionKey);

            var request = new OauthTokenRequest(this._settings.GitHubOAuth.ClientId, this._settings.GitHubOAuth.ClientSecret, code);
            var token = await _client.Oauth.CreateAccessToken(request);
            Session.SetString(TokenSessionKey, token.AccessToken);
            //this._client.Credentials = new Credentials(token.AccessToken);

            return RedirectToIndex();
        }

        [NonAction]
        private ActionResult RedirectToIndex()
        {
            if (string.IsNullOrEmpty(this._settings.HomePageUrl))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect(this._settings.HomePageUrl);
            }
        }
        #endregion

    }
}
