import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import { Provider } from "react-redux";

import "@fortawesome/fontawesome-free/css/all.min.css";
import "./assets/plugins/nucleo/css/nucleo.css";
import "./assets/scss/argon-dashboard-react.scss";

import store from "./app/store";
import Home from "./app/pages/employer/Home.js";
import Login from "./app/pages/auth/Login.js";
import Register from "./app/pages/auth/Register.js";
import history from "./app/history";
import Auth from "./app/components/Auth";

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <Auth>
         <BrowserRouter history={history}>
          <Switch>
            <Route path="/home" render={(props) => <Home {...props} />} />
            <Route path="/login" render={(props) => <Login {...props} />} />
            <Route
              path="/register"
              render={(props) => <Register {...props} />}
            />
            <Redirect from="/" to="/home" />
          </Switch>
        </BrowserRouter>
      </Auth>
    </Provider>
  </React.StrictMode>,
  document.getElementById("root")
);
