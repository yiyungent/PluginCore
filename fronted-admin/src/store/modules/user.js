// import { login, logout, getInfo } from "@/api/user";
import { getToken, setToken, removeToken } from "@/utils/auth";
import { resetRouter } from "@/router";
import oidcService from "@/open-id-connect/oidcService";

const getDefaultState = () => {
  return {
    token: getToken(),
    userName: "",
    avatar: ""
  };
};

const state = getDefaultState();

const mutations = {
  RESET_STATE: state => {
    Object.assign(state, getDefaultState());
  },
  SET_TOKEN: (state, token) => {
    state.token = token;
  },
  SET_NAME: (state, userName) => {
    state.userName = userName;
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar;
  }
};

const actions = {
  // user login
  login({ commit }, token) {
    // oidcService.signIn();
    return new Promise((resolve, reject) => {
      if (!token) {
        return reject("Verification failed, please Login again.");
      }
      commit("SET_TOKEN", token);
      setToken(token);
      resolve(token);
    });
  },

  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      oidcService
        .getInfo()
        .then(userInfo => {
          if (!userInfo) {
            return reject("Verification failed, please Login again.");
          }

          const { userName, avatar } = userInfo;

          commit("SET_NAME", userName);
          commit("SET_AVATAR", avatar);
          resolve(userInfo);
        })
        .catch(error => {
          reject(error);
        });
    });
  },

  // user logout
  logout({ commit, state }) {
    return new Promise((resolve, reject) => {
      oidcService
        .signOut()
        .then(() => {
          removeToken(); // must remove  token  first
          resetRouter();
          commit("RESET_STATE");
          resolve();
        })
        .catch(error => {
          reject(error);
        });
    });
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      removeToken(); // must remove  token  first
      commit("RESET_STATE");
      resolve();
    });
  }
};

export default {
  namespaced: true,
  state,
  mutations,
  actions
};
