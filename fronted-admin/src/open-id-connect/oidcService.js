import Oidc from "oidc-client";

// 参考: https://github.com/joaojosefilho/vuejsOidcClient/blob/master/src/services/SecurityService.js

var mgr = new Oidc.UserManager({
  userStore: new Oidc.WebStorageStateStore(),
  authority: "https://localhost:5001",
  client_id: "vuejsclient",
  // client_secret: "vuejsclient",
  redirect_uri: window.location.origin + "/oidc/callback",
  response_type: "id_token token",
  scope: "openid profile Remember.Core.WebApi",
  post_logout_redirect_uri: window.location.origin + "/oidc/logoutCallback",
  //   silent_redirect_uri: window.location.origin + "/static/silent-renew.html",
  accessTokenExpiringNotificationTime: 10,
  //   automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true
  // response_mode: "query" // 回调地址参数使用query?key1=val1&key2=val2方式, 默认 使用 # 分割
  // 源代码地址: https://github.com/IdentityModel/oidc-client-js/blob/dev/src/OidcClient.js#L105
  // 注意: 经测试，response_type: code 才能使用query方式, implicit 只能使用 #, 不过implicit不需要client_secret, 而 code方式需要
});

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;

mgr.events.addUserLoaded(function(user) {
  console.log("New User Loaded：", arguments);
  console.log("Acess_token: ", user.access_token);
});

mgr.events.addAccessTokenExpiring(function() {
  console.log("AccessToken Expiring：", arguments);
});

mgr.events.addAccessTokenExpired(function() {
  console.log("AccessToken Expired：", arguments);
  alert("Session expired. Going out!");
  mgr
    .signoutRedirect()
    .then(function(resp) {
      console.log("signed out", resp);
    })
    .catch(function(err) {
      console.log(err);
    });
});

mgr.events.addSilentRenewError(function() {
  console.error("Silent Renew Error：", arguments);
});

mgr.events.addUserSignedOut(function() {
  alert("Going out!");
  console.log("UserSignedOut：", arguments);
  mgr
    .signoutRedirect()
    .then(function(resp) {
      console.log("signed out", resp);
    })
    .catch(function(err) {
      console.log(err);
    });
});

// 对 Oidc.UserManager 进一步封装，方便使用
export default {
  // Renew the token manually
  renewToken: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .signinSilent()
        .then(function(user) {
          if (user == null) {
            self.signIn(null);
          } else {
            return resolve(user);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Get the user who is logged in
  getUser: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Check if there is any user logged in
  getSignedIn: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(false);
          } else {
            return resolve(true);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Redirect of the current window to the authorization endpoint.
  signIn: () => {
    mgr.signinRedirect().catch(function(err) {
      console.log(err);
    });
  },

  // Redirect of the current window to the end session endpoint
  signOut: () => {
    mgr
      .signoutRedirect()
      .then(function(resp) {
        console.log("signed out", resp);
      })
      .catch(function(err) {
        console.log(err);
      });
  },

  // Get the profile of the user logged in
  getProfile: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user.profile);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Get the token id
  getIdToken: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user.id_token);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Get the session state
  getSessionState: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user.session_state);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Get the access token of the logged in user
  getAcessToken: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user.access_token);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Takes the scopes of the logged in user
  getScopes: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user.scopes);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Get the user name logged in
  getName: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            return resolve(user.profile.name);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // Get the user info logged in
  getInfo: () => {
    const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn();
            return resolve(null);
          } else {
            var userInfo = {
              userName: user.profile.name,
              avatar: user.profile.avatar
            };
            return resolve(userInfo);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  },

  // 登录回调
  signinRedirectCallback: () => {
    // const self = this;
    return new Promise((resolve, reject) => {
      mgr
        .signinRedirectCallback()
        .then(function(user) {
          if (user == null) {
            // self.signIn();
            return resolve(null);
          } else {
            return resolve(user);
          }
        })
        .catch(function(err) {
          console.log(err);
          return reject(err);
        });
    });
  }
};
