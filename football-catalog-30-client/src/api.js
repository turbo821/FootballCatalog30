import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7200/api/football",
});
const hubUrl = "https://localhost:7200/playersHub";

export { api, hubUrl };