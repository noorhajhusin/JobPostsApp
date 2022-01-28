import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom/cjs/react-router-dom.min";
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
  Button,
} from "reactstrap";
import Header from "../../../components/Headers/DashboardHeader";
import { loadJobsList } from "../employerDashboardSlice";
// core components

const Jobs = () => {
  const dispatch = useDispatch();
  const jobsList = useSelector(({ employer }) => employer.jobsList);
  const history = useHistory();

  const jobStatuses=[ 'Published', 'Closed' , 'Inactive', 'Pending']

  useEffect(() => {
    dispatch(loadJobsList());
  }, []);

  return (
    <>
      <div className="header bg-gradient-info pb-8 pt-5 pt-md-8">
        <Container fluid>
          <div className="header-body"></div>
        </Container>
      </div>
      <Container className="mt--7" fluid>
        <Row>
          <div className="col">
            <Card className="shadow">
              <CardHeader className="border-0">
                <div
                  style={{ display: "flex", justifyContent: "space-between" }}
                >
                  <h3 className="mb-0">My jobs</h3>
                  <Button
                    onClick={() => {
                      history.push("/home/jobs/new");
                    }}
                  >
                    Create New
                  </Button>
                </div>
              </CardHeader>
              <Table className="align-items-center table-flush" responsive>
                <thead className="thead-light">
                  <tr>
                    <th scope="col">Title</th>
                    <th scope="col">Date</th>
                    <th scope="col">Status</th>
                    <th scope="col">Candidates</th>
                    <th scope="col"></th>
                  </tr>
                </thead>
                <tbody>
                  {(!jobsList ||
                    !Array.isArray(jobsList) ||
                    jobsList.length === 0) && (
                    <tr>
                      <td colSpan={6}>
                        <div style={{ textAlign: "center" }}>
                          No added jobs!
                        </div>
                      </td>
                    </tr>
                  )}
                  {jobsList &&
                    Array.isArray(jobsList) &&
                    jobsList.length > 0 &&
                    jobsList.map((item, key) => {
                      return (
                        <tr>
                          <th scope="row">
                              <a
                                className=""
                                href="#pablo"
                                onClick={(e) => e.preventDefault()}
                              >
                                <span className="mb-0 text-sm">
                                  {item.title}
                                </span>
                              </a>
                          </th>
                          <td>{new Date(item.startDate).toLocaleDateString("en-US")}</td>
                          <td>
                            <strong>
                                  {jobStatuses[item.status]}
                            </strong>
                          </td>
                          <td>
                            <div className="avatar-group">
                              <a
                                className="avatar avatar-sm"
                                href="#pablo"
                                id="tooltip742438047"
                                onClick={(e) => e.preventDefault()}
                              >
                                <img
                                  alt="..."
                                  className="rounded-circle"
                                  src={require("../../../../assets/img/theme/team-1-800x800.jpg")}
                                />
                              </a>
                              <UncontrolledTooltip
                                delay={0}
                                target="tooltip742438047"
                              >
                                Ryan Tompson
                              </UncontrolledTooltip>
                              <a
                                className="avatar avatar-sm"
                                href="#pablo"
                                id="tooltip941738690"
                                onClick={(e) => e.preventDefault()}
                              >
                                <img
                                  alt="..."
                                  className="rounded-circle"
                                  src={require("../../../../assets/img/theme/team-2-800x800.jpg")}
                                />
                              </a>
                              <a
                                className="avatar avatar-sm"
                                href="#pablo"
                                id="tooltip804044742"
                                onClick={(e) => e.preventDefault()}
                              >
                                <img
                                  alt="..."
                                  className="rounded-circle"
                                  src={require("../../../../assets/img/theme/team-3-800x800.jpg")}
                                />
                              </a>
                              <a
                                className="avatar avatar-sm"
                                href="#pablo"
                                id="tooltip996637554"
                                onClick={(e) => e.preventDefault()}
                              >
                                <img
                                  alt="..."
                                  className="rounded-circle"
                                  src={require("../../../../assets/img/theme/team-4-800x800.jpg")}
                                />
                              </a>
                            </div>
                          </td>
                          <td className="text-right">
                              <Button
                                className="btn-icon-only text-light"
                                href="#pablo"
                                role="button"
                                size="sm"
                                color=""
                                onClick={(e) => e.preventDefault()}
                              >
                                <i className="fa fa-edit" />
                              </Button>
                              <Button
                                className="btn-icon-only text-light"
                                href="#pablo"
                                role="button"
                                size="sm"
                                color=""
                                onClick={(e) => e.preventDefault()}
                              >
                                <i className="fa fa-trash" />
                              </Button>
                          </td>
                        </tr>
                      );
                    })}
                </tbody>
              </Table>
              <CardFooter className="py-4">
                <nav aria-label="...">
                  {jobsList && Array.isArray(jobsList) && jobsList.length > 0 && (
                    <Pagination
                      className="pagination justify-content-end mb-0"
                      listClassName="justify-content-end mb-0"
                    >
                      <PaginationItem className="disabled">
                        <PaginationLink
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                          tabIndex="-1"
                        >
                          <i className="fas fa-angle-left" />
                          <span className="sr-only">Previous</span>
                        </PaginationLink>
                      </PaginationItem>
                      <PaginationItem className="active">
                        <PaginationLink
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                        >
                          1
                        </PaginationLink>
                      </PaginationItem>
                      <PaginationItem>
                        <PaginationLink
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                        >
                          2 <span className="sr-only">(current)</span>
                        </PaginationLink>
                      </PaginationItem>
                      <PaginationItem>
                        <PaginationLink
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                        >
                          3
                        </PaginationLink>
                      </PaginationItem>
                      <PaginationItem>
                        <PaginationLink
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                        >
                          <i className="fas fa-angle-right" />
                          <span className="sr-only">Next</span>
                        </PaginationLink>
                      </PaginationItem>
                    </Pagination>
                  )}
                </nav>
              </CardFooter>
            </Card>
          </div>
        </Row>
      </Container>
    </>
  );
};

export default Jobs;
