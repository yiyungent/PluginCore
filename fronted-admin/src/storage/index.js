const storage = {
  set: (key, value) => {
    localStorage.setItem(key, value);
  },
  get: key => {
    const value = localStorage.getItem(key);
    return value;
  },
  remove: key => {
    localStorage.removeItem(key);
  },
  clear: () => {
    localStorage.clear();
  }
};

export default storage;
