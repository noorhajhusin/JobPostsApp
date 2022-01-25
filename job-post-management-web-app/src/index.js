import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import { Provider } from "react-redux";

import "@fortawesome/fontawesome-free/css/all.min.css";
import "./assets/plugins/nucleo/css/nucleo.css";
import "./assets/scss/argon-dashboard-react.scss";

import store  from "./app/store"
import Dashboard from "./app/pages/Dashboard.js";
import Login from "./app/pages/Login.js";
import Register from "./app/pages/Register.js";

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <Switch>
          <Route path="/dashboard" render={(props) => <Dashboard {...props} />} />
          <Route path="/login" render={(props) => <Login {...props} />} />
          <Route path="/register" render={(props) => <Register {...props} />} />
          <Redirect from="/" to="/dashboard" />
        </Switch>
      </BrowserRouter>
    </Provider>
  </React.StrictMode>,
  document.getElementById("root")
);
