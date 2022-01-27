import Dashboard from "./pages/Dashboard";
import Jobs from "./pages/Jobs";

var routes = [
  {
    path: "/home/dashboard",
    name: "Dashboard",
    icon: "ni ni-tv-2 text-primary",
    component: Dashboard,
  },
  {
    path: "/home/jobs",
    name: "Jobs",
    icon: "ni ni-planet text-blue",
    component: Jobs,
  },
  {
    path: "/home/profile",
    name: "User Profile",
    icon: "ni ni-single-02 text-yellow",
    // component: Profile,
  }
];
export default routes;
