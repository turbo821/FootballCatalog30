import React from "react";
import { Link } from "react-router-dom";

const Layout = ({ children }) => {
  return (
    <div className="d-flex flex-column min-vh-100">
      <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
        <div className="container">
          <Link to="/" className="navbar-brand">
            Football Catalog 3.0
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav">
              <li className="nav-item">
                <Link to="/add-player" className="nav-link">
                  Добавить футболиста
                </Link>
              </li>
              <li className="nav-item">
                <Link to="/" className="nav-link">
                  Каталог футболистов
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>

      <main className="container my-4 flex-grow-1">{children}</main>

      <footer className="bg-dark text-white text-center py-3 mt-auto">
        <p className="mb-0">&copy; 2024 Football Catalog 3.0. Все права защищены.</p>
      </footer>
    </div>
  );
};

export default Layout;
