import {
    Badge,
    Card,
    CardHeader,
    CardFooter,
    DropdownMenu,
    DropdownItem,
    UncontrolledDropdown,
    DropdownToggle,
    Media,
    Pagination,
    PaginationItem,
    PaginationLink,
    Progress,
    Table,
    Container,
    Row,
    UncontrolledTooltip,
  } from "reactstrap";
import Header from "../components/Header/Header";
  
  const Dashboard = () => {
    return (
      <>
        {/* Page content */}
        <Header/>
        <Container className="mt--7" fluid>
          {/* Table */}
          <Row>
            <div className="col">
              Dashboard
            </div>
          </Row>
        </Container>
      </>
    );
  };
  
  export default Dashboard;
  