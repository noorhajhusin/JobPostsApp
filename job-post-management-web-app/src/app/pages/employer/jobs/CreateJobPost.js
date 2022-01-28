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
  CardBody,
  Button,
  Form,
  FormGroup,
  InputGroup,
  InputGroupAddon,
  Input,
  InputGroupText,
  Col,
  Label,
} from "reactstrap";
import Header from "../../../components/Headers/DashboardHeader";
import { createJobPost, loadJobsList } from "../employerDashboardSlice";
// core components

const CreateJobPost = () => {
  const dispatch = useDispatch();
  const history = useHistory();

  const handleSubmit = (e) => {
    const title = e.target[0].value;
    const description = e.target[1].value;
    const startDate = e.target[2].value;
    const endDate = e.target[3].value;
    const deadLineDate = e.target[4].value;
    const address = e.target[5].value;
    dispatch(createJobPost({title,description,startDate,endDate,deadLineDate,address}))
    e.preventDefault();
  };

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
                <h3 className="mb-0">Create new job post</h3>
              </CardHeader>
              <CardBody>
                <Form onSubmit={handleSubmit} role="form">
                  <FormGroup>
                    <Label>Job post title</Label>
                    <InputGroup className="input-group-alternative mb-3">
                      <Input
                        type="text"
                        name="title"
                      />
                    </InputGroup>
                  </FormGroup>
                  <FormGroup>
                    <Label>Job description</Label>
                    <InputGroup className="input-group-alternative mb-3">
                      <Input
                        type="textarea"
                        size={5}
                        name="description"
                        aria-multiline
                        rows="5"
                      />
                    </InputGroup>
                  </FormGroup>
                  <Row className="my-4">
                    <Col md="4">
                      <FormGroup>
                        <Label>Start date</Label>
                        <InputGroup className="input-group-alternative">
                          <Input
                            type="date"
                            name="startdate"
                          />
                        </InputGroup>
                      </FormGroup>
                    </Col>
                    <Col md="4">
                      <FormGroup>
                        <Label>End date</Label>
                        <InputGroup className="input-group-alternative">
                          <Input
                            type="date"
                            name="startdate"
                          />
                        </InputGroup>
                      </FormGroup>
                    </Col>
                    <Col md="4">
                      <FormGroup>
                        <Label>Due date</Label>
                        <InputGroup className="input-group-alternative">
                          <Input
                            type="date"
                            name="startdate"
                          />
                        </InputGroup>
                      </FormGroup>
                    </Col>
                  </Row>
                  <FormGroup>
                    <Label>Job address</Label>
                    <InputGroup className="input-group-alternative mb-3">
                      <Input
                        type="text"
                        name="address"
                      />
                    </InputGroup>
                  </FormGroup>
                  <br />
                  <div className="text-center">
                    <Button className="mt-4" color="primary" type="submit">
                      Create job post
                    </Button>
                  </div>
                </Form>
              </CardBody>
              <CardFooter className="py-4">
                <nav aria-label="..."></nav>
              </CardFooter>
            </Card>
          </div>
        </Row>
      </Container>
    </>
  );
};

export default CreateJobPost;
