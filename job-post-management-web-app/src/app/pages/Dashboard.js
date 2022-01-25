import React, { useEffect } from "react";
import { useLocation, Route, Switch, Redirect } from "react-router-dom";
// reactstrap components
import { Button, Container } from "reactstrap";
// core components
import AppNavbar from "../components/Navbar/Navbar.js";
import Footer from "../components/Footer/Footer.js";
import Sidebar from "../components/Sidebar/Sidebar.js";

import routes from "../routes.js";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom/cjs/react-router-dom.min";
import { logout } from "../store/auth/authSlice.js";

const Dashboard = (props) => {
  const dispatch= useDispatch();
  const user = useSelector(({ auth }) => auth.user);
  const history=useHistory();
  const mainContent = React.useRef(null);
  const location = useLocation();

  useEffect(() => {
  if(!user){
    history.push('/login');
  }
  }, [user,history]);


  useEffect(() => {
    document.documentElement.scrollTop = 0;
    document.scrollingElement.scrollTop = 0;
    mainContent.current.scrollTop = 0;
  }, [location]);

  const getRoutes = (routes) => {
    return routes.map((prop, key) => {
      if (prop.layout === "/admin") {
        return (
          <Route
            path={prop.layout + prop.path}
            component={prop.component}
            key={key}
          />
        );
      } else {
        return null;
      }
    });
  };

  const getBrandText = (path) => {
    for (let i = 0; i < routes.length; i++) {
      if (
        props.location.pathname.indexOf(routes[i].layout + routes[i].path) !==
        -1
      ) {
        return routes[i].name;
      }
    }
    return "Brand";
  };

  return (
    <>
      <Sidebar
        {...props}
        routes={routes}
        logo={{
          innerLink: "/admin/index",
          imgSrc: require("../../assets/img/brand/argon-react.png").default,
          imgAlt: "...",
        }}
      />
      <div className="main-content" ref={mainContent}>
        <AppNavbar
          {...props}
          brandText={getBrandText(props.location.pathname)}
        />
        <Switch>
          {getRoutes(routes)}
          <Redirect from="*" to="/dashboard" />
        </Switch>
        <Container fluid>
          <Footer />
        </Container>
      </div>
    </>
  );
};

export default Dashboard;
