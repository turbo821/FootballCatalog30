import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import AddPlayerPage from "./components/AddPlayerPage";
import PlayerListPage from "./components/PlayerListPage";
import Layout from "./components/Layout";

const App = () => {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/add-player" element={<AddPlayerPage />} />
          <Route path="/" element={<PlayerListPage />} />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;
