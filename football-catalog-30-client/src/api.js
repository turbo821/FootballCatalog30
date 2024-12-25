import axios from "axios";

const api = axios.create({
  baseURL: baseURL,
});

const hubUrl = process.env.REACT_APP_HUB_URL;

export { api, hubUrl };