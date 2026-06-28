import React from "react";
import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7012",
});

export default api;
