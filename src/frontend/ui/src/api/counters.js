import axios from "axios";

const http = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL,
});

const methods = {
  async incrementLocal() {
    try {
      var result = await http.post("/counters/local", { key: "default-key" });
      return result.data;
    } catch {
      return { value: 0, key: "null" };
    }
  },

  async incrementGlobal() {
    try {
      var result = await http.post("/counters/global", { key: "default-key" });
      return result.data;
    } catch {
      return { value: 0, key: "null" };
    }
  },
};

console.log(process.env.VUE_APP_API_BASE_URL);

export default { methods };
