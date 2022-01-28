import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom/cjs/react-router-dom.min";
import {
  Button,
  Card,
  CardHeader,
  CardBody,
  FormGroup,
  Form,
  Input,
  InputGroupAddon,
  InputGroupText,
  InputGroup,
  Row,
  Col,
  Container,
} from "reactstrap";
import { register } from "../../store/auth/authSlice";

const Register = () => {
  const dispatch= useDispatch();
  const user = useSelector(({ auth }) => auth.user);
  const registerError = useSelector(({ auth }) => auth.registerError);
  const isLoading = useSelector(({ auth }) => auth.isLoading);
  const history=useHistory();
  useEffect(() => {
  if(user){
    history.push('/dashboard');
  }
  }, [user,history]);
  

  const handleSubmit=(e)=>{
    const firstName=e.target[0].value;
    const lastName=e.target[1].value;
    const email=e.target[2].value;
    const password=e.target[3].value;
    const confirmPassword=e.target[4].value;
    dispatch(register({firstName,lastName,email,password,confirmPassword}))
    e.preventDefault();
  }

  return (
    <div className="main-content">
      <div className="header bg-gradient-info py-7 py-lg-8">
        <Container>
          <div className="header-body text-center mb-7">
            <Row className="justify-content-center">
              <Col lg="5" md="6">
                <h1 className="text-white">Welcome!</h1>
                <p className="text-lead text-light">
                  Job posts website
                </p>
              </Col>
            </Row>
          </div>
        </Container>
        <div className="separator separator-bottom separator-skew zindex-100">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            preserveAspectRatio="none"
            version="1.1"
            viewBox="0 0 2560 100"
            x="0"
            y="0"
          >
            <polygon className="fill-default" points="2560 0 2560 100 0 100" />
          </svg>
        </div>
      </div>
      {/* Page content */}
      <Container className="mt--9 pb-5">
        <Row className="justify-content-center">
          <Col lg="6" md="8">
            <Card className="bg-secondary shadow border-0">
              <CardBody className="px-lg-5 py-lg-5">
                <div className="text-center text-muted mb-4">
                  <small>Please enter your information to Sign Up!</small>
                </div>
                {registerError&&<div className="text-center text-muted mb-4 text-red">
                  <small>Error in Email or passsword!</small>
                </div>}
                <Form onSubmit={handleSubmit} role="form">
                  <FormGroup>
                    <InputGroup className="input-group-alternative mb-3">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-hat-3" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input placeholder="First name" type="text" name="first-name"/>
                    </InputGroup>
                    <InputGroup className="input-group-alternative mb-3">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-hat-3" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input placeholder="Last name" type="text" name="last-name"/>
                    </InputGroup>
                  </FormGroup>
                  <FormGroup>
                    <InputGroup className="input-group-alternative mb-3">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-email-83" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        placeholder="Email"
                        type="email"
                        name="email"
                        autoComplete="new-email"
                      />
                    </InputGroup>
                  </FormGroup>
                  <FormGroup>
                    <InputGroup className="input-group-alternative">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-lock-circle-open" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        placeholder="Password"
                        type="password"
                        name="password"
                        autoComplete="new-password"
                      />
                    </InputGroup>
                  </FormGroup>
                  <FormGroup>
                    <InputGroup className="input-group-alternative">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-lock-circle-open" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        placeholder="Password confirmation"
                        type="password"
                        name="password-confirm"
                        autoComplete="new-password"
                      />
                    </InputGroup>
                  </FormGroup>
                  <Row className="my-4">
                    <Col xs="6">
                      <div className="custom-control custom-control-alternative custom-radio">
                        <input
                          className="custom-control-input"
                          id="employer"
                          type="radio"
                          checked
                          radioGroup="type" 
                        />
                        <label
                          className="custom-control-label"
                          htmlFor="employer"
                        >
                          <span className="text-muted">
                            Employer
                          </span>
                        </label>
                      </div>
                    </Col>
                    <Col xs="6">
                      <div className="custom-control custom-control-alternative custom-radio">
                        <input
                          className="custom-control-input"
                          id="candidate"
                          checked={false}
                          type="radio"
                          radioGroup="type" 
                        />
                        <label
                          className="custom-control-label"
                          htmlFor="candidate"
                        >
                          <span className="text-muted">
                            Job seeker
                          </span>
                        </label>
                      </div>
                    </Col>
                  </Row>
                  <br/>
                  <Row className="my-4">
                    <Col xs="12">
                      <div className="custom-control custom-control-alternative custom-checkbox">
                        <input
                          className="custom-control-input"
                          id="customCheckRegister"
                          type="checkbox"
                        />
                        <label
                          className="custom-control-label"
                          htmlFor="customCheckRegister"
                        >
                          <span className="text-muted">
                            I agree with the{" "}
                            <a
                              href="#pablo"
                              onClick={(e) => e.preventDefault()}
                            >
                              Privacy Policy
                            </a>
                          </span>
                        </label>
                      </div>
                    </Col>
                  </Row>
                  <div className="text-center">
                    <Button disabled={isLoading} className="mt-4" color="primary" type="submit">
                      Create account
                    </Button>
                  </div>
                </Form>
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default Register;
