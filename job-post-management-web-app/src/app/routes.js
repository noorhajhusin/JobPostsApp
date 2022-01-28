import Dashboard from "./pages/employer/dashboard/Dashboard";
import CreateJobPost from "./pages/employer/jobs/CreateJobPost";
import Jobs from "./pages/employer/jobs/Jobs";

var routes = [
  {
    path: "/home/jobs/new",
    component: CreateJobPost,
  },
  {
    path: "/home/dashboard",
    name: "Dashboard",
    icon: "ni ni-tv-2 text-primary",
    component: Dashboard,
    sidebar:true
  },
  {
    path: "/home/jobs",
    name: "Jobs",
    icon: "ni ni-planet text-blue",
    component: Jobs,
    sidebar:true
  },
  {
    path: "/home/profile",
    name: "User Profile",
    icon: "ni ni-single-02 text-yellow",
    // component: Profile,
    sidebar:true
  }
];
export default routes;
