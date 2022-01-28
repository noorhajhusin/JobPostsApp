import { configureStore } from '@reduxjs/toolkit';
import authReducer from './auth/authSlice'
import employerDashboardSlice from '../pages/employer/employerDashboardSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    employer: employerDashboardSlice,
  },
});

export default store;
