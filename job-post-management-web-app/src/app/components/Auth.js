import React, { useEffect } from "react";
import {  useDispatch, useSelector } from "react-redux";
import { refreshToken } from "../store/auth/authSlice";

const Auth = (props) => {
  const dispatch = useDispatch();
  const checkingAuthentication = useSelector(({ auth }) => auth.checkingAuthentication);

  useEffect(() => {
    dispatch(refreshToken());
  }, []);

 if(checkingAuthentication)return <div>Loading...</div>
 else return <React.Fragment children={props.children}></React.Fragment>;
};

  
  export default Auth;
