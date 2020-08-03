import axios from "axios";

const http = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL,
});

const methods = {
  async get() {
    try {
      var result = await http.get("/app/info");
      return result.data;
    } catch {
      return { };
    }
  },
};

export default { methods };