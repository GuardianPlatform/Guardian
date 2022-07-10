import { useRef, useState, useEffect } from "react";
import { Link, useNavigate, useLocation } from 'react-router-dom';
import "./Login.css";

import axios from "../../api/axios";
const LOGIN_URL = "Account/authenticate";

const Login = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/";


    const userRef = useRef();
    const errRef = useRef();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errMsg, setErrMsg] = useState('');


    useEffect(() => {
        userRef.current.focus();
    }, []);

    useEffect(() => {
        setErrMsg('');
    }, [email, password]);



    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post(LOGIN_URL,
                JSON.stringify({ email, password }),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true
                }
            );

            const data = response?.data.data;
            const token = data.jwToken;
            const roles = data.roles;
            const user = data.userName;

            localStorage.setItem('token', `Bearer ${token}`);
            localStorage.setItem('roles', roles)
            localStorage.setItem('user', user)

            setEmail('');
            setPassword('');
            navigate(from, { replace: true });
        } catch (e) {
            if (!e?.response) {
                setErrMsg("No Server Response");
            } else if (e.response?.status === 400) {
                setErrMsg('Missing Email or Password');
            } else {
                setErrMsg('Login Failed');
            }
            if (typeof e.response.data.errors !== 'undefined') {
                setErrMsg(e.response.data.errors);
            } else if (typeof e.response.data.ErrorMessage !== 'undefined') {
                setErrMsg(e.response.data.ErrorMessage)
            }
            errRef?.current?.focus();
        }

    }
    return (

        <section>
            <img src={require('../../assets/logo.png')} className="logo" alt="brand-logo" />
            <h1>Guardian Games</h1>
            <div ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</div>

            <form onSubmit={handleSubmit}>
                <label htmlFor="email">Email:</label>
                <input
                    type="text"
                    id="email"
                    ref={userRef}
                    autoComplete="off"
                    onChange={(e) => setEmail(e.target.value)}
                    value={email}
                    required
                />

                <label htmlFor="password">Password:</label>
                <input
                    type="password"
                    id="password"
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
                    required
                />
                <button>Sign In</button>
            </form>
            <p>
                You don't have an account yet? <br />
                <Link to="/register">Sign Up</Link>
            </p>
        </section>

    );
}

export default Login;