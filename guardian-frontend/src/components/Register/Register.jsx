import { useRef, useState, useEffect } from "react";
import { faCheck, faTimes, faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import axios from "../../api/axios";
import "./Register.css"

const EMAIL_REGEX = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const NAME_REGEX = /^[a-zA-Z]+[a-zA-Z]+$/;
const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,24}$/;

const REGISTER_URL = '/Account/register';

const Register = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [email, setEmail] = useState('');
    const [validEmail, setValidEmail] = useState(false);
    const [emailFocus, setEmailFocus] = useState(false);

    const [userName, setUserName] = useState('');
    const [userNameFocus, setUserNameFocus] = useState(false);

    const [firstName, setFirstName] = useState('');
    const [validFirstName, setValidFirstName] = useState('');
    const [firstNameFocus, setFirstNameFocus] = useState('');

    const [lastName, setLastName] = useState('');
    const [validLastName, setValidLastName] = useState('');
    const [lastNameFocus, setLastNameFocus] = useState('');

    const [password, setPassword] = useState('');
    const [validPassword, setValidPassword] = useState('');
    const [passwordFocus, setPasswordFocus] = useState('');

    const [confirmPassword, setConfirmPassword] = useState('');
    const [validConfirmPassword, setValidConfirmPassword] = useState(false);
    const [confirmPasswordFocus, setConfirmPasswordFocus] = useState(false);

    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    const [responseMsg, setResponseMsg] = useState('');


    useEffect(() => {
        userRef.current.focus();
    }, [])

    useEffect(() => {
        const result = EMAIL_REGEX.test(email);
        setValidEmail(result);
    }, [email])

    useEffect(() => {
        const result = NAME_REGEX.test(firstName);

        setValidFirstName(result);
    }, [firstName])

    useEffect(() => {
        const result = NAME_REGEX.test(lastName);
        setValidLastName(result);
    }, [lastName])

    useEffect(() => {
        const result = PASSWORD_REGEX.test(password);
        setValidPassword(result);
        const confirm = password === confirmPassword;
        setValidConfirmPassword(confirm);
    }, [password, confirmPassword])



    useEffect(() => {
        setErrMsg('');
    }, [email, userName, firstName, lastName, password, confirmPassword])




    const handleSubmit = async (e) => {
        e.preventDefault();
        const v1 = EMAIL_REGEX.test(email);
        const v2 = PASSWORD_REGEX.test(password);
        const v3 = NAME_REGEX.test(firstName);
        const v4 = NAME_REGEX.test(lastName);
        if (!v1 || !v2 || !v3 || !v4) {
            setErrMsg("Invalid Entry");
            return;
        }
        try {
            const response = await axios.post(REGISTER_URL,
                JSON.stringify({ email, userName, firstName, lastName, password, confirmPassword }),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true
                }
            );

            console.log(response.data);
            console.log(response.accessToken);
            console.log(JSON.stringify(response))
            setSuccess(true);
            setEmail('');
            setUserName('');
            setFirstName('');
            setLastName('');
            setPassword('');
            setConfirmPassword('');
            setResponseMsg(response.data.message.toString());
        } catch (e) {
            if (!e?.response) {
                setErrMsg('No server response');
            } else if (e.response?.status === 409) {
                setErrMsg('Email Taken');
            } else {
                let err;
                if (typeof e.response.data.errors !== 'undefined') {
                    err = <div>
                        {
                            Object.entries(e.response.data.errors).map(([key, value]) =>
                                <div>{key}: {value[0]}</div>)
                        }
                    </div>
                } else {
                    err = e.response.data.ErrorMessage
                }

                setErrMsg(err)
            }
            errRef.current.focus();
        }
    }

    return (
        <>

            {success ? (
                <section>
                    <img src={require('../../assets/logo.png')} className="logo" alt="brand-logo" />
                    <h1 className="register-h1">Success!</h1>
                    <p className="confirmation-link">{responseMsg}</p>
                    <p>
                        <Link to="/login">Sign in</Link>
                    </p>
                </section>
            ) : (
                <section>
                    <img src={require('../../assets/logo.png')} className="logo" alt="brand-logo" />
                    <div ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</div>
                    <h1 className="register-h1">Create account</h1>
                    <form onSubmit={handleSubmit}>

                        <label htmlFor="email">
                            Email:
                            <FontAwesomeIcon className={validEmail ? "valid" : "hide"} icon={faCheck} />
                            <FontAwesomeIcon className={validEmail || !email ? "hide" : "invalid"} icon={faTimes} />
                        </label>
                        <input
                            type="text"
                            id="email"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            aria-invalid={validEmail ? "false" : "true"}
                            aria-describedby="uidnote"
                            onFocus={() => setEmailFocus(true)}
                            onBlur={() => setEmailFocus(false)}
                        />
                        <label htmlFor="userName">
                            User Name:
                        </label>
                        <input
                            type="text"
                            id="userName"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setUserName(e.target.value)}
                            required
                            aria-describedby="namenote"
                            onFocus={() => setUserNameFocus(true)}
                            onBlur={() => setUserNameFocus(false)}
                        />

                        <label htmlFor="firstName">
                            First Name:
                            <FontAwesomeIcon className={validFirstName ? "valid" : "hide"} icon={faCheck} />
                            <FontAwesomeIcon className={validFirstName || !firstName ? "hide" : "invalid"} icon={faTimes} />
                        </label>
                        <input
                            type="text"
                            id="firstName"
                            autoComplete="off"
                            onChange={(e) => setFirstName(e.target.value)}
                            required
                            aria-invalid={validFirstName ? "false" : "true"}
                            aria-describedby="namenote"
                            onFocus={() => setFirstNameFocus(true)}
                            onBlur={() => setFirstNameFocus(false)}
                        />
                        <p id="namenote" className={firstNameFocus && firstName && !validFirstName ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            Only letter allowed.
                        </p>


                        <label htmlFor="lastName">
                            Last Name:
                            <FontAwesomeIcon className={validLastName ? "valid" : "hide"} icon={faCheck} />
                            <FontAwesomeIcon className={validLastName || !lastName ? "hide" : "invalid"} icon={faTimes} />
                        </label>
                        <input
                            type="text"
                            id="lastName"
                            autoComplete="off"
                            onChange={(e) => setLastName(e.target.value)}
                            required
                            aria-invalid={validLastName ? "false" : "true"}
                            aria-describedby="namenote"
                            onFocus={() => setLastNameFocus(true)}
                            onBlur={() => setLastNameFocus(false)}
                        />
                        <p id="namenote" className={lastNameFocus && lastName && !validLastName ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            Only letters allowed.
                        </p>


                        <label htmlFor="password">
                            Password:
                            <FontAwesomeIcon className={validPassword ? "valid" : "hide"} icon={faCheck} />
                            <FontAwesomeIcon className={validPassword || !password ? "hide" : "invalid"} icon={faTimes} />
                        </label>
                        <input
                            type="password"
                            id="password"
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            aria-invalid={validPassword ? "false" : "true"}
                            aria-describedby="pwdnote"
                            onFocus={() => setPasswordFocus(true)}
                            onBlur={() => setPasswordFocus(false)}
                        />
                        <p id="pwdnote"
                            className={passwordFocus && password && !validPassword ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            8 to 24 characters. <br />
                            Must include uppercase and lowercase letters, and a number.
                        </p>


                        <label htmlFor="confirm_password">
                            Confirm Password:
                            <FontAwesomeIcon className={validConfirmPassword && confirmPassword ? "valid" : "hide"} icon={faCheck} />
                            <FontAwesomeIcon className={validConfirmPassword || !confirmPassword ? "hide" : "invalid"} icon={faTimes} />
                        </label>
                        <input
                            type="password"
                            id="confirm_password"
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                            aria-invalid={validConfirmPassword ? "false" : "true"}
                            aria-describedby="confirmnote"
                            onFocus={() => setConfirmPasswordFocus(true)}
                            onBlur={() => setConfirmPasswordFocus(false)}
                        />
                        <p id="confirmnote"
                            className={confirmPasswordFocus && !confirmPassword && !validConfirmPassword ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            Must match the first password input field.
                        </p>




                        <button disabled={!validEmail || !validPassword || !validConfirmPassword
                            || !validFirstName || !validLastName ? true : false}>Sign Up</button>
                    </form>
                    <p className="note">You are already registered?  <Link to="/login">Sign In</Link></p>

                </section>
            )}
        </>
    )
}

export default Register;